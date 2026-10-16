using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Anthropic;
using Anthropic.Exceptions;
using Anthropic.Models.Beta.Messages;
using BetaRole = Anthropic.Models.Beta.Messages.Role;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Services.AI;

/// <summary>
/// The production extraction path: the PDF goes to the Anthropic Files API once (file_id cached
/// on the document) and each category is a streamed Beta Messages call that references it.
/// </summary>
public class AnthropicApiWqmpExtractionClient : IWqmpExtractionClient
{
    private readonly AnthropicClient _anthropic;
    private readonly AnthropicFileService _anthropicFileService;
    private readonly ILogger<AnthropicApiWqmpExtractionClient> _logger;
    private readonly NeptuneConfiguration _configuration;

    public AnthropicApiWqmpExtractionClient(
        AnthropicClient anthropic,
        AnthropicFileService anthropicFileService,
        ILogger<AnthropicApiWqmpExtractionClient> logger,
        IOptions<NeptuneConfiguration> configuration)
    {
        _anthropic = anthropic;
        _anthropicFileService = anthropicFileService;
        _logger = logger;
        _configuration = configuration.Value;
    }

    public string PathName => "API";

    public async Task<IWqmpExtractionSession> OpenDocumentAsync(int waterQualityManagementPlanDocumentID,
        IReadOnlyList<WqmpExtractionCategory> categories, ConcurrentQueue<string> hiccups, CancellationToken cancellationToken)
    {
        // NPT-1133: local environments no longer get the shared key, so say what's missing
        // instead of letting the upload 401 into the "Anthropic account issue" alert.
        if (string.IsNullOrWhiteSpace(_configuration.AnthropicApiKey))
        {
            throw new InvalidOperationException(
                "AnthropicApiKey is not set, so AI extraction can't use the Anthropic API. Locally, extraction runs through the Claude CLI (ClaudeCliEnabled).");
        }

        // NPT-1044: upload to Anthropic Files API once and cache the file_id on the document.
        // The Files-API source path on the Beta Messages API supports up to 500 MB per file
        // — vs. 32 MB on the URL-source path we used previously — and dodges the SAS-URL
        // expiry race we used to see on long-running extractions.
        var fileID = await _anthropicFileService.EnsureUploadedFileIDAsync(waterQualityManagementPlanDocumentID, cancellationToken);
        _logger.LogInformation("Anthropic file ID resolved for documentID={DocumentID}: {FileID}",
            waterQualityManagementPlanDocumentID, fileID);

        // Build all 4 tools upfront — identical across all parallel calls so the tools-level cache is shared.
        var allTools = categories.Select(BuildToolForCategory).ToList();
        return new Session(this, waterQualityManagementPlanDocumentID, fileID, allTools, hiccups);
    }

    private sealed class Session(
        AnthropicApiWqmpExtractionClient client,
        int documentID,
        string fileID,
        List<BetaTool> allTools,
        ConcurrentQueue<string> hiccups) : IWqmpExtractionSession
    {
        public bool WarmCacheWithFirstCategory => true;

        public async Task<WqmpCategoryCallResult> ExtractCategoryAsync(WqmpCategoryCall call, TaskCompletionSource responseStarted,
            CancellationToken cancellationToken)
        {
            // Retry once on a stale-file_id 404 — invalidate the cached id and re-upload
            // (serialized across the 4 parallel calls inside RefreshFileIDAsync), then retry.
            try
            {
                return await client.StreamCategoryAsync(call, fileID, allTools, hiccups, responseStarted, cancellationToken);
            }
            catch (AnthropicNotFoundException ex)
            {
                client._logger.LogWarning(ex, "Anthropic 404 on {Category} for documentID={DocumentID} (likely stale file_id); refreshing and retrying once.",
                    call.Category.Key, documentID);
                hiccups.Enqueue($"{call.Category.Key}: stale file_id 404, re-uploaded and retried");
                var refreshedFileID = await client._anthropicFileService.RefreshFileIDAsync(documentID, fileID, cancellationToken);
                return await client.StreamCategoryAsync(call, refreshedFileID, allTools, hiccups, responseStarted, cancellationToken);
            }
        }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;
    }

    /// <summary>
    /// One category call bound to a specific file_id. A 4-minute timeout keeps a single stalled
    /// call from hanging the whole extraction.
    /// </summary>
    private async Task<WqmpCategoryCallResult> StreamCategoryAsync(WqmpCategoryCall call, string fileID, List<BetaTool> allTools,
        ConcurrentQueue<string> hiccups, TaskCompletionSource responseStarted, CancellationToken cancellationToken)
    {
        var sw = Stopwatch.StartNew();
        var key = call.Category.Key;
        var toolName = call.Category.ToolName;

        using var categoryCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        categoryCts.CancelAfter(TimeSpan.FromMinutes(4));

        // System prompt: evidence instructions (cached — stable across all 4 calls)
        var systemBlocks = new List<BetaTextBlockParam>
        {
            new() { Text = call.SystemPrompt, CacheControl = new BetaCacheControlEphemeral() },
        };

        // User message: PDF document (referenced by file_id, cached) + domain context (cached) + per-category prompt (varies)
        var messageContent = new List<BetaContentBlockParam>
        {
            new BetaRequestDocumentBlock { Source = new BetaFileDocumentSource { FileID = fileID } },
            new BetaTextBlockParam { Text = $"DomainContext:\n{call.DomainContext}", CacheControl = new BetaCacheControlEphemeral() },
            new BetaTextBlockParam { Text = $"{call.Prompt}\n\nReturn your extraction by calling the `{toolName}` tool exactly once. Do not call any other tool and do not reply with text." },
        };

        var parameters = new MessageCreateParams
        {
            Model = _configuration.ClaudeModelId,
            // NPT-1054: bumped from 8192 → 16384. Sonnet 4.5+ uses interleaved thinking
            // by default when tool_use is in play; thinking tokens count toward the same
            // output budget. On the SourceControlBMPs call with a longer prompt + a
            // 65-page PDF, Claude consumed all 8192 tokens on thinking and never reached
            // the tool call (stop_reason=max_tokens, no text or input_json captured).
            // NPT-1132: 32768 for Sonnet 5.x, which thinks by default (adaptive) and whose
            // tokenizer counts the same text as ~30% more tokens. Only tokens produced are
            // billed, so the headroom costs nothing on calls that don't use it.
            MaxTokens = 32768,
            OutputConfig = BuildOutputConfig(_configuration.ClaudeEffort),
            // The Files-API source variant (BetaFileDocumentSource) is gated behind
            // this beta header on the Messages endpoint. Without it the request gets
            // routed to the standard validator and rejected with
            // "Input tag 'file' found using 'type' does not match any of the
            // expected tags: 'base64', 'content', 'text', 'url'".
            Betas = ["files-api-2025-04-14"],
            System = systemBlocks,
            Messages = [new() { Role = BetaRole.User, Content = messageContent }],
            Tools = allTools.Select(t => (BetaToolUnion)t).ToList(),
            // NPT-1132: the same ToolChoice on every category call. A tool_choice change
            // between requests invalidates the messages cache, which is where the PDF
            // sits, so forcing a different tool per category (the previous design) made
            // all four calls re-cache the whole PDF at 1.25x. The prompt's last line names
            // the tool instead; the stream handler below checks which tool was called.
            // `auto` is also what Sonnet 5.5 / Opus 5.5 require (they reject forced tools).
            ToolChoice = new BetaToolChoiceAuto { DisableParallelToolUse = true },
        };

        // Stream the response — keeps the HTTP connection alive via SSE so there's no
        // HttpClient.Timeout to worry about, even for large PDFs on a cold cache.
        var toolInputJson = new StringBuilder();
        long cachedTokens = 0;
        long cacheCreationTokens = 0;
        long inputTokens = 0;
        long outputTokens = 0;
        string stopReason = null;
        var inExpectedTool = false;
        string otherToolCalled = null;

        await foreach (var evt in _anthropic.Beta.Messages.CreateStreaming(parameters, categoryCts.Token))
        {
            if (evt.TryPickContentBlockStart(out var blockStart))
            {
                // With ToolChoice=auto the model picks the tool; only keep the input of
                // the one this category asked for.
                inExpectedTool = false;
                if (blockStart.ContentBlock.TryPickBetaToolUse(out var toolUse))
                {
                    inExpectedTool = toolUse.Name == toolName;
                    if (!inExpectedTool) otherToolCalled = toolUse.Name;
                }
            }
            else if (evt.TryPickContentBlockDelta(out var delta))
            {
                // Tool-use input arrives as input_json_delta chunks
                if (inExpectedTool && delta.Delta.TryPickInputJson(out var jsonDelta))
                {
                    toolInputJson.Append(jsonDelta.PartialJson);
                }
            }
            else if (evt.TryPickDelta(out var msgDelta))
            {
                // message_delta carries usage info
                if (msgDelta.Usage != null)
                {
                    outputTokens = msgDelta.Usage.OutputTokens;
                }
                stopReason = msgDelta.Delta?.StopReason?.ToString() ?? stopReason;
            }
            else if (evt.TryPickStart(out var msgStart))
            {
                if (msgStart.Message?.Usage != null)
                {
                    inputTokens = msgStart.Message.Usage.InputTokens;
                    cachedTokens = msgStart.Message.Usage.CacheReadInputTokens ?? 0;
                    cacheCreationTokens = msgStart.Message.Usage.CacheCreationInputTokens ?? 0;
                }
                responseStarted?.TrySetResult();
            }
        }

        if (stopReason != null && stopReason.Contains("refusal", StringComparison.OrdinalIgnoreCase))
        {
            // NPT-1132: Sonnet 5.x can decline (stop_reason "refusal"); a WQMP shouldn't trigger
            // it, so surface it rather than let it look like an ordinary empty category.
            hiccups.Enqueue($"{key}: model declined (stop_reason=refusal), empty fallback used");
        }
        else if (toolInputJson.Length == 0)
        {
            hiccups.Enqueue(otherToolCalled != null
                ? $"{key}: called {otherToolCalled} instead of {toolName}"
                : $"{key}: no tool output (stop_reason={stopReason})");
        }
        else if (stopReason != null && stopReason.Contains("max_tokens", StringComparison.OrdinalIgnoreCase))
        {
            hiccups.Enqueue($"{key}: hit max_tokens");
        }

        var refused = stopReason != null && stopReason.Contains("refusal", StringComparison.OrdinalIgnoreCase);
        return new WqmpCategoryCallResult
        {
            OutputJson = toolInputJson.Length > 0 && !refused ? toolInputJson.ToString() : null,
            Usage = new WqmpExtractionCallUsageDto
            {
                Category = key,
                InputTokens = inputTokens,
                CacheCreationInputTokens = cacheCreationTokens,
                CacheReadInputTokens = cachedTokens,
                OutputTokens = outputTokens,
                StopReason = stopReason,
                ElapsedMs = sw.ElapsedMilliseconds,
            },
        };
    }

    /// <summary>NPT-1132: <see cref="NeptuneConfiguration.ClaudeEffort"/> as an output config; null when unset.</summary>
    public static BetaOutputConfig BuildOutputConfig(string effort) => NormalizeEffort(effort) switch
    {
        null => null,
        "low" => new BetaOutputConfig { Effort = Effort.Low },
        "medium" => new BetaOutputConfig { Effort = Effort.Medium },
        "high" => new BetaOutputConfig { Effort = Effort.High },
        "xhigh" => new BetaOutputConfig { Effort = Effort.Xhigh },
        _ => new BetaOutputConfig { Effort = Effort.Max },
    };

    /// <summary>The effort level, normalized to lower case, or null when unset. Throws on anything else.</summary>
    public static string NormalizeEffort(string effort)
    {
        if (string.IsNullOrWhiteSpace(effort)) return null;
        var normalized = effort.Trim().ToLowerInvariant();
        return normalized is "low" or "medium" or "high" or "xhigh" or "max"
            ? normalized
            : throw new InvalidOperationException($"ClaudeEffort '{effort}' isn't one of low, medium, high, xhigh, max.");
    }

    private static BetaTool BuildToolForCategory(WqmpExtractionCategory category)
    {
        var parsed = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(category.Schema);
        return new BetaTool
        {
            Name = category.ToolName,
            Description = $"Emit the extracted {category.Key} fields as structured JSON matching the required schema.",
            // NPT-1106: strict tool use (GA, no beta header) — the API validates the tool input
            // against the schema server-side, guaranteeing well-formed JSON that matches it
            // exactly. Kills the recurring UnwrapItems failure class: `items` emitted as a
            // double-encoded string with unescaped quotes / trailing commas that no regex
            // cleanup could reliably repair. Strict requires additionalProperties:false +
            // required on every object INCLUDING the root, so the schema JSON is passed to
            // InputSchema wholesale via the raw-data constructor.
            //
            // Strict is per-category: the API compiles EVERY strict tool in the request into a
            // grammar and rejects the request when the combined grammar is too large ("The
            // compiled grammar is too large"). All four tools ride in every request so the
            // prompt-cache prefix (tools -> system -> the large PDF) stays identical across the
            // four parallel category calls — flipping Strict per call would split that cache
            // four ways. Instead the WQMP tool (22 of the 33 ExtractedValue objects — the bulk
            // of the grammar) stays non-strict: its single-object output has never exhibited
            // the corruption class, which lives in the array categories' `items` handling
            // (SourceControlBMPs above all — see NPT-1054). The three array tools stay strict.
            Strict = category.Key != "WQMP",
            InputSchema = new(parsed ?? new Dictionary<string, JsonElement>()),
        };
    }
}
