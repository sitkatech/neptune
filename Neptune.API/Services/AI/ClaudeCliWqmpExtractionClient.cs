using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace Neptune.API.Services.AI;

/// <summary>
/// NPT-1133: the local-development extraction path. Each category runs <c>claude -p</c> on this
/// machine, signed in as the developer, so local extractions and Neptune.Eval runs don't spend
/// the shared API key. Startup refuses this client outside Development.
///
/// The PDF is written to a temp folder and the CLI reads it with its Read tool. A Read without
/// a page range sends the whole PDF as one document block, the same input the API path sends,
/// but only up to 20 MB; larger PDFs are split into page-range parts here so every part is
/// still read whole. Page-range reads are avoided: they need poppler's pdftoppm and, without
/// it, the CLI reports success with an empty answer. Every part must come back from the Read
/// tool or the call fails with a clear error.
/// </summary>
public class ClaudeCliWqmpExtractionClient : IWqmpExtractionClient
{
    /// <summary>The CLI's Read tool refuses whole-PDF reads over 20 MB; split below that with some headroom.</summary>
    public const long MaxPdfPartBytes = 18L * 1024 * 1024;

    /// <summary>Above this many pages the request can pass the CLI's default 200k context, so use the 1M variant.</summary>
    private const int LongContextPageThreshold = 80;

    private readonly NeptuneDbContext _dbContext;
    private readonly WqmpPdfBlobReader _pdfBlobReader;
    private readonly ILogger<ClaudeCliWqmpExtractionClient> _logger;
    private readonly NeptuneConfiguration _configuration;

    public ClaudeCliWqmpExtractionClient(
        NeptuneDbContext dbContext,
        WqmpPdfBlobReader pdfBlobReader,
        ILogger<ClaudeCliWqmpExtractionClient> logger,
        IOptions<NeptuneConfiguration> configuration)
    {
        _dbContext = dbContext;
        _pdfBlobReader = pdfBlobReader;
        _logger = logger;
        _configuration = configuration.Value;
    }

    public string PathName => "CLI";

    public async Task<IWqmpExtractionSession> OpenDocumentAsync(int waterQualityManagementPlanDocumentID,
        IReadOnlyList<WqmpExtractionCategory> categories, ConcurrentQueue<string> hiccups, CancellationToken cancellationToken)
    {
        var document = await _dbContext.WaterQualityManagementPlanDocuments
            .Include(x => x.FileResource)
            .AsNoTracking()
            .SingleAsync(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID, cancellationToken);

        if (document.FileResource.ContentLength > _configuration.MaxExtractablePdfSizeBytes)
        {
            throw new AnthropicFileService.PdfTooLargeForUploadException(
                document.FileResource.ContentLength, _configuration.MaxExtractablePdfSizeBytes);
        }

        var folder = Path.Combine(Path.GetTempPath(), "neptune-wqmp-cli", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(folder);
        try
        {
            var sourcePath = Path.Combine(folder, "source.pdf");
            using (var pdf = await _pdfBlobReader.OpenAsync(document, cancellationToken))
            await using (var file = File.Create(sourcePath))
            {
                await pdf.Content.CopyToAsync(file, cancellationToken);
            }

            var (parts, pageCount) = WritePdfParts(sourcePath, folder, MaxPdfPartBytes);
            _logger.LogInformation("Claude CLI: documentID={DocumentID} ({Pages} pages) written to {Folder} as {Parts} part(s)",
                waterQualityManagementPlanDocumentID, pageCount?.ToString() ?? "unknown", folder, parts.Count);

            return new Session(this, folder, parts, pageCount, hiccups);
        }
        catch
        {
            TryDeleteFolder(folder);
            throw;
        }
    }

    /// <summary>One file the CLI reads, covering original pages <see cref="FirstPage"/>–<see cref="LastPage"/>.</summary>
    public sealed record PdfPart(string FileName, int FirstPage, int LastPage);

    /// <summary>
    /// Moves the PDF to <c>document.pdf</c> when it's within <paramref name="maxPartBytes"/>,
    /// otherwise writes consecutive page-range parts that each are. Returns the parts in page
    /// order and the page count, which is null when a PDF that needs no split couldn't be parsed.
    /// </summary>
    public static (List<PdfPart> parts, int? pageCount) WritePdfParts(string sourcePath, string folder, long maxPartBytes)
    {
        var totalBytes = new FileInfo(sourcePath).Length;

        if (totalBytes <= maxPartBytes)
        {
            // The count only picks the context size. A PDF PDFsharp can't parse (encrypted,
            // slightly malformed) may still be one Claude reads fine, as the API path does, so
            // don't fail the extraction over it.
            int? count;
            try
            {
                using var reader = PdfReader.Open(sourcePath, PdfDocumentOpenMode.Import);
                count = reader.PageCount;
            }
            catch (Exception)
            {
                count = null;
            }
            File.Move(sourcePath, Path.Combine(folder, "document.pdf"));
            return ([new PdfPart("document.pdf", 1, count ?? 0)], count);
        }

        // PDFsharp copies only the objects each page references. PdfPig's page copy pulled the
        // whole file's shared images into every part (one page of a 47 MB scan came out at 47 MB).
        PdfDocument source;
        try
        {
            source = PdfReader.Open(sourcePath, PdfDocumentOpenMode.Import);
        }
        catch (Exception ex)
        {
            throw new ClaudeCliException(
                $"This PDF is over the Claude CLI's 20 MB read limit and couldn't be split into smaller parts ({ex.Message}). Use the API path for this document.", ex);
        }
        using var _ = source;
        var pageCount = source.PageCount;
        if (pageCount < 1)
        {
            throw new ClaudeCliException("This PDF has no readable pages, so it can't be split for the Claude CLI. Use the API path for this document.");
        }

        var parts = new List<PdfPart>();

        // Even page ranges sized from the average page; a range that still comes out too big
        // (scanned pages vary a lot) is halved until it fits.
        void Write(int firstPage, int lastPage)
        {
            using var part = new PdfDocument();
            for (var page = firstPage; page <= lastPage; page++)
            {
                part.AddPage(source.Pages[page - 1]);
            }
            using var buffer = new MemoryStream();
            part.Save(buffer, closeStream: false);
            var bytes = buffer.ToArray();

            if (bytes.LongLength > maxPartBytes)
            {
                if (firstPage == lastPage)
                {
                    throw new ClaudeCliException(
                        $"Page {firstPage} of this PDF is {bytes.LongLength / (1024.0 * 1024.0):F0} MB on its own, over the Claude CLI's 20 MB read limit. Use the API path for this document.");
                }
                var middle = (firstPage + lastPage) / 2;
                Write(firstPage, middle);
                Write(middle + 1, lastPage);
                return;
            }

            var fileName = $"document-pages-{firstPage}-{lastPage}.pdf";
            File.WriteAllBytes(Path.Combine(folder, fileName), bytes);
            parts.Add(new PdfPart(fileName, firstPage, lastPage));
        }

        foreach (var (firstPage, lastPage) in PlanPageRanges(pageCount, totalBytes, maxPartBytes))
        {
            Write(firstPage, lastPage);
        }
        return (parts, pageCount);
    }

    /// <summary>Splits 1..<paramref name="pageCount"/> into the fewest even ranges whose average size fits.</summary>
    public static List<(int firstPage, int lastPage)> PlanPageRanges(int pageCount, long totalBytes, long maxPartBytes)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pageCount, 1);
        var partCount = (int)Math.Min(pageCount, Math.Max(1, (long)Math.Ceiling((double)totalBytes / maxPartBytes)));
        var pagesPerPart = (int)Math.Ceiling((double)pageCount / partCount);
        var ranges = new List<(int, int)>();
        for (var first = 1; first <= pageCount; first += pagesPerPart)
        {
            ranges.Add((first, Math.Min(pageCount, first + pagesPerPart - 1)));
        }
        return ranges;
    }

    /// <summary>The instructions that tell the model where the PDF is and how to read it.</summary>
    public static string BuildDocumentInstructions(IReadOnlyList<PdfPart> parts)
    {
        if (parts.Count == 1)
        {
            return $"The WQMP PDF is the file `{parts[0].FileName}` in the current working directory. Read it with the Read tool exactly once, " +
                   "WITHOUT a `pages` parameter, so you receive the whole document at once. Do not read any other file.";
        }

        var sb = new StringBuilder();
        sb.AppendLine($"The WQMP PDF is too large to read in one call, so it is split into {parts.Count} files in the current working directory, in page order:");
        foreach (var part in parts)
        {
            sb.AppendLine($"- `{part.FileName}`: original pages {part.FirstPage}-{part.LastPage}");
        }
        sb.Append("Read EACH file with the Read tool exactly once, WITHOUT a `pages` parameter, and treat them together as one document. " +
                  "Every page reference you report (DocumentSource and BoundingBox.PageNumber) must be the ORIGINAL page number: " +
                  "the file's first original page plus the page's position within that file, minus 1. Do not read any other file.");
        return sb.ToString();
    }

    /// <summary>
    /// The CLI takes the schema as a command-line argument, and the WQMP schema inlines 22
    /// ExtractedValue objects (~29k characters, near Windows' 32k command-line limit). Defines the
    /// shape once under $defs and references it, as the array schemas already do; the per-field
    /// description stays beside each $ref. Same structure, about a tenth of the size.
    /// </summary>
    public static string CompactSchema(string schemaJson)
    {
        var root = JsonNode.Parse(schemaJson)!.AsObject();
        JsonObject extractedValueDefinition = null;

        void Visit(JsonNode node)
        {
            if (node is JsonObject obj)
            {
                if (obj["properties"] is JsonObject properties)
                {
                    foreach (var (name, value) in properties.ToList())
                    {
                        if (value is JsonObject candidate && IsExtractedValueShape(candidate))
                        {
                            if (extractedValueDefinition == null)
                            {
                                extractedValueDefinition = (JsonObject)candidate.DeepClone();
                                extractedValueDefinition["description"] = "Attribute with evidence.";
                            }
                            var reference = new JsonObject { ["$ref"] = "#/$defs/ExtractedValue" };
                            if (candidate["description"] is JsonValue description)
                            {
                                reference["description"] = description.DeepClone();
                            }
                            properties[name] = reference;
                        }
                        else if (value != null)
                        {
                            Visit(value);
                        }
                    }
                }
                foreach (var (name, value) in obj.ToList())
                {
                    if (name != "properties" && value != null) Visit(value);
                }
            }
            else if (node is JsonArray array)
            {
                foreach (var item in array) if (item != null) Visit(item);
            }
        }

        Visit(root);

        if (extractedValueDefinition != null)
        {
            var defs = root["$defs"] as JsonObject ?? new JsonObject();
            if (!defs.ContainsKey("ExtractedValue")) defs["ExtractedValue"] = extractedValueDefinition;
            root["$defs"] = defs;
        }
        return root.ToJsonString();
    }

    private static bool IsExtractedValueShape(JsonObject obj) =>
        obj["properties"] is JsonObject p && p.ContainsKey("Value") && p.ContainsKey("ExtractionEvidence")
        && p.ContainsKey("DocumentSource") && p.ContainsKey("BoundingBox");

    private sealed class Session(
        ClaudeCliWqmpExtractionClient client,
        string folder,
        List<PdfPart> parts,
        int? pageCount,
        ConcurrentQueue<string> hiccups) : IWqmpExtractionSession
    {
        // No prompt cache shared between CLI processes, so nothing to warm.
        public bool WarmCacheWithFirstCategory => false;

        public async Task<WqmpCategoryCallResult> ExtractCategoryAsync(WqmpCategoryCall call, TaskCompletionSource responseStarted,
            CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            var key = call.Category.Key;
            // An unknown page count gets the long context too: a smaller window can't hold a long plan.
            var model = pageCount is null or > LongContextPageThreshold ? $"{client._configuration.ClaudeModelId}[1m]" : client._configuration.ClaudeModelId;

            var arguments = new List<string>
            {
                "-p",
                "--output-format", "stream-json",
                "--verbose",
                "--json-schema", CompactSchema(call.Category.Schema),
                "--model", model,
                "--system-prompt", $"{call.SystemPrompt}\n\n{BuildDocumentInstructions(parts)}",
                // Read only, confined to the temp folder (the working directory); nothing else
                // from the developer's Claude setup (settings, hooks, MCP servers, skills).
                "--tools", "Read",
                "--allowedTools", "Read",
                "--permission-mode", "dontAsk",
                "--setting-sources", "",
                "--strict-mcp-config",
                "--disable-slash-commands",
                "--no-session-persistence",
            };
            // Same effort as the API path, so a CLI run approximates the production setting.
            if (AnthropicApiWqmpExtractionClient.NormalizeEffort(client._configuration.ClaudeEffort) is { } effort)
            {
                arguments.Add("--effort");
                arguments.Add(effort);
            }
            // The prompt goes through stdin: DomainContext alone is too long for a Windows command line.
            var prompt = $"DomainContext:\n{call.DomainContext}\n\n{call.Prompt}\n\nReturn your extraction as the structured output.";

            var run = await client.RunCliAsync(arguments, prompt, folder, key, responseStarted, cancellationToken);

            if (run.Result == null)
            {
                throw new ClaudeCliException($"The Claude CLI stopped without a result on {key} (exit code {run.ExitCode}). {Tail(run.StdErr, 500)}".Trim());
            }
            if (run.Result.IsError)
            {
                throw new ClaudeCliException(DescribeCliError(run.Result.ResultText, key));
            }

            var unread = parts.Select(p => p.FileName).Where(f => !run.DocumentsRead.Contains(f)).ToList();
            if (unread.Count > 0)
            {
                var problem = run.ReadProblems.FirstOrDefault();
                throw new ClaudeCliException(
                    $"The Claude CLI didn't read the whole PDF on {key} ({unread.Count} of {parts.Count} file(s) never came back from the Read tool)" +
                    (problem != null ? $": {Tail(problem, 300)}" : "."));
            }

            if (run.Result.StructuredOutput == null)
            {
                hiccups.Enqueue($"{key}: no structured output (stop_reason={run.Result.StopReason})");
            }

            return new WqmpCategoryCallResult
            {
                OutputJson = run.Result.StructuredOutput,
                Usage = new WqmpExtractionCallUsageDto
                {
                    Category = key,
                    InputTokens = run.Result.InputTokens,
                    CacheCreationInputTokens = run.Result.CacheCreationInputTokens,
                    CacheReadInputTokens = run.Result.CacheReadInputTokens,
                    OutputTokens = run.Result.OutputTokens,
                    StopReason = run.Result.StopReason,
                    ElapsedMs = sw.ElapsedMilliseconds,
                },
            };
        }

        public ValueTask DisposeAsync()
        {
            TryDeleteFolder(folder);
            return ValueTask.CompletedTask;
        }
    }

    /// <summary>Turns the CLI's error text into one that tells the developer what to do.</summary>
    public static string DescribeCliError(string resultText, string category)
    {
        var text = resultText ?? "";
        if (text.Contains("not logged in", StringComparison.OrdinalIgnoreCase) || text.Contains("/login", StringComparison.OrdinalIgnoreCase))
        {
            return "The Claude CLI isn't logged in. Run `claude` once and sign in, then try the extraction again.";
        }
        // Claude Code's plan-limit wording ("Claude AI usage limit reached", "You've hit your limit").
        // Rate limits and context-size errors fall through to the generic message.
        if (text.Contains("usage limit", StringComparison.OrdinalIgnoreCase) || text.Contains("hit your limit", StringComparison.OrdinalIgnoreCase))
        {
            return $"Your Claude plan's usage limit was reached (Claude CLI: {Tail(text, 300)}).";
        }
        return $"The Claude CLI failed on {category}: {Tail(text, 500)}";
    }

    private async Task<ClaudeCliRun> RunCliAsync(List<string> arguments, string stdin, string workingDirectory, string category,
        TaskCompletionSource responseStarted, CancellationToken cancellationToken)
    {
        var startInfo = new ProcessStartInfo(_configuration.ClaudeCliPath)
        {
            WorkingDirectory = workingDirectory,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
            // Explicit UTF-8 (no BOM) both ways: on Windows the default is the console code page,
            // which mangles the prompts' non-ASCII characters (≤, –, ✓).
            StandardInputEncoding = new UTF8Encoding(false),
            StandardOutputEncoding = Encoding.UTF8,
            StandardErrorEncoding = Encoding.UTF8,
        };
        foreach (var argument in arguments) startInfo.ArgumentList.Add(argument);
        // Never let the CLI fall back to an API key from this process's environment: that would
        // spend API credits, which is what this path exists to avoid.
        startInfo.Environment.Remove("ANTHROPIC_API_KEY");
        startInfo.Environment.Remove("ANTHROPIC_AUTH_TOKEN");

        using var process = new Process { StartInfo = startInfo };
        try
        {
            process.Start();
        }
        catch (Win32Exception ex)
        {
            throw new ClaudeCliException(
                $"Couldn't start the Claude CLI ('{_configuration.ClaudeCliPath}'): {ex.Message}. Install Claude Code, or set ClaudeCliPath to the full path of the claude executable.", ex);
        }

        var run = new ClaudeCliRun();
        var stderr = new StringBuilder();
        process.ErrorDataReceived += (_, e) =>
        {
            if (e.Data != null && stderr.Length < 20_000) lock (stderr) stderr.AppendLine(e.Data);
        };
        process.BeginErrorReadLine();

        using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutCts.CancelAfter(TimeSpan.FromMinutes(_configuration.ClaudeCliTimeoutMinutes));
        try
        {
            await process.StandardInput.WriteAsync(stdin.AsMemory(), timeoutCts.Token);
            process.StandardInput.Close();

            string line;
            while ((line = await process.StandardOutput.ReadLineAsync(timeoutCts.Token)) != null)
            {
                run.Feed(line);
                // This path must never spend an API key; see ClaudeCliRun.CredentialProblem.
                if (run.CredentialProblem is { } credentialProblem)
                {
                    try { process.Kill(entireProcessTree: true); } catch (InvalidOperationException) { }
                    throw new ClaudeCliException(credentialProblem);
                }
                if (run.Started) responseStarted?.TrySetResult();
            }
            await process.WaitForExitAsync(timeoutCts.Token);
        }
        catch (OperationCanceledException)
        {
            try { process.Kill(entireProcessTree: true); } catch (InvalidOperationException) { }
            if (cancellationToken.IsCancellationRequested) throw;
            throw new ClaudeCliException($"The Claude CLI took longer than {_configuration.ClaudeCliTimeoutMinutes} minutes on {category} (ClaudeCliTimeoutMinutes).");
        }

        run.ExitCode = process.ExitCode;
        lock (stderr) run.StdErr = stderr.ToString();
        _logger.LogInformation("Claude CLI finished {Category}: exit={ExitCode}, turns={Turns}, documents read={DocumentsRead}, apiKeySource={ApiKeySource}",
            category, run.ExitCode, run.Result?.NumTurns, run.DocumentsRead.Count, run.ApiKeySource);
        return run;
    }

    private static string Tail(string text, int max) =>
        string.IsNullOrEmpty(text) || text.Length <= max ? text ?? "" : "…" + text[^max..];

    private static void TryDeleteFolder(string folder)
    {
        try
        {
            if (Directory.Exists(folder)) Directory.Delete(folder, recursive: true);
        }
        catch (IOException)
        {
            // A leftover temp folder isn't worth failing the extraction over.
        }
        catch (UnauthorizedAccessException)
        {
        }
    }
}

/// <summary>A Claude CLI failure with a message written for the developer (shown in the review wizard's alert).</summary>
public class ClaudeCliException : InvalidOperationException
{
    public ClaudeCliException(string message, Exception inner = null) : base(message, inner) { }
}

/// <summary>
/// Reads the CLI's <c>--output-format stream-json</c> events: which files the Read tool returned
/// as documents, what went wrong when one didn't, and the final result event.
/// </summary>
public sealed class ClaudeCliRun
{
    /// <summary>What the init event's <c>apiKeySource</c> says when the CLI uses the developer's own login.</summary>
    public const string LoginApiKeySource = "none";

    private readonly Dictionary<string, string> _readToolFiles = new();

    /// <summary>The credential the CLI reported in its init event; null until that event arrives.</summary>
    public string ApiKeySource { get; private set; }
    public HashSet<string> DocumentsRead { get; } = new(StringComparer.OrdinalIgnoreCase);
    public List<string> ReadProblems { get; } = new();
    public ClaudeCliResult Result { get; private set; }
    public bool Started { get; private set; }
    public int ExitCode { get; set; }
    public string StdErr { get; set; }

    /// <summary>
    /// Why the run must stop, or null. The init event names the credential before the first
    /// request goes out: anything but "none" (the developer's own login) means an API key. A run
    /// that answers without ever naming its credential is stopped too (fail closed), unless it
    /// only reports an error, so "not logged in" still gets its own message.
    /// </summary>
    public string CredentialProblem =>
        ApiKeySource != null && ApiKeySource != LoginApiKeySource
            ? $"The Claude CLI was about to use an API key (apiKeySource={ApiKeySource}) instead of your Claude login, so the extraction was stopped. " +
              "Remove the key from your Claude Code setup (ANTHROPIC_API_KEY, apiKeyHelper) and sign in with `claude`."
            : ApiKeySource == null && (Started || Result is { IsError: false })
                ? "The Claude CLI didn't say which credential it was using, so the extraction was stopped (it must be your Claude login, never an API key). Update Claude Code and try again."
                : null;

    public void Feed(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return;
        JsonNode evt;
        try
        {
            evt = JsonNode.Parse(line);
        }
        catch (JsonException)
        {
            return;
        }
        if (evt is not JsonObject obj) return;

        switch ((string)obj["type"])
        {
            case "system" when (string)obj["subtype"] == "init":
                // Fail closed: a CLI that doesn't say which credential it uses doesn't get to run.
                ApiKeySource = obj["apiKeySource"] is JsonValue source && source.TryGetValue<string>(out var value) ? value : "(not reported)";
                break;
            case "assistant":
                Started = true;
                foreach (var block in Blocks(obj))
                {
                    if ((string)block["type"] == "tool_use" && (string)block["name"] == "Read" && block["id"] is JsonValue id)
                    {
                        _readToolFiles[(string)id] = Path.GetFileName((string)block["input"]?["file_path"] ?? "");
                    }
                }
                break;
            case "user":
                foreach (var block in Blocks(obj))
                {
                    if ((string)block["type"] != "tool_result" || block["tool_use_id"] is not JsonValue toolUseID) continue;
                    if (!_readToolFiles.TryGetValue((string)toolUseID, out var fileName)) continue;

                    // A whole-file read returns a document block; a paged read (only possible
                    // where poppler is installed) returns page images. Either way the file was read.
                    if (block["content"] is JsonArray content && content.Any(c => (string)c?["type"] is "document" or "image"))
                    {
                        DocumentsRead.Add(fileName);
                    }
                    else
                    {
                        ReadProblems.Add(block["content"] is JsonValue text ? (string)text : block["content"]?.ToJsonString() ?? "");
                    }
                }
                break;
            case "result":
                Result = ClaudeCliResult.From(obj);
                break;
        }
    }

    private static IEnumerable<JsonObject> Blocks(JsonObject evt) =>
        (evt["message"]?["content"] as JsonArray ?? []).OfType<JsonObject>();
}

public sealed class ClaudeCliResult
{
    public bool IsError { get; init; }
    public string ResultText { get; init; }
    /// <summary>The schema-validated output as JSON, or null when there was none.</summary>
    public string StructuredOutput { get; init; }
    public string StopReason { get; init; }
    public int? NumTurns { get; init; }
    public long InputTokens { get; init; }
    public long CacheCreationInputTokens { get; init; }
    public long CacheReadInputTokens { get; init; }
    public long OutputTokens { get; init; }

    public static ClaudeCliResult From(JsonObject evt)
    {
        var usage = evt["usage"] as JsonObject;
        long Usage(string name) => usage?[name] is JsonValue v && v.TryGetValue<long>(out var n) ? n : 0;
        return new ClaudeCliResult
        {
            IsError = evt["is_error"] is JsonValue e && e.TryGetValue<bool>(out var isError) && isError,
            ResultText = evt["result"] is JsonValue r && r.TryGetValue<string>(out var text) ? text : null,
            StructuredOutput = evt["structured_output"] is JsonObject or JsonArray ? evt["structured_output"]!.ToJsonString() : null,
            StopReason = evt["stop_reason"] is JsonValue s && s.TryGetValue<string>(out var stop) ? stop : null,
            NumTurns = evt["num_turns"] is JsonValue t && t.TryGetValue<int>(out var turns) ? turns : null,
            InputTokens = Usage("input_tokens"),
            CacheCreationInputTokens = Usage("cache_creation_input_tokens"),
            CacheReadInputTokens = Usage("cache_read_input_tokens"),
            OutputTokens = Usage("output_tokens"),
        };
    }
}
