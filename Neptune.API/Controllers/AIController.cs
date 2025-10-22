using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis; // Added for Experimental attribute
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using OpenAI;
using OpenAI.Files;
using OpenAI.Responses;

namespace Neptune.API.Controllers;

[ApiController]
[Route("ai")]
public class AIController(
    NeptuneDbContext dbContext,
    ILogger<AIController> logger,
    KeystoneService keystoneService,
    IOptions<NeptuneConfiguration> appConfiguration,
    AzureBlobStorageService azureBlobStorageService,
    OpenAIClient openAIClient)
    : SitkaController<AIController>(dbContext, logger, keystoneService, appConfiguration)
{
#pragma warning disable OPENAI001 // Suppress experimental OpenAI SDK warnings for evaluation usage

    private const string ExtractorInstructions =
        "You are part of an automated workflow and must return only JSON. No narrative, no follow-up suggestions. " +
        "You are helping a stormwater technician extract data from a Water Quality Management Plan (WQMP) used in Orange County California. " +
        "If an attribute is not found output null for all of its child fields. Do not hallucinate.\n";

    private const string SchemaVersion = "v1.0"; // retained only for overall extraction context

    private static readonly FileUploadPurpose VectorStoreFileUploadPurpose = FileUploadPurpose.UserData;

    private static readonly Lazy<string> ExtractedValueSchema = new(BuildExtractedValueJsonSchema);
    private static readonly Lazy<string> WqmpSchema = new(BuildWqmpSchemaJson);
    private static readonly Lazy<string> ParcelSchema = new(BuildParcelSchemaJson);
    private static readonly Lazy<string> TreatmentBmpSchema = new(BuildTreatmentBmpSchemaJson);
    private static readonly Lazy<string> SourceControlBmpSchema = new(BuildSourceControlBmpSchemaJson);

    [HttpPost("water-quality-management-plan-documents/{waterQualityManagementPlanDocumentID}/extract-all")]
    [AdminFeature]
    [Experimental("OPENAI001")]
    public async Task<ActionResult<WaterQualityManagementPlanDocumentExtractionResultDto>> ExtractAll([FromRoute] int waterQualityManagementPlanDocumentID)
    {
        var docDto = await WaterQualityManagementPlanDocuments.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
        if (docDto == null) return NotFound();

        var cancellationToken = HttpContext.RequestAborted;
        var vectorStoreId = await EnsureVectorStoreWithFileAsync(waterQualityManagementPlanDocumentID, docDto);
        var domainContext = await BuildSchemaAndDomainContext();

        var extractionEvidenceInstructions =
            $"SchemaVersion: {SchemaVersion}. Use ONLY the provided WQMP PDF. Each attribute object MUST match ExtractedValueSchema. " +
            "Value = raw extracted string or null; ExtractionEvidence = source snippet (preceding sentence, target sentence, following sentence OR nearby table text); DocumentSource = page reference (e.g. 'Page 12'). " +
            "If not found set Value, ExtractionEvidence, DocumentSource to null. Do not add or rename properties.\n" +
            $"ExtractedValueSchema: {ExtractedValueSchema.Value}";

        var categoryPrompts = new Dictionary<string, string>
        {
            ["WQMP"] = $"{ExtractorInstructions}{extractionEvidenceInstructions} Extract root WQMP attributes. WqmpSchema: {WqmpSchema.Value} Return a single JSON object.",
            ["Parcels"] = $"{ExtractorInstructions}{extractionEvidenceInstructions} Extract all Parcels. ParcelSchema: {ParcelSchema.Value} Return a JSON array (empty array if none).",
            ["TreatmentBMPs"] = $"{ExtractorInstructions}{extractionEvidenceInstructions} Extract all Treatment BMPs. TreatmentBmpSchema: {TreatmentBmpSchema.Value} Return a JSON array (empty array if none).",
            ["SourceControlBMPs"] = $"{ExtractorInstructions}{extractionEvidenceInstructions} Extract all Source Control BMPs. SourceControlBmpSchema: {SourceControlBmpSchema.Value} Return a JSON array (empty array if none)."
        };

        var responseClient = CreateResponseClient();
        var fileSearchTool = CreateFileSearchTool(vectorStoreId);

        async Task<string> ExtractCategoryAsync(string key, string prompt, bool expectArray)
        {
            var fullPrompt = domainContext + "\n\n" + prompt;
            var response = await responseClient.CreateResponseAsync(fullPrompt, new ResponseCreationOptions { Tools = { fileSearchTool } }, cancellationToken);
            var output = ExtractMessageText(response.Value.OutputItems);
            if (!IsValidJson(output) || !MatchesExtractionSchema(output, expectArray))
            {
                logger.LogWarning("Initial JSON invalid or schema mismatch for {Category}. Retrying.", key);
                var retryPrompt = fullPrompt + "\n\nPrevious output invalid. Re-output ONLY valid JSON per schema. No narrative.";
                var retryResp = await responseClient.CreateResponseAsync(retryPrompt, new ResponseCreationOptions { Tools = { fileSearchTool } }, cancellationToken);
                output = ExtractMessageText(retryResp.Value.OutputItems);
                if (!IsValidJson(output) || !MatchesExtractionSchema(output, expectArray))
                {
                    logger.LogError("Retry still invalid for {Category}. Using empty fallback.", key);
                    output = expectArray ? "[]" : "{}";
                }
            }
            return output;
        }

        var tasks = new List<Task<string>>
        {
            ExtractCategoryAsync("WQMP", categoryPrompts["WQMP"], false),
            ExtractCategoryAsync("Parcels", categoryPrompts["Parcels"], true),
            ExtractCategoryAsync("TreatmentBMPs", categoryPrompts["TreatmentBMPs"], true),
            ExtractCategoryAsync("SourceControlBMPs", categoryPrompts["SourceControlBMPs"], true)
        };
        var results = await Task.WhenAll(tasks);
        var map = new Dictionary<string,string>{{"WQMP",results[0]},{"Parcels",results[1]},{"TreatmentBMPs",results[2]},{"SourceControlBMPs",results[3]}};

        var consolidationPrompt =
            $"Consolidate into single JSON: {{ 'SchemaVersion': '{SchemaVersion}', 'WQMP': {map["WQMP"]}, 'Parcels': {map["Parcels"]}, 'TreatmentBMPs': {map["TreatmentBMPs"]}, 'SourceControlBMPs': {map["SourceControlBMPs"]} }}. Preserve values exactly.";
        var consolidationFullPrompt = domainContext + "\n\n" + consolidationPrompt;
        var consolidationResponse = await responseClient.CreateResponseAsync(consolidationFullPrompt, new ResponseCreationOptions { Tools = { fileSearchTool } }, cancellationToken);
        var finalOutput = ExtractMessageText(consolidationResponse.Value.OutputItems);
        if (!IsValidJson(finalOutput) || !MatchesConsolidatedSchema(finalOutput))
        {
            logger.LogWarning("Consolidation invalid. Retrying.");
            var retryPrompt = consolidationFullPrompt + "\n\nPrevious output invalid. Return only valid consolidated JSON.";
            var retryResp = await responseClient.CreateResponseAsync(retryPrompt, new ResponseCreationOptions { Tools = { fileSearchTool } }, cancellationToken);
            finalOutput = ExtractMessageText(retryResp.Value.OutputItems);
            if (!IsValidJson(finalOutput) || !MatchesConsolidatedSchema(finalOutput))
            {
                logger.LogError("Still invalid. Using fallback consolidated object.");
                finalOutput = $"{{ \"SchemaVersion\": \"{SchemaVersion}\", \"WQMP\": {map["WQMP"]}, \"Parcels\": {map["Parcels"]}, \"TreatmentBMPs\": {map["TreatmentBMPs"]}, \"SourceControlBMPs\": {map["SourceControlBMPs"]} }}";
            }
        }

        var rawBuilder = new System.Text.StringBuilder();
        foreach (var kvp in map) rawBuilder.AppendLine($"{kvp.Key}: {kvp.Value}");
        var extractionResult = new WaterQualityManagementPlanDocumentExtractionResultDto { FinalOutput = finalOutput, RawResults = rawBuilder.ToString(), ExtractedAt = DateTime.UtcNow };
        return Ok(extractionResult);
    }

    private static string BuildExtractedValueJsonSchema()
    {
        var schema = new
        {
            type = "object",
            description = "ExtractedValue schema (SchemaVersion " + SchemaVersion + "). Attribute with evidence.",
            properties = new
            {
                Value = new { type = "string", description = "Raw extracted value or null." },
                ExtractionEvidence = new { type = "string", description = "Snippet: preceding, target, following sentence OR nearby table text." },
                DocumentSource = new { type = "string", description = "Page reference (e.g. 'Page 12')." }
            },
            required = new[] { "Value", "ExtractionEvidence", "DocumentSource" },
            additionalProperties = false
        };
        return JsonSerializer.Serialize(schema);
    }

    private static string BuildWqmpSchemaJson()
    {
        var required = new[]
        {
            "WaterQualityManagementPlanName","Jurisdiction","MaintenanceContactName","MaintenanceContactOrganization","MaintenanceContactPhone","MaintenanceContactAddress1","MaintenanceContactAddress2","MaintenanceContactCity","MaintenanceContactState","MaintenanceContactZip","WaterQualityManagementPlanPermitTerm","HydrologicSubarea","RecordNumber","RecordedWQMPAreaInAcres","TrashCaptureStatusType"
        };
        var schema = new
        {
            type = "object",
            description = "WQMP root schema (uses ExtractedValue objects). Nullable fields may be null.",
            properties = new Dictionary<string, object>
            {
                ["WaterQualityManagementPlanName"] = Prop("Title of the WQMP."),
                ["Jurisdiction"] = Prop("Jurisdiction responsible."),
                ["WaterQualityManagementPlanLandUse"] = PropNullable("Land use classification."),
                ["WaterQualityManagementPlanPriority"] = PropNullable("Priority category."),
                ["WaterQualityManagementPlanStatus"] = PropNullable("Current status."),
                ["WaterQualityManagementPlanDevelopmentType"] = PropNullable("Development type."),
                ["ApprovalDate"] = PropNullable("Approval date."),
                ["MaintenanceContactName"] = Prop("Maintenance contact or owner name. "),
                ["MaintenanceContactOrganization"] = Prop("Maintenance contact or owner organization."),
                ["MaintenanceContactPhone"] = Prop("Maintenance contact phone."),
                ["MaintenanceContactAddress1"] = Prop("Address line 1."),
                ["MaintenanceContactAddress2"] = Prop("Address line 2."),
                ["MaintenanceContactCity"] = Prop("Address city."),
                ["MaintenanceContactState"] = Prop("Address state."),
                ["MaintenanceContactZip"] = Prop("Address ZIP."),
                ["WaterQualityManagementPlanPermitTerm"] = Prop("Permit term."),
                ["DateOfConstruction"] = PropNullable("Construction completion date."),
                ["HydrologicSubarea"] = Prop("Hydrologic subarea."),
                ["RecordNumber"] = Prop("Agency record number."),
                ["RecordedWQMPAreaInAcres"] = Prop("Area in acres."),
                ["TrashCaptureStatusType"] = Prop("Trash capture status.")
            },
            required,
            additionalProperties = false
        };
        return JsonSerializer.Serialize(schema);

        object Prop(string description) => new { type = "object", description };
        object PropNullable(string description) => new { type = "object", description, nullable = true };
    }

    private static string BuildParcelSchemaJson()
    {
        var schema = new
        {
            type = "object",
            description = "Parcel schema (ExtractedValue objects).",
            properties = new Dictionary<string, object>
            {
                ["ParcelNumber"] = new { type = "object", description = "APN (e.g. XXX-XX-XXX or XXX-XXX-XX)" }
            },
            required = new[] { "ParcelNumber" },
            additionalProperties = false
        };
        return JsonSerializer.Serialize(schema);
    }

    private static string BuildTreatmentBmpSchemaJson()
    {
        var required = new[]
        {
            "TreatmentBMPName","TreatmentBMPType","LocationPointAsWellKnownText","Jurisdiction","Notes","SystemOfRecordID","OwnerOrganization","TreatmentBMPLifespanType","TrashCaptureStatusType","SizingBasisType"
        };
        var schema = new
        {
            type = "object",
            description = "Treatment BMP schema (ExtractedValue objects). Nullable fields may be null.",
            properties = new Dictionary<string, object>
            {
                ["TreatmentBMPName"] = Prop("BMP name."),
                ["TreatmentBMPType"] = Prop("BMP type/classification."),
                ["LocationPointAsWellKnownText"] = Prop("Location WKT point."),
                ["Jurisdiction"] = Prop("Responsible jurisdiction."),
                ["Notes"] = Prop("Notes/comments."),
                ["SystemOfRecordID"] = Prop("External identifier."),
                ["YearBuilt"] = PropNullable("Year built."),
                ["OwnerOrganization"] = Prop("Owning organization."),
                ["TreatmentBMPLifespanType"] = Prop("Lifespan category."),
                ["TreatmentBMPLifespanEndDate"] = PropNullable("Lifespan end date."),
                ["RequiredFieldVisitsPerYear"] = PropNullable("Routine visits/year."),
                ["RequiredPostStormFieldVisitsPerYear"] = PropNullable("Post-storm visits/year."),
                ["TrashCaptureStatusType"] = Prop("Trash capture status."),
                ["SizingBasisType"] = Prop("Sizing basis."),
                ["TrashCaptureEffectiveness"] = PropNullable("Trash capture effectiveness.")
            },
            required,
            additionalProperties = false
        };
        return JsonSerializer.Serialize(schema);

        object Prop(string description) => new { type = "object", description };
        object PropNullable(string description) => new { type = "object", description, nullable = true };
    }

    private static string BuildSourceControlBmpSchemaJson()
    {
        var required = new[] { "SourceControlBMPAttribute", "SourceControlBMPNote" }; // IsPresent nullable
        var schema = new
        {
            type = "object",
            description = "Source Control BMP schema (ExtractedValue objects).",
            properties = new Dictionary<string, object>
            {
                ["SourceControlBMPAttribute"] = Prop("Source control attribute name."),
                ["IsPresent"] = PropNullable("Indicates presence (Yes/No)."),
                ["SourceControlBMPNote"] = Prop("Attribute notes.")
            },
            required,
            additionalProperties = false
        };
        return JsonSerializer.Serialize(schema);

        object Prop(string description) => new { type = "object", description };
        object PropNullable(string description) => new { type = "object", description, nullable = true };
    }

    private async Task<string> EnsureVectorStoreWithFileAsync(int documentId, WaterQualityManagementPlanDocumentDto docDto)
    {
        var existingVectorStoreId = await WaterQualityManagementPlanDocumentVectorStores.GetByWaterQualityManagementPlanDocumentIDAsDtoAsync(DbContext, documentId);
        if (!string.IsNullOrWhiteSpace(existingVectorStoreId)) return existingVectorStoreId;
        var vectorStoreClient = openAIClient.GetVectorStoreClient();
        var fileClient = openAIClient.GetOpenAIFileClient();
        var fileStream = await azureBlobStorageService.DownloadBlobFromBlobStorageAsStream(docDto.FileResource.FileResourceGUID.ToString());
        var fileUploadResult = await fileClient.UploadFileAsync(fileStream.Content, docDto.FileResource.OriginalFilename, VectorStoreFileUploadPurpose);
        if (fileUploadResult?.Value == null) throw new Exception($"OpenAI file upload failed for documentId={documentId}");
        var options = new OpenAI.VectorStores.VectorStoreCreationOptions { Name = $"WQMP_{documentId}" };
        options.FileIds.Add(fileUploadResult.Value.Id);
        var vectorStoreCreateResult = await vectorStoreClient.CreateVectorStoreAsync(options);
        if (vectorStoreCreateResult?.Value == null) throw new Exception($"OpenAI vector store creation failed for documentId={documentId}");
        var vectorStoreId = vectorStoreCreateResult.Value.Id;
        await WaterQualityManagementPlanDocumentVectorStores.UpsertAsync(DbContext, documentId, vectorStoreId);
        return vectorStoreId;
    }

    private async Task<string> BuildSchemaAndDomainContext()
    {
        var domainTables = new
        {
            Jurisdictions = await DbContext.StormwaterJurisdictions.Include(x => x.Organization).Select(x => x.Organization.OrganizationName).AsNoTracking().ToListAsync(),
            TreatmentBMPTypes = await DbContext.TreatmentBMPTypes.Select(x => x.TreatmentBMPTypeName).AsNoTracking().ToListAsync(),
            HydrologicSubareas = await DbContext.HydrologicSubareas.Select(x => x.HydrologicSubareaName).AsNoTracking().ToListAsync(),
            WaterQualityManagementPlanLandUse = WaterQualityManagementPlanLandUse.All.Select(x => x.WaterQualityManagementPlanLandUseDisplayName),
            WaterQualityManagementPlanPriority = WaterQualityManagementPlanPriority.All.Select(x => x.WaterQualityManagementPlanPriorityDisplayName),
            WaterQualityManagementPlanStatus = WaterQualityManagementPlanStatus.All.Select(x => x.WaterQualityManagementPlanStatusDisplayName),
            WaterQualityManagementPlanDevelopmentType = WaterQualityManagementPlanDevelopmentType.All.Select(x => x.WaterQualityManagementPlanDevelopmentTypeDisplayName),
            WaterQualityManagementPlanPermitTerm = WaterQualityManagementPlanPermitTerm.All.Select(x => x.WaterQualityManagementPlanPermitTermDisplayName),
            WaterQualityManagementPlanModelingApproach = WaterQualityManagementPlanModelingApproach.All.Select(x => x.WaterQualityManagementPlanModelingApproachDisplayName),
            TrashCaptureStatusType = TrashCaptureStatusType.All.Select(x => x.TrashCaptureStatusTypeDisplayName),
            TreatmentBMPLifespanType = TreatmentBMPLifespanType.All.Select(x => x.TreatmentBMPLifespanTypeDisplayName),
            SizingBasisType = SizingBasisType.All.Select(x => x.SizingBasisTypeDisplayName),
            DryWeatherFlowOverride = DryWeatherFlowOverride.All.Select(x => x.DryWeatherFlowOverrideDisplayName),
            SourceControlBMPAttributes = await DbContext.SourceControlBMPAttributes.AsNoTracking().Select(x => x.SourceControlBMPAttributeName).ToListAsync(),
        };
        var domainTablesJson = JsonSerializer.Serialize(domainTables);
        return $"SCHEMA_VERSION: {SchemaVersion}\nDOMAIN TABLES: {domainTablesJson}\n";
    }

    [Experimental("OPENAI001")]
    private OpenAIResponseClient CreateResponseClient() => new("gpt-4.1", apiKey: appConfiguration.Value.OpenAIApiKey);

    [Experimental("OPENAI001")]
    private static ResponseTool CreateFileSearchTool(string fileId) => ResponseTool.CreateFileSearchTool([fileId]);

    [Experimental("OPENAI001")]
    private static string ExtractMessageText(IEnumerable<ResponseItem> items)
    {
        var sb = new System.Text.StringBuilder();
        foreach (var outputItem in items)
        {
            if (outputItem is MessageResponseItem m)
            {
                var text = m.Content?.FirstOrDefault()?.Text;
                if (!string.IsNullOrEmpty(text)) sb.Append(Regex.Replace(text, "【.*?】", string.Empty));
            }
        }
        return sb.ToString();
    }

    private static bool IsValidJson(string candidate)
    {
        if (string.IsNullOrWhiteSpace(candidate)) return false;
        try { using var _ = JsonDocument.Parse(candidate); return true; } catch { return false; }
    }

    private static bool MatchesExtractionSchema(string json, bool expectArray)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (expectArray)
            {
                if (root.ValueKind != JsonValueKind.Array) return false;
                foreach (var item in root.EnumerateArray()) if (item.ValueKind != JsonValueKind.Object || !AllChildPropertiesMatchTripleSchema(item)) return false;
            }
            else
            {
                if (root.ValueKind != JsonValueKind.Object || !AllChildPropertiesMatchTripleSchema(root)) return false;
            }
            return true;
        }
        catch { return false; }
    }

    private static bool AllChildPropertiesMatchTripleSchema(JsonElement obj)
    {
        foreach (var prop in obj.EnumerateObject())
        {
            var v = prop.Value;
            if (v.ValueKind == JsonValueKind.Null) continue;
            if (v.ValueKind != JsonValueKind.Object) return false;
            if (!(v.TryGetProperty("Value", out _) && v.TryGetProperty("ExtractionEvidence", out _) && v.TryGetProperty("DocumentSource", out _))) return false;
        }
        return true;
    }

    private static bool MatchesConsolidatedSchema(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            if (root.ValueKind != JsonValueKind.Object) return false;
            if (!root.TryGetProperty("WQMP", out var wqmp) || !root.TryGetProperty("Parcels", out var parcels) || !root.TryGetProperty("TreatmentBMPs", out var tbmps) || !root.TryGetProperty("SourceControlBMPs", out var scbmps)) return false;
            if (wqmp.ValueKind != JsonValueKind.Object || !AllChildPropertiesMatchTripleSchema(wqmp)) return false;
            foreach (var arr in new[] { parcels, tbmps, scbmps })
            {
                if (arr.ValueKind != JsonValueKind.Array) return false;
                foreach (var item in arr.EnumerateArray()) if (item.ValueKind != JsonValueKind.Object || !AllChildPropertiesMatchTripleSchema(item)) return false;
            }
            return true;
        }
        catch { return false; }
    }

    [HttpPost("water-quality-management-plan-documents/{waterQualityManagementPlanDocumentID}/ask")]
    [AdminFeature]
    [Experimental("OPENAI001")]
    public async Task Ask([FromRoute] int waterQualityManagementPlanDocumentID, [FromBody] ChatRequestDto chatRequestDto)
    {
        var docDto = await WaterQualityManagementPlanDocuments.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
        if (docDto == null) { Response.StatusCode = 404; return; }
        var vectorStoreId = await EnsureVectorStoreWithFileAsync(waterQualityManagementPlanDocumentID, docDto);
        Response.Headers.Append("Content-Type", "text/event-stream");
        Response.Headers.Append("X-Accel-Buffering", "no");
        var domainContext = await BuildSchemaAndDomainContext();
        var client = CreateResponseClient();
        var tool = CreateFileSearchTool(vectorStoreId);
        var userPrompt = domainContext + "\n\n" + string.Join("\n\n", chatRequestDto.Messages.Select(m => m.Content));
        var response = await client.CreateResponseAsync(userPrompt, new ResponseCreationOptions { Tools = { tool } });
        var outputText = ExtractMessageText(response.Value.OutputItems).Replace("\n", "<br>").Replace("\r", "");
        if (!string.IsNullOrWhiteSpace(outputText)) await Response.WriteAsync($"data: {outputText}\n\n");
        await Response.WriteAsync("data: ---MessageCompleted---\n\n");
    }

    [HttpPost("clean-up")]
    [AdminFeature]
    [Experimental("OPENAI001")]
    public async Task PostChatCompletions()
    {
        Response.Headers.Append("Content-Type", "text/event-stream");
        Response.Headers.Append("X-Accel-Buffering", "no");
        var fileClient = openAIClient.GetOpenAIFileClient();
        var vectorStoreClient = openAIClient.GetVectorStoreClient();
        var assistantClient = openAIClient.GetAssistantClient();
        var assistants = assistantClient.GetAssistantsAsync();
        await Response.WriteAsync("data: ---STARTING CLEANUP---\n\n");
        await foreach (var assistant in assistants)
        {
            await Response.WriteAsync($"data: DELETING ASSISTANT {assistant.Id}\n\n");
            await assistantClient.DeleteAssistantAsync(assistant.Id);
        }
        var files = await fileClient.GetFilesAsync();
        foreach (var file in files.Value)
        {
            await Response.WriteAsync($"data: DELETING FILE {file.Id}\n\n");
            await fileClient.DeleteFileAsync(file.Id);
        }
        var vectorStores = vectorStoreClient.GetVectorStoresAsync();
        await foreach (var store in vectorStores)
        {
            await Response.WriteAsync($"data: DELETING VECTORSTORE {store.Id}\n\n");
            await vectorStoreClient.DeleteVectorStoreAsync(store.Id);
        }
        await Response.WriteAsync("data: ---DONE---\n\n");
    }

    [HttpGet("vector-stores")]
    [AdminFeature]
    [Experimental("OPENAI001")]
    public async Task<ActionResult<List<object>>> GetVectorStores()
    {
        var client = openAIClient.GetVectorStoreClient();
        var vectorStores = new List<object>();
        await foreach (var store in client.GetVectorStoresAsync())
            vectorStores.Add(new { store.Id, store.Name, store.FileCounts, store.UsageBytes, store.CreatedAt, store.Status });
        return Ok(vectorStores);
    }

#pragma warning restore OPENAI001
}
