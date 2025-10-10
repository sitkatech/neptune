using System.ClientModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
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
using OpenAI.Assistants;
using OpenAI.Files;

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


    [HttpPost("water-quality-management-plan-documents/{waterQualityManagementPlanDocumentID}/ask")]
    [AdminFeature]
    public async Task Ask([FromRoute] int waterQualityManagementPlanDocumentID, [FromBody] ChatRequestDto chatRequestDto)
    {
        var docDto = await WaterQualityManagementPlanDocuments.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
        if (docDto == null)
        {
            Response.StatusCode = 404;
            return;
        }

        Response.Headers.Append("Content-Type", "text/event-stream");
        Response.Headers.Append("X-Accel-Buffering", "no");

        var fileClient = openAIClient.GetOpenAIFileClient();
#pragma warning disable OPENAI001
        var assistantClient = openAIClient.GetAssistantClient();

        var assistantID = await WaterQualityManagementPlanDocumentAssistants.GetByWaterQualityManagementPlanDocumentIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
        // Check for existing assistant for this project and document

        var needToCreateAssistant = await TryValidateAssistant(assistantID, assistantClient);

        if (needToCreateAssistant)
        {
            var blobDownloadResult = await azureBlobStorageService.DownloadBlobFromBlobStorageAsStream(docDto.FileResource.FileResourceGUID.ToString());

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
                //HydromodificationAppliesType = HydromodificationAppliesType.All.Select(x => x.HydromodificationAppliesTypeDisplayName),
                WaterQualityManagementPlanModelingApproach = WaterQualityManagementPlanModelingApproach.All.Select(x => x.WaterQualityManagementPlanModelingApproachDisplayName),
                TrashCaptureStatusType = TrashCaptureStatusType.All.Select(x => x.TrashCaptureStatusTypeDisplayName),
                TreatmentBMPLifespanType = TreatmentBMPLifespanType.All.Select(x => x.TreatmentBMPLifespanTypeDisplayName),
                SizingBasisType = SizingBasisType.All.Select(x => x.SizingBasisTypeDisplayName),
                DryWeatherFlowOverride = DryWeatherFlowOverride.All.Select(x => x.DryWeatherFlowOverrideDisplayName),
                SourceControlBMPAttributes = await DbContext.SourceControlBMPAttributes.AsNoTracking().Select(x => x.SourceControlBMPAttributeName).ToListAsync(),
            };
            var domainTablesJson = JsonSerializer.Serialize(domainTables);
            var schemaAndDomainContext =
                $"SCHEMA STRUCTURE: {schemaStructure}\n\nDOMAIN TABLES: {domainTablesJson}\n\n";

            var assistant = await CreateAssistantClient(fileClient, blobDownloadResult.Content, assistantClient, docDto.FileResource.OriginalFilename, schemaAndDomainContext);
            assistantID = assistant.Value.Id;

            await WaterQualityManagementPlanDocumentAssistants.UpsertAsync(DbContext, waterQualityManagementPlanDocumentID, assistantID);
        }

        var threadOptions = new ThreadCreationOptions();
        var messages = chatRequestDto.Messages.Select(message => new ThreadInitializationMessage(
            message.Role == "user" ? MessageRole.User : MessageRole.Assistant,
            [
                message.Content
            ]
        ));

        foreach (var threadInitializationMessage in messages)
        {
            threadOptions.InitialMessages.Add(threadInitializationMessage);
        }

        var streamingResponses = assistantClient.CreateThreadAndRunStreamingAsync(assistantID, threadOptions);

        await foreach (var streamingUpdate in streamingResponses)
        {
            if (streamingUpdate is MessageContentUpdate contentUpdate)
            {
                string output = null;
                if (!string.IsNullOrEmpty(contentUpdate.Text))
                {
                    // Remove all content between 【 and 】 including the symbols
                    var cleanedText = Regex.Replace(contentUpdate.Text, "【.*?】", string.Empty);
                    // Replace newlines with <br> for HTML rendering in the browser
                    output = cleanedText.Replace("\n", "<br>").Replace("\r", "");
                }
                if (contentUpdate.TextAnnotation != null)
                {
                    output += $" [Reference: {docDto.FileResource.OriginalFilename}]";
                }
                if (!string.IsNullOrWhiteSpace(output))
                {
                    await Response.WriteAsync($"data: {output}\n\n");
                }
            }
            if (streamingUpdate.UpdateKind == StreamingUpdateReason.MessageCompleted)
            {
                await Response.WriteAsync($"data: ---{streamingUpdate.UpdateKind.ToString()}---\n\n");
            }
        }
#pragma warning restore OPENAI001
    }

    [HttpPost("water-quality-management-plan-documents/{waterQualityManagementPlanDocumentID}/extract-data")]
    [AdminFeature]
    public async Task<IActionResult> ExtractData([FromRoute] int waterQualityManagementPlanDocumentID)
    {
        var schemaStructure = JsonSerializer.Serialize(new WaterQualityManagementPlanExtractDto());
        var domainTables = new
        {
            Jurisdictions = await DbContext.StormwaterJurisdictions.Include(x => x.Organization)
                .Select(x => x.Organization.OrganizationName).ToListAsync(),
            TreatmentBMPTypes = await DbContext.TreatmentBMPTypes.Select(x => x.TreatmentBMPTypeName).ToListAsync(),
            HydrologicSubareas = await DbContext.HydrologicSubareas.Select(x => x.HydrologicSubareaName).ToListAsync(),
            WaterQualityManagementPlanLandUse = WaterQualityManagementPlanLandUse.All.Select(x => x.WaterQualityManagementPlanLandUseDisplayName),
            WaterQualityManagementPlanPriority = WaterQualityManagementPlanPriority.All.Select(x => x.WaterQualityManagementPlanPriorityDisplayName),
            WaterQualityManagementPlanStatus = WaterQualityManagementPlanStatus.All.Select(x => x.WaterQualityManagementPlanStatusDisplayName),
            WaterQualityManagementPlanDevelopmentType = WaterQualityManagementPlanDevelopmentType.All.Select(x => x.WaterQualityManagementPlanDevelopmentTypeDisplayName),
            WaterQualityManagementPlanPermitTerm = WaterQualityManagementPlanPermitTerm.All.Select(x => x.WaterQualityManagementPlanPermitTermDisplayName),
            HydromodificationAppliesType = HydromodificationAppliesType.All.Select(x => x.HydromodificationAppliesTypeDisplayName),
            WaterQualityManagementPlanModelingApproach = WaterQualityManagementPlanModelingApproach.All.Select(x => x.WaterQualityManagementPlanModelingApproachDisplayName),
            TrashCaptureStatusType = TrashCaptureStatusType.All.Select(x => x.TrashCaptureStatusTypeDisplayName),
            TreatmentBMPLifespanType = TreatmentBMPLifespanType.All.Select(x => x.TreatmentBMPLifespanTypeDisplayName),
            SizingBasisType = SizingBasisType.All.Select(x => x.SizingBasisTypeDisplayName),
            DryWeatherFlowOverride = DryWeatherFlowOverride.All.Select(x => x.DryWeatherFlowOverrideDisplayName),
            SourceControlBMPAttributes = await DbContext.SourceControlBMPAttributes.Select(x => x.SourceControlBMPAttributeName).ToListAsync(),
        };
        var domainTablesJson = JsonSerializer.Serialize(domainTables);

        var extractionPrompt =
            $"You are tasked with extracting data for a WaterQualityManagementPlan from a PDF document. Use the following schema structure for your output.\nSCHEMA STRUCTURE: {schemaStructure}\nDOMAIN TABLES: {domainTablesJson}\nInstructions: Only use values for each field that appear in the corresponding domain table. For each field, provide the extracted value, a rationale, and a snippet from the document showing why you chose that value. If the document does not contain a value for a field, leave it blank or null. Do not invent data. Only use information found in the document. Return a single JSON object matching the schema.";

        var docDto = await WaterQualityManagementPlanDocuments.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
        if (docDto == null)
        {
            return NotFound();
        }

        // Removed event-stream headers since we are not streaming the response

        var fileClient = openAIClient.GetOpenAIFileClient();
#pragma warning disable OPENAI001
        var assistantClient = openAIClient.GetAssistantClient();

        var assistantID = await WaterQualityManagementPlanDocumentAssistants.GetByWaterQualityManagementPlanDocumentIDAsDtoAsync(DbContext, waterQualityManagementPlanDocumentID);
        // Check for existing assistant for this project and document

        var needToCreateAssistant = await TryValidateAssistant(assistantID, assistantClient);

        if (needToCreateAssistant)
        {
            var blobDownloadResult = await azureBlobStorageService.DownloadBlobFromBlobStorageAsStream(docDto.FileResource.FileResourceGUID.ToString());

            var assistant = await CreateAssistantClient(fileClient, blobDownloadResult.Content, assistantClient, docDto.FileResource.OriginalFilename, extractionPrompt);
            assistantID = assistant.Value.Id;

            await WaterQualityManagementPlanDocumentAssistants.UpsertAsync(DbContext, waterQualityManagementPlanDocumentID, assistantID);
        }

        var threadOptions = new ThreadCreationOptions();
        threadOptions.InitialMessages.Add(new ThreadInitializationMessage(
            MessageRole.User,
            [
                extractionPrompt
            ]
        ));

        var streamingResponses = assistantClient.CreateThreadAndRunStreamingAsync(assistantID, threadOptions);
        var resultBuilder = new System.Text.StringBuilder();
        await foreach (var streamingUpdate in streamingResponses)
        {
            if (streamingUpdate is MessageContentUpdate contentUpdate)
            {
                if (!string.IsNullOrEmpty(contentUpdate.Text))
                {
                    // Remove all content between 【 and 】 including the symbols
                    var cleanedText = Regex.Replace(contentUpdate.Text, "【.*?】", string.Empty);
                    resultBuilder.Append(cleanedText.Replace("\n", "<br>").Replace("\r", ""));
                }
                if (contentUpdate.TextAnnotation != null)
                {
                    resultBuilder.Append($" [Reference: {docDto.FileResource.OriginalFilename}]");
                }
            }
        }
        var finalResult = resultBuilder.ToString();
        if (!string.IsNullOrWhiteSpace(finalResult))
        {
            return Content(finalResult, "text/plain");
        }
        else
        {
            return StatusCode(500, new { error = "No response from AI service." });
        }
#pragma warning restore OPENAI001
    }

    [Experimental("OPENAI001")]
    private static async Task<bool> TryValidateAssistant(string existingAssistantID, AssistantClient assistantClient)
    {
        bool needToCreateAssistant = false;
        if (!string.IsNullOrWhiteSpace(existingAssistantID))
        {
            // Check if the assistant exists in OpenAI
            try
            {
                var openAIAssistant = await assistantClient.GetAssistantAsync(existingAssistantID);
                if (openAIAssistant == null)
                {
                    needToCreateAssistant = true;
                }
            }
            catch
            {
                // If OpenAI throws (e.g. not found), treat as not existing
                needToCreateAssistant = true;
            }
        }
        else
        {
            needToCreateAssistant = true;
        }

        return needToCreateAssistant;
    }

    [Experimental("OPENAI001")]
    private static async Task<ClientResult<Assistant>> CreateAssistantClient(OpenAIFileClient fileClient, Stream blobStream, AssistantClient assistantClient, string filename, string instructions)
    {
        OpenAIFile file = await fileClient.UploadFileAsync(blobStream, filename, FileUploadPurpose.Assistants);
        var assistant = await assistantClient.CreateAssistantAsync("gpt-4.1",
            new AssistantCreationOptions()
            {
                Instructions = instructions,
                Temperature = 0.4f,
                Tools =
                {
                    new FileSearchToolDefinition(),
                },
                ToolResources = new()
                {
                    FileSearch = new()
                    {
                        NewVectorStores =
                        {
                            new VectorStoreCreationHelper([file.Id]),
                        }
                    }
                },
            });
        return assistant;
    }
}
