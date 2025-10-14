using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
    private const string ExtractorInstructions =
        "You are part of an automated workflow and should return only JSON in your response. Do not include any narrative and do not provide any follow-up suggestions. " +
        "You are helping a stormwater technician extract data from a Water Quality Management Plan (WQMP). You will populate an existing JSON schema, and you must match this schema exactly. " +
        "The WQMPs are for Orange County California, and used by the MS4 permitees including Orange County Public Works and cities. " +
        "In the first step, you will extract the basic attributes of the WQMP. In follow-up prompts you will extract nested child records. " +
        "Each attribute you extract will be placed in the \"Value\" property of the named attribute. The \"ExtractionEvidence\" will include a snippet from the WQMP PDF that shows the sentence in the WQMP where the data came from, " +
        "plus the sentence before and after. If the attribute was extracted from a table or label just include relevant text nearby. The \"DocumentSource\" should include the Page # in the document where the attribute value was found. " +
        "Here is the sub-schema for each extracted attribute: { \"Value\": \"\", \"ExtractionEvidence\": \"\", \"DocumentSource\": \"\" } " +
        "A second LLM will be reviewing this work for accuracy, so please do not hallucinate data. " +
        "If an attribute is not found simply put null in Value, ExtractionEvidence, and DocumentSource.\n";

    private const string ValidatorInstructions =
        "You are part of an automated workflow and should return only JSON in your response. " +
        "Do not include any narrative and do not provide any follow-up suggestions. " +
        "You are helping a stormwater technician extract data from a Water Quality Management Plan (WQMP). " +
        "You are the second LLM to see this data. The first LLM extracted the data as best it could. Your job is to double check the work. " +
        "You will score each attribute in the JSON schema from 0 to 100. " +
        "Zero represents a complete hallucination that does not reflect any reasonable information in the source document. " +
        "100 represents a complete match with data you can verify. \n\n" +
        "Add a validation score to each extracted property, along with a short validation evidence snippet narrative explaining your score: " +
        "\n\n { \"Value\": \"\", \"ExtractionEvidence\": \"\", \"DocumentSource\": \"\", \"ValidationScore\": \"\", \"ValidationEvidence\": \"\" }\n\n" +
        "I will provide the WQMP PDF and the previously extracted JSON schema.";

    private const string AskInstructions = "Ok thanks! Now you are a WQMP ChatBot available for the end user to ask narrative questions about. You are no longer constrained to JSON output, but use the context you gained while operating in that mode to be helpful to the stormwater technician. They will ask questions about the source data, how you extracted, and other things they want to know about the WQMP.\n";

    [HttpPost("clean-up")]
    [AllowAnonymous]
    public async Task PostChatCompletions()
    {
        Response.Headers.Append("Content-Type", "text/event-stream");
        Response.Headers.Append("X-Accel-Buffering", "no"); //NOTE: this allows for event-stream to work through our Application Gateway, which buffers responses by default.

        var fileClient = openAIClient.GetOpenAIFileClient();
#pragma warning disable OPENAI001
        var vectorStoreClient = openAIClient.GetVectorStoreClient();
        var assistantClient = openAIClient.GetAssistantClient();
        var assistants = assistantClient.GetAssistantsAsync();
#pragma warning restore OPENAI001
        await Response.WriteAsync($"data: ---STARTING CLEANUP---\n\n");
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
        await foreach (var vectorStore in vectorStores)
        {
            await Response.WriteAsync($"data: DELETING VECTORSTORE {vectorStore.Id}\n\n");
            await vectorStoreClient.DeleteVectorStoreAsync(vectorStore.Id);
        }
        await Response.WriteAsync($"data: ---DONE---\n\n");
    }

    [HttpPost("water-quality-management-plan-documents/{waterQualityManagementPlanDocumentID}/extract-all")]
    [AdminFeature]
    [Experimental("OPENAI001")]
    public async Task<ActionResult<WaterQualityManagementPlanDocumentExtractionResultDto>> ExtractAll([FromRoute] int waterQualityManagementPlanDocumentID)
    {
        // Step 1: Get document DTO
        var docDto = await WaterQualityManagementPlanDocuments.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
        if (docDto == null)
            return NotFound();

        // Step 2: Ensure vector store exists and file is uploaded
        var vectorStoreId = await EnsureVectorStoreWithFileAsync(waterQualityManagementPlanDocumentID, docDto);

        // Step 3: Prepare schema and domain context
        var schemaAndDomainContext = await BuildSchemaAndDomainContext();

        // Step 4: Prepare extraction prompts
        var extractionPrompts = BuildExtractionPrompts(schemaAndDomainContext);

        // Step 5: Use OpenAI Chat API for each prompt
        var responseClient = CreateResponseClient();
        var fileSearchTool = CreateFileSearchTool(vectorStoreId);

        var aggregatedResults = new List<string>();
        foreach (var prompt in extractionPrompts)
        {
            var fullPrompt = aggregatedResults.Count == 0
                ? schemaAndDomainContext + "\n\n" + prompt
                : schemaAndDomainContext + "\n\n" + string.Join("\n\n", aggregatedResults) + "\n\n" + prompt;

            var response = await responseClient.CreateResponseAsync(
                userInputText: fullPrompt,
                new ResponseCreationOptions()
                {
                    Tools = { fileSearchTool }
                });
            aggregatedResults.Add(ExtractMessageText(response.Value.OutputItems));
        }

        // Step 6: Aggregate results
        var rawResults = string.Join("\n\n", aggregatedResults.Take(aggregatedResults.Count - 1));
        var extractionResult = new WaterQualityManagementPlanDocumentExtractionResultDto
        {
            FinalOutput = aggregatedResults.Last(),
            RawResults = rawResults,
            ExtractedAt = DateTime.UtcNow
        };
        return Ok(extractionResult);
    }

    // Helper: Ensure vector store exists and file is uploaded
    private async Task<string> EnsureVectorStoreWithFileAsync(int documentId, WaterQualityManagementPlanDocumentDto docDto)
    {
        var vectorStoreId = await WaterQualityManagementPlanDocumentVectorStores.GetByWaterQualityManagementPlanDocumentIDAsDtoAsync(DbContext, documentId);
        if (!string.IsNullOrWhiteSpace(vectorStoreId))
            return vectorStoreId;

        #pragma warning disable OPENAI001
        var vectorStoreClient = openAIClient.GetVectorStoreClient();
        var fileClient = openAIClient.GetOpenAIFileClient();
        logger.LogInformation($"Starting file download from blob storage for documentId={documentId}, fileResourceGUID={docDto.FileResource.FileResourceGUID}");
        var fileStream = await azureBlobStorageService.DownloadBlobFromBlobStorageAsStream(docDto.FileResource.FileResourceGUID.ToString());
        logger.LogInformation($"File download complete. Starting upload to OpenAI for filename={docDto.FileResource.OriginalFilename}");
        var fileUploadResult = await fileClient.UploadFileAsync(fileStream.Content, docDto.FileResource.OriginalFilename, FileUploadPurpose.UserData);
        if (fileUploadResult == null || fileUploadResult.Value == null)
        {
            logger.LogError($"OpenAI file upload failed: result is null for documentId={documentId}");
            throw new Exception($"OpenAI file upload failed for documentId={documentId}");
        }
        logger.LogInformation($"OpenAI file upload result: {JsonSerializer.Serialize(fileUploadResult.Value)}");
        var openAiFileId = fileUploadResult.Value.Id;
        logger.LogInformation($"File uploaded to OpenAI. FileId={openAiFileId}. Creating vector store.");

        var options = new OpenAI.VectorStores.VectorStoreCreationOptions
        {
            Name = $"WQMP_{documentId}"
        };
        options.FileIds.Add(openAiFileId);
        var vectorStoreCreateResult = await vectorStoreClient.CreateVectorStoreAsync(options);
        if (vectorStoreCreateResult == null || vectorStoreCreateResult.Value == null)
        {
            logger.LogError($"OpenAI vector store creation failed: result is null for documentId={documentId}");
            throw new Exception($"OpenAI vector store creation failed for documentId={documentId}");
        }
        logger.LogInformation($"OpenAI vector store creation result: {JsonSerializer.Serialize(vectorStoreCreateResult.Value)}");
        vectorStoreId = vectorStoreCreateResult.Value.Id;
        logger.LogInformation($"Vector store created. VectorStoreId={vectorStoreId}");
        await WaterQualityManagementPlanDocumentVectorStores.UpsertAsync(DbContext, documentId, vectorStoreId);
        return vectorStoreId;
    }

    // Helper: Build extraction prompts
    private List<string> BuildExtractionPrompts(string schemaAndDomainContext)
    {
        var extractionEvidenceInstructions = "Use only the provided WQMP PDF document for extraction. Each attribute you extract will be placed in the 'Value' property of the named attribute. The 'ExtractionEvidence' will include a snippet from the WQMP PDF that shows the sentence in the WQMP where the data came from, plus the sentence before and after. If the attribute was extracted from a table or label just include relevant text nearby. The 'DocumentSource' should include the Page # in the document where the attribute value was found. Here is the sub-schema for each extracted attribute: { 'Value': '', 'ExtractionEvidence': '', 'DocumentSource': '' }\n\nA second LLM will be reviewing this work for accuracy, so please do not hallucinate data. If an attribute is not found simply put null in Value, ExtractionEvidence, and DocumentSource.\n";

        var treatmentBMPSchemaStructure = JsonSerializer.Serialize(new TreatmentBMPExtractDto());
        var treatmentBMPPrompt = $@"Extract all Treatment BMPs for this WQMP using only the provided WQMP PDF document. {extractionEvidenceInstructions} Use the following JSON schema for each BMP: {treatmentBMPSchemaStructure} Return a JSON array. If none are found, return an empty array. Do not include any narrative or follow-up questions.";

        var parcelSchemaStructure = JsonSerializer.Serialize(new WaterQualityManagementPlanParcelExtractDto());
        var parcelPrompt = $@"Extract all Parcels for this WQMP using only the provided WQMP PDF document. {extractionEvidenceInstructions} Use the following JSON schema for each Parcel: {parcelSchemaStructure} Return a JSON array. If none are found, return an empty array. Do not include any narrative or follow-up questions.";

        var sourceControlBMPSchemaStructure = JsonSerializer.Serialize(new SourceControlBMPExtractDto());
        var sourceControlBMPPrompt = $@"Extract all Source Control BMPs for this WQMP using only the provided WQMP PDF document. {extractionEvidenceInstructions} Use the following JSON schema for each Source Control BMP: {sourceControlBMPSchemaStructure} Return a JSON array. If none are found, return an empty array. Do not include any narrative or follow-up questions.";

        var consolidationPrompt = @"Now consolidate all previously extracted data from the provided WQMP PDF document into a single JSON object with the following structure: { 'WQMP': {}, 'Parcels': [], 'TreatmentBMPs': [], 'SourceControlBMPs': [] } Return only valid JSON, and the attribute values should follow this schema { 'Value': '', 'ExtractionEvidence': '', 'DocumentSource': '' }. Do not include any narrative, explanation, or follow-up questions and DO NOT Hallucinate.";

        return new List<string>
        {
            ExtractorInstructions,
            parcelPrompt,
            treatmentBMPPrompt,
            sourceControlBMPPrompt,
            consolidationPrompt
        };
    }


    [HttpPost("water-quality-management-plan-documents/{waterQualityManagementPlanDocumentID}/ask")]
    [AdminFeature]
    [Experimental("OPENAI001")]
    public async Task Ask([FromRoute] int waterQualityManagementPlanDocumentID, [FromBody] ChatRequestDto chatRequestDto)
    {
        var docDto = await WaterQualityManagementPlanDocuments.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
        if (docDto == null)
        {
            Response.StatusCode = 404;
            return;
        }

        // Get the OpenAI vector store ID from the new table
        var vectorStoreId = await WaterQualityManagementPlanDocumentVectorStores.GetByWaterQualityManagementPlanDocumentIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
        if (string.IsNullOrWhiteSpace(vectorStoreId))
        {
            // Create a new vector store via OpenAI API
            var vectorStoreClient = openAIClient.GetVectorStoreClient();
            var fileClient = openAIClient.GetOpenAIFileClient();
            // Upload the WQMP PDF file to OpenAI
            var fileStream = await azureBlobStorageService.DownloadBlobFromBlobStorageAsStream(docDto.FileResource.FileResourceGUID.ToString());
            var fileUploadResult = await fileClient.UploadFileAsync(fileStream.Content, docDto.FileResource.OriginalFilename, FileUploadPurpose.Assistants);
            var openAiFileId = fileUploadResult.Value.Id;
            // Create the vector store and attach the file
            var options = new OpenAI.VectorStores.VectorStoreCreationOptions
            {
                Name = $"WQMP_{waterQualityManagementPlanDocumentID}"
            };
            options.FileIds.Add(openAiFileId);
            var vectorStoreCreateResult = await vectorStoreClient.CreateVectorStoreAsync(options);
            vectorStoreId = vectorStoreCreateResult.Value.Id;
            // Save the new vector store ID in the database
            await WaterQualityManagementPlanDocumentVectorStores.UpsertAsync(DbContext, waterQualityManagementPlanDocumentID, vectorStoreId);
        }

        Response.Headers.Append("Content-Type", "text/event-stream");
        Response.Headers.Append("X-Accel-Buffering", "no");

        // Prepare schema and domain context (unchanged)
        var schemaAndDomainContext = await BuildSchemaAndDomainContext();

        var responseClient = CreateResponseClient();
        var fileSearchTool = CreateFileSearchTool(vectorStoreId);

        // Combine all user messages into a single prompt
        var userPrompt = schemaAndDomainContext + "\n\n" + string.Join("\n\n", chatRequestDto.Messages.Select(m => m.Content));

        var response = await responseClient.CreateResponseAsync(
            userInputText: userPrompt,
            new ResponseCreationOptions()
            {
                Tools = { fileSearchTool }
            });

        var outputText = ExtractMessageText(response.Value.OutputItems).Replace("\n", "<br>").Replace("\r", "");
        if (!string.IsNullOrWhiteSpace(outputText))
        {
            await Response.WriteAsync($"data: {outputText}\n\n");
        }
        // Signal end of stream
        await Response.WriteAsync("data: ---MessageCompleted---\n\n");
    }

    // DRY Helper: Build schema and domain context string
    private async Task<string> BuildSchemaAndDomainContext()
    {
        var schemaStructure = JsonSerializer.Serialize(new WaterQualityManagementPlanExtractDto());
        var domainTables = new
        {
            Jurisdictions = await DbContext.StormwaterJurisdictions.Include(x => x.Organization)
                .Select(x => x.Organization.OrganizationName).AsNoTracking().ToListAsync(),
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
        return $"SCHEMA STRUCTURE: {schemaStructure}\n\nDOMAIN TABLES: {domainTablesJson}\n\n";
    }

    // DRY Helper: Create OpenAIResponseClient
    [Experimental("OPENAI001")]
    private OpenAIResponseClient CreateResponseClient() =>
        new OpenAIResponseClient(model: "gpt-4o-mini", apiKey: appConfiguration.Value.OpenAIApiKey);

    // DRY Helper: Create file search tool
    [Experimental("OPENAI001")]
    private static ResponseTool CreateFileSearchTool(string fileId) =>
        ResponseTool.CreateFileSearchTool(vectorStoreIds: [fileId]);

    // DRY Helper: Extract cleaned message text from response items
    [Experimental("OPENAI001")]
    private static string ExtractMessageText(IEnumerable<ResponseItem> items)
    {
        var resultBuilder = new System.Text.StringBuilder();
        foreach (var outputItem in items)
        {
            if (outputItem is MessageResponseItem message)
            {
                var text = message.Content?.FirstOrDefault()?.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    var cleanedText = Regex.Replace(text, "【.*?】", string.Empty);
                    resultBuilder.Append(cleanedText);
                }
            }
        }
        return resultBuilder.ToString();
    }

    [HttpGet("vector-stores")]
    public async Task<ActionResult<List<object>>> GetVectorStores()
    {
        #pragma warning disable OPENAI001
        var vectorStoreClient = openAIClient.GetVectorStoreClient();
        var vectorStores = new List<object>();
        await foreach (var store in vectorStoreClient.GetVectorStoresAsync())
        {
            vectorStores.Add(new
            {
                Id = store.Id,
                Name = store.Name,
                FileCounts = store.FileCounts,
                UsageBytes = store.UsageBytes,
                CreatedAt = store.CreatedAt,
                Status = store.Status
            });
        }
        #pragma warning restore OPENAI001
        return Ok(vectorStores);
    }
}
