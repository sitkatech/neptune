using System.ClientModel;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    OpenAIClient openAIClient)
    : SitkaController<AIController>(dbContext, logger, keystoneService, appConfiguration)
{
    private const string Instructions = "You will be referred to as ESA Intelligence. Please respond in two parts, Part 1: Respond to the user in a friendly manner, seeing if the summary is satisfactory. Limit this part to one or two sentences."
                                        + " Part 2: The project summary based on the file, use their instructions to determine the content of the summary."
                                        + " Please use the contents of the file to produce the results, DO NOT INVENT ANYTHING. Limit the summary to a maximum of 4 paragraphs."
                                        + " Separate the two parts with \"---SUMMARY---\". Like this: "
                                        + " Let me know if you need any updates. ---SUMMARY--- This project was conducted by ESA to perform";

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


    [HttpPost("/water-quality-management-plans/{waterQualityManagementPlanID}/documents/{waterQualityManagementPlanDocumentID}/ask")]
    [AdminFeature]
    [EntityNotFound(typeof(WaterQualityManagementPlan), "waterQualityManagementPlanID")]
    public async Task Ask([FromRoute] int waterQualityManagementPlanID, [FromRoute] int waterQualityManagementPlanDocumentID, [FromBody] ChatRequestDto messageDto)
    {
        var waterQualityManagementPlanDto = await WaterQualityManagementPlans.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanID);
        // Serialize projectDto for context
        var waterQualityManagementPlanContext = $"WQMP CONTEXT: {JsonSerializer.Serialize(waterQualityManagementPlanDto)}\n";
        var docDto = WaterQualityManagementPlans.GetByIDAsDtoAsync(DbContext, waterQualityManagementPlanID);
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

        //var assistantID = ProjectDocument.GetAssistantIDByProjectDocumentID(DbContext, waterQualityManagementPlanDocumentID);
        //// Check for existing assistant for this project and document

        //bool needToCreateAssistant = await TryValidateAssistant(assistantID, assistantClient);

        //if (needToCreateAssistant)
        //{
        //    var fileSteam = await egnyteClient.Files.DownloadFileAsStream(docDto.EgnytePath);
        //    Stream blobStream = fileSteam.Data;

        //    ClientResult<Assistant> assistant = await CreateAssistantClient(fileClient, blobStream, assistantClient, waterQualityManagementPlanContext, docDto.FileName);
        //    assistantID = assistant.Value.Id;

        //    await ProjectDocument.UpsertAssistant(DbContext, waterQualityManagementPlanDocumentID, assistantID);
        //}

        //var threadOptions = new ThreadCreationOptions();
        //var messages = messageDto.Messages.Select(message => new ThreadInitializationMessage(
        //    message.Role == "user" ? MessageRole.User : MessageRole.Assistant,
        //    [
        //        message.Content
        //    ]
        //));

        //foreach (ThreadInitializationMessage threadInitializationMessage in messages)
        //{
        //    threadOptions.InitialMessages.Add(threadInitializationMessage);
        //}

        //var streamingResponses = assistantClient.CreateThreadAndRunStreamingAsync(assistantID, threadOptions);

        //await foreach (var streamingUpdate in streamingResponses)
        //{
        //    if (streamingUpdate is MessageContentUpdate contentUpdate)
        //    {
        //        string output = null;
        //        if (!string.IsNullOrEmpty(contentUpdate.Text))
        //        {
        //            // Remove all content between 【 and 】 including the symbols
        //            var cleanedText = Regex.Replace(contentUpdate.Text, "【.*?】", string.Empty);
        //            // Replace newlines with <br> for HTML rendering in the browser
        //            output = cleanedText.Replace("\n", "<br>").Replace("\r", "");
        //        }
        //        if (contentUpdate.TextAnnotation != null)
        //        {
        //            output += $" [Reference: {docDto.FileName}]";
        //        }
        //        if (!string.IsNullOrWhiteSpace(output))
        //        {
        //            await Response.WriteAsync($"data: {output}\n\n");
        //        }
        //    }
        //    if (streamingUpdate.UpdateKind == StreamingUpdateReason.MessageCompleted)
        //    {
        //        await Response.WriteAsync($"data: ---{streamingUpdate.UpdateKind.ToString()}---\n\n");
        //    }
        //}
#pragma warning restore OPENAI001
    }

    //[HttpPost("/ai/rag-ask")]
    //[AdminFeature]
    //public async Task RagAsk([FromBody] ChatRequestDto request)
    //{
    //    Response.Headers.Append("Content-Type", "text/event-stream");
    //    Response.Headers.Append("X-Accel-Buffering", "no");

    //    var input = new DatabricksRagInputDto
    //    {
    //        Messages = request.Messages?.Select(m => new DatabricksRagMessageInputDto
    //        {
    //            Role = m.Role,
    //            Content = m.Content
    //        }).ToList(),
    //        Stream = true,
    //        CustomInputs = new DatabricksRagCustomInputsDto
    //        {
    //            Filters = request.Filters != null ? new Dictionary<string, object>(request.Filters) : new Dictionary<string, object>()
    //        }
    //    };

    //    await foreach (var message in ragService.QueryStreamAsync(input))
    //    {
    //        if (!string.IsNullOrWhiteSpace(message.Content))
    //        {
    //            // Optionally, you can HTML-encode or format the content here
    //            await Response.WriteAsync($"data: {JsonSerializer.Serialize(message)}\n\n");
    //        }
    //        if (message.IsDone)
    //        {
    //            await Response.WriteAsync($"data: {JsonSerializer.Serialize(message)}\n\n");
    //            break;
    //        }
    //    }
    //}

    //[HttpGet("/projects/{projectID}/similarity-search")]
    //[AdminFeature]
    //public async Task<ActionResult<List<ProjectSimilarityResultDto>>> ProjectSimilaritySearch([FromRoute] int projectID, [FromQuery] int? num_results)
    //{
    //    var projectDto = Project.GetByProjectID(DbContext, projectID);
    //    if (projectDto == null)
    //    {
    //        return NotFound($"Project {projectID} not found.");
    //    }

    //    var allProjects = Project.List(DbContext).ToList();
    //    // Coalesce project descriptions
    //    var queryText = projectDto.Description
    //                    ?? projectDto.StandardDescription
    //                    ?? projectDto.OpportunityDescription;

    //    if (string.IsNullOrWhiteSpace(queryText))
    //        return BadRequest("No project description available for similarity search.");

    //    var idToExclude = projectDto.DeltekID;
    //    int numResults = num_results ?? 5;
    //    List<ProjectSimilarityResultDto> result;
    //    try
    //    {
    //        var searchResult = await similaritySearchService.SearchAsync(idToExclude, queryText, numResults);
    //        result = searchResult.Select(row =>
    //        {
    //            var match = allProjects.FirstOrDefault(p => p.DeltekID == row.DeltekId);
    //            return new ProjectSimilarityResultDto
    //            {
    //                DeltekId = row.DeltekId,
    //                ShortName = row.ShortName,
    //                MarketingName = row.MarketingName,
    //                LongName = row.LongName,
    //                DNumber = row.DNumber,
    //                Description = row.Description,
    //                Score = row.Score,
    //                ProjectID = match?.ProjectID ?? 0,
    //                ClientName = match?.ClientName,
    //                ClientID = match?.ClientID
    //            };
    //        }).ToList();
    //    }
    //    catch (HttpRequestException ex)
    //    {
    //        return StatusCode(502, ex.Message);
    //    }
    //    return Ok(result);
    //}

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
    private static async Task<ClientResult<Assistant>> CreateAssistantClient(OpenAIFileClient fileClient, Stream blobStream, AssistantClient assistantClient, string projectContext, string filename)
    {
        OpenAIFile file = await fileClient.UploadFileAsync(blobStream, filename, FileUploadPurpose.Assistants);
        var assistant = await assistantClient.CreateAssistantAsync("gpt-4.1",
            new AssistantCreationOptions()
            {
                Instructions = projectContext + Instructions,
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