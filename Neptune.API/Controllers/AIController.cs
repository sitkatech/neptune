using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.API.Services.Attributes;
using Neptune.API.Services.Authorization;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using Neptune.Models.DataTransferObjects.WaterQualityManagementPlan;
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
    private const string AskInstructions = "You will be referred to as ESA Intelligence. Please respond in two parts, Part 1: Respond to the user in a friendly manner, seeing if the summary is satisfactory. Limit this part to one or two sentences."
                                        + " Part 2: The water quality management plan summary based on the file, use their instructions to determine the content of the summary."
                                        + " Please use the contents of the file to produce the results, DO NOT INVENT ANYTHING. Limit the summary to a maximum of 4 paragraphs."
                                        + " Separate the two parts with \"---SUMMARY---\". Like this: "
                                        + " Let me know if you need any updates. ---SUMMARY--- This water quality management plan was conducted by ESA to perform";

    [HttpPost("clean-up")]
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


    [HttpPost("water-quality-management-plans/{waterQualityManagementPlanID}/documents/{waterQualityManagementPlanDocumentID}/ask")]
    [AdminFeature]
    [EntityNotFound(typeof(WaterQualityManagementPlan), "waterQualityManagementPlanID")]
    public async Task Ask([FromRoute] int waterQualityManagementPlanID, [FromRoute] int waterQualityManagementPlanDocumentID, [FromBody] ChatRequestDto messageDto)
    {
        var waterQualityManagementPlanDto = await WaterQualityManagementPlans.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanID);
        var waterQualityManagementPlanContext = $"WQMP CONTEXT: {JsonSerializer.Serialize(waterQualityManagementPlanDto)}\n";
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

            var assistant = await CreateAssistantClient(fileClient, blobDownloadResult.Content, assistantClient, waterQualityManagementPlanContext, docDto.FileResource.OriginalFilename, AskInstructions);
            assistantID = assistant.Value.Id;

            await WaterQualityManagementPlanDocumentAssistants.UpsertAsync(DbContext, waterQualityManagementPlanDocumentID, assistantID);
        }

        var threadOptions = new ThreadCreationOptions();
        var messages = messageDto.Messages.Select(message => new ThreadInitializationMessage(
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

    [HttpPost("water-quality-management-plans/{waterQualityManagementPlanID}/documents/{waterQualityManagementPlanDocumentID}/extract-data")]
    [AdminFeature]
    [EntityNotFound(typeof(WaterQualityManagementPlan), "waterQualityManagementPlanID")]
    public async Task ExtractData([FromRoute] int waterQualityManagementPlanID, [FromRoute] int waterQualityManagementPlanDocumentID)
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

            var assistant = await CreateAssistantClient(fileClient, blobDownloadResult.Content, assistantClient, "", docDto.FileResource.OriginalFilename, extractionPrompt);
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
    private static async Task<ClientResult<Assistant>> CreateAssistantClient(OpenAIFileClient fileClient, Stream blobStream, AssistantClient assistantClient, string wqmpContext, string filename, string instructions)
    {
        OpenAIFile file = await fileClient.UploadFileAsync(blobStream, filename, FileUploadPurpose.Assistants);
        var assistant = await assistantClient.CreateAssistantAsync("gpt-4.1",
            new AssistantCreationOptions()
            {
                Instructions = instructions + wqmpContext,
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
