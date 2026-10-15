using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Services.AI;

/// <summary>
/// NPT-1133: how a WQMP extraction reaches Claude. <see cref="WqmpExtractionService"/> owns the
/// prompts, the four categories and all post-processing; an implementation only gets one
/// document in front of Claude and returns each category's raw JSON.
///
/// <see cref="AnthropicApiWqmpExtractionClient"/> is the production path (API key, Files API,
/// prompt caching). <see cref="ClaudeCliWqmpExtractionClient"/> is the local-development path
/// through the developer's own Claude Code login; Startup refuses it outside Development.
/// </summary>
public interface IWqmpExtractionClient
{
    /// <summary>"API" or "CLI", for logs and the eval report.</summary>
    string PathName { get; }

    /// <summary>
    /// Prepares the document for the category calls (uploads it, or writes it to a temp folder).
    /// <paramref name="hiccups"/> collects failures the client recovers from, as the service's
    /// own list does.
    /// </summary>
    Task<IWqmpExtractionSession> OpenDocumentAsync(int waterQualityManagementPlanDocumentID,
        IReadOnlyList<WqmpExtractionCategory> categories, ConcurrentQueue<string> hiccups, CancellationToken cancellationToken);
}

/// <summary>One document, prepared for its category calls. Dispose releases anything the client made for it.</summary>
public interface IWqmpExtractionSession : IAsyncDisposable
{
    /// <summary>
    /// True when the first category should run alone until its response starts, so the others
    /// read the PDF from the prompt cache instead of writing it again (API path, NPT-1132).
    /// </summary>
    bool WarmCacheWithFirstCategory { get; }

    /// <param name="responseStarted">Signalled once the response starts; may be null.</param>
    Task<WqmpCategoryCallResult> ExtractCategoryAsync(WqmpCategoryCall call, TaskCompletionSource responseStarted, CancellationToken cancellationToken);
}

/// <summary>A category and the JSON schema its output must match.</summary>
public sealed record WqmpExtractionCategory(string Key, string Schema)
{
    public string ToolName => $"emit_{Key.ToLower()}_extraction";
}

/// <summary>Everything one category call sends besides the PDF.</summary>
public sealed class WqmpCategoryCall
{
    public WqmpExtractionCategory Category { get; init; }
    /// <summary>The evidence instructions; identical on every call.</summary>
    public string SystemPrompt { get; init; }
    public string DomainContext { get; init; }
    /// <summary>The rendered category prompt.</summary>
    public string Prompt { get; init; }
}

public sealed class WqmpCategoryCallResult
{
    /// <summary>The category's output JSON, or null when the model produced none (already reported as a hiccup).</summary>
    public string OutputJson { get; init; }
    public WqmpExtractionCallUsageDto Usage { get; init; }
}
