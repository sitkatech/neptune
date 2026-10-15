using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.API.Services.AI;

public class WqmpExtractionService
{
    private const string SchemaVersion = "v1.0";
    private static readonly Lazy<string> ExtractedValueSchema = new(BuildExtractedValueJsonSchema);
    private static readonly Lazy<string> WqmpSchema = new(BuildWqmpSchemaJson);
    private static readonly Lazy<string> ParcelSchema = new(BuildParcelSchemaJson);
    private static readonly Lazy<string> QuickBmpSchema = new(BuildQuickBmpSchemaJson);
    private static readonly Lazy<string> SourceControlBmpSchema = new(BuildSourceControlBmpSchemaJson);

    private readonly IWqmpExtractionClient _client;
    private readonly NeptuneDbContext _dbContext;
    private readonly IPromptTemplateService _promptTemplateService;
    private readonly ILogger<WqmpExtractionService> _logger;
    private readonly NeptuneConfiguration _configuration;
    private readonly IHostEnvironment _environment;

    public WqmpExtractionService(
        IWqmpExtractionClient client,
        NeptuneDbContext dbContext,
        IPromptTemplateService promptTemplateService,
        ILogger<WqmpExtractionService> logger,
        IOptions<NeptuneConfiguration> configuration,
        IHostEnvironment environment)
    {
        _client = client;
        _dbContext = dbContext;
        _promptTemplateService = promptTemplateService;
        _logger = logger;
        _configuration = configuration.Value;
        _environment = environment;
    }

    public async Task<WaterQualityManagementPlanDocumentExtractionResultDto> ExtractFromDocument(
        int waterQualityManagementPlanDocumentID, int personID, CancellationToken cancellationToken)
    {
        var totalSw = Stopwatch.StartNew();
        _logger.LogInformation("Starting WQMP extraction for documentID={DocumentID}, personID={PersonID}, model={Model}, path={ExtractionPath}",
            waterQualityManagementPlanDocumentID, personID, _configuration.ClaudeModelId, _client.PathName);

        var domainContext = await BuildDomainContext();

        // NPT-1106 round 3: "not found" is expressed with sentinels ("" for the three strings,
        // all-zero BoundingBox) instead of nulls — strict tool use caps union-typed schema
        // parameters at 16 per request, and nullable-everywhere schemas carried 132.
        // NormalizeNotFoundSentinels converts the sentinels back to nulls post-receive, so the
        // stored ExtractionResultJson contract is unchanged.
        var evidenceInstructions =
            $"SchemaVersion: {SchemaVersion}. Use ONLY the provided WQMP PDF. Each attribute object MUST match ExtractedValueSchema. " +
            "Value = raw extracted string, or empty string \"\" when not found; ExtractionEvidence = source snippet (preceding sentence, target sentence, following sentence OR nearby table text); DocumentSource = page reference (e.g. 'Page 12'). " +
            "BoundingBox = {PageNumber, X, Y, Width, Height} locating the evidence on the page. X/Y/Width/Height are 0-1 fractions of page size where (0,0) is top-left and (1,1) is bottom-right. " +
            "ALWAYS emit a real BoundingBox whenever Value is non-empty. The rectangle should tightly cover the ACTUAL TEXT of ExtractionEvidence — measure from the top of the character baselines to the bottom, and from the first character to the last. Do NOT center the box on surrounding whitespace, adjacent paragraphs, or 'the general area'; point directly at the text characters themselves. " +
            "For scanned/rasterized pages, look at the page image and estimate from the ink positions. Use a typical line height of ~0.02–0.04 (2–4% of page height) for a single-line field value; taller for multi-line evidence. " +
            "If a field is not found in the document: set Value, ExtractionEvidence, and DocumentSource to empty strings and set every BoundingBox number (PageNumber, X, Y, Width, Height) to 0. Do not add or rename properties.\n" +
            $"ExtractedValueSchema: {ExtractedValueSchema.Value}";

        var categoryConfigs = new Dictionary<string, (PromptTemplate template, string schema, bool expectArray)>
        {
            ["WQMP"] = (PromptTemplate.ExtractWqmpFields, WqmpSchema.Value, false),
            ["Parcels"] = (PromptTemplate.ExtractParcels, ParcelSchema.Value, true),
            ["QuickBMPs"] = (PromptTemplate.ExtractQuickBMPs, QuickBmpSchema.Value, true),
            ["SourceControlBMPs"] = (PromptTemplate.ExtractSourceControlBMPs, SourceControlBmpSchema.Value, true),
        };

        // NPT-1132: failures this method recovers from silently (empty fallbacks, retries),
        // surfaced on the result so the extraction eval can count them.
        var hiccups = new System.Collections.Concurrent.ConcurrentQueue<string>();

        // NPT-1133: the client gets the document in front of Claude (Files API upload, or a temp
        // folder for the CLI); everything else here is shared by both paths.
        await using var session = await _client.OpenDocumentAsync(waterQualityManagementPlanDocumentID,
            categoryConfigs.Select(kvp => new WqmpExtractionCategory(kvp.Key, kvp.Value.schema)).ToList(), hiccups, cancellationToken);

        _logger.LogInformation("Document ready (elapsed {ElapsedMs}ms); invoking 4 category extractions via the Claude {ExtractionPath}...",
            totalSw.ElapsedMilliseconds, _client.PathName);

        // Per-category extraction. `responseStarted` is signalled when the response begins,
        // i.e. once this call's cache write is readable.
        async Task<(string output, WqmpExtractionCallUsageDto usage)> ExtractCategoryAsync(string key, PromptTemplate template, string schema, bool expectArray,
            TaskCompletionSource responseStarted = null)
        {
            var catSw = Stopwatch.StartNew();
            _logger.LogInformation("Starting extraction category: {Category}", key);

            var templateModel = new
            {
                EvidenceInstructions = evidenceInstructions,
                DomainContext = domainContext,
                ExtractedValueSchema = ExtractedValueSchema.Value,
                Schema = schema,
            };
            var call = new WqmpCategoryCall
            {
                Category = new WqmpExtractionCategory(key, schema),
                SystemPrompt = evidenceInstructions,
                DomainContext = domainContext,
                Prompt = _promptTemplateService.Render(template, templateModel),
            };

            var result = await session.ExtractCategoryAsync(call, responseStarted, cancellationToken);
            var output = result.OutputJson ?? (expectArray ? "[]" : "{}");

            if (!IsValidJson(output))
            {
                _logger.LogError("Category output was invalid JSON for {Category} after {ElapsedMs}ms. Using empty fallback.",
                    key, catSw.ElapsedMilliseconds);
                hiccups.Enqueue($"{key}: invalid JSON, empty fallback used");
                output = expectArray ? "[]" : "{}";
            }
            else
            {
                // NPT-1106 round 3: the strict schemas express "not found" as sentinels
                // ("" strings / all-zero BoundingBox) to stay under the 16-union strict
                // compilation cap. Convert them back to nulls here so the stored
                // ExtractionResultJson keeps the original null-based contract the SPA
                // review wizard and the approve endpoint consume.
                output = NormalizeNotFoundSentinels(output);
                _logger.LogInformation("Finished extraction category: {Category} in {ElapsedMs}ms ({OutputChars} chars, cached={CachedTokens})",
                    key, catSw.ElapsedMilliseconds, output.Length, result.Usage.CacheReadInputTokens);
            }

            return (output, result.Usage);
        }

        // NPT-1132: a cache entry is readable only once the response that wrote it begins
        // streaming, so four simultaneous calls each paid to cache the whole PDF. Start the first
        // category, wait until its stream begins (or it fails), then start the rest so they read
        // the PDF from the cache (0.1x) instead of writing it again (1.25x). The CLI path has no
        // shared cache to warm, so it starts all four at once.
        var firstKey = categoryConfigs.Keys.First();
        var firstStreamStarted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var firstConfig = categoryConfigs[firstKey];
        var firstTask = ExtractCategoryAsync(firstKey, firstConfig.template, firstConfig.schema, firstConfig.expectArray, firstStreamStarted);
        if (session.WarmCacheWithFirstCategory)
        {
            await Task.WhenAny(firstStreamStarted.Task, firstTask);
        }
        var tasks = new List<Task<(string output, WqmpExtractionCallUsageDto usage)>> { firstTask };
        tasks.AddRange(categoryConfigs.Where(kvp => kvp.Key != firstKey).Select(kvp =>
            ExtractCategoryAsync(kvp.Key, kvp.Value.template, kvp.Value.schema, kvp.Value.expectArray)));
        var results = await Task.WhenAll(tasks);
        var keys = categoryConfigs.Keys.ToList();
        var map = new Dictionary<string, string>();
        var callUsage = new List<WqmpExtractionCallUsageDto>();
        for (var i = 0; i < keys.Count; i++)
        {
            map[keys[i]] = results[i].output;
            callUsage.Add(results[i].usage);
            await LogTokenUsage(personID, results[i].usage.InputTokens, results[i].usage.OutputTokens,
                results[i].usage.CacheReadInputTokens, $"WQMP Extraction - {keys[i]}");
        }

        // Array categories return { "items": [...] } — unwrap to just the array.
        // NPT-1054: Claude sometimes JSON-encodes `items` as a string instead of emitting a real
        // array (observed on the SourceControlBMPs call with longer prompts + multi-line array
        // examples). Detect that shape and parse it back to an array before consolidating;
        // otherwise the downstream JSON_QUERY consumer sees a string literal where an array
        // should be.
        //
        // Returns (Output, FailedFallback). FailedFallback is true ONLY when unwrap fell back to
        // "[]" because the inner JSON couldn't be parsed; legitimate empty `items` arrays and
        // successful parses report false so callers can distinguish flaky-model failures from a
        // genuine empty result and retry accordingly.
        (string Output, bool FailedFallback) UnwrapItems(string json)
        {
            try
            {
                using var doc = JsonDocument.Parse(json);
                if (doc.RootElement.TryGetProperty("items", out var items))
                {
                    if (items.ValueKind == JsonValueKind.String)
                    {
                        var innerJson = items.GetString();
                        if (string.IsNullOrEmpty(innerJson))
                        {
                            _logger.LogWarning("Tool output `items` was an empty string — emitting empty array.");
                            return ("[]", false);
                        }
                        try
                        {
                            using var innerDoc = JsonDocument.Parse(innerJson);
                            _logger.LogWarning("Tool output had `items` as a JSON-encoded string ({Len} chars) — unwrapped successfully.", innerJson.Length);
                            return (innerJson, false);
                        }
                        catch (JsonException ex)
                        {
                            // The string content failed to parse as JSON. Try a best-effort cleanup:
                            // strip a stray trailing comma + newline before the closing bracket,
                            // which is a common Claude foible on long array outputs.
                            var cleaned = System.Text.RegularExpressions.Regex.Replace(
                                innerJson, @",\s*([\]\}])", "$1");
                            try
                            {
                                using var cleanedDoc = JsonDocument.Parse(cleaned);
                                _logger.LogWarning("Tool output `items` was a JSON-encoded string with trailing-comma issues — repaired and unwrapped ({Len} chars).", cleaned.Length);
                                return (cleaned, false);
                            }
                            catch (JsonException ex2)
                            {
                                // NPT-1054 diagnostics: the head-only truncation was masking the actual
                                // corruption point in long outputs. Capture head/tail/window-around-error
                                // and persist the full failing content to /tmp so we can repair offline.
                                static string Slice(string s, int start, int len)
                                    => start < 0 || start >= s.Length ? "" : s.Substring(start, Math.Min(len, s.Length - start));

                                // ex.BytePositionInLine is per-line; compute an approximate absolute offset
                                // by walking lines to ex.LineNumber, then add the in-line position.
                                int approxOffset = -1;
                                if (ex.LineNumber.HasValue)
                                {
                                    long line = 0, offset = 0;
                                    foreach (var ch in innerJson)
                                    {
                                        if (line == ex.LineNumber.Value) break;
                                        if (ch == '\n') line++;
                                        offset++;
                                    }
                                    approxOffset = (int)Math.Min(offset + (ex.BytePositionInLine ?? 0), innerJson.Length - 1);
                                }
                                var windowStart = Math.Max(0, approxOffset - 200);
                                var windowAround = approxOffset >= 0 ? Slice(innerJson, windowStart, 400) : "(no position)";

                                // The /tmp dump is a dev-only diagnostic. WQMP content can include
                                // facility addresses and contacts; QA/prod pods would also accumulate
                                // dumps on a long-lived volume with no retention. Gating to
                                // Development keeps the artifact useful for local repro without
                                // leaking content or filling shared disk in deployed envs.
                                string dumpPath = null;
                                if (_environment.IsDevelopment())
                                {
                                    try
                                    {
                                        dumpPath = Path.Combine(Path.GetTempPath(), $"wqmp-sc-extraction-failure-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid():N}.json");
                                        File.WriteAllText(dumpPath, innerJson);
                                    }
                                    catch (Exception dumpEx)
                                    {
                                        _logger.LogWarning(dumpEx, "Failed to write extraction-failure dump file");
                                    }
                                }

                                _logger.LogError(ex2,
                                    "Tool output `items` was a JSON-encoded string but failed to parse even after cleanup. " +
                                    "First error: {FirstErr}. Cleanup error: {CleanupErr}. Approx offset: {Offset} of {Len}. " +
                                    "Dump file: {DumpPath}. Head: {Head} | Window around error: {Window} | Tail: {Tail}",
                                    ex.Message, ex2.Message, approxOffset, innerJson.Length, dumpPath ?? "(disabled)",
                                    Slice(innerJson, 0, 400),
                                    windowAround,
                                    Slice(innerJson, Math.Max(0, innerJson.Length - 400), 400));
                                return ("[]", true);
                            }
                        }
                    }
                    return (items.GetRawText(), false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UnwrapItems failed to parse tool output JSON; using raw output.");
            }
            return (json, false);
        }

        // NPT-1054: retry SC once when UnwrapItems falls back to "[]" due to a corrupt inner JSON
        // (the flaky-model failure mode that captures 25k–30k char tool outputs but emits a
        // double-encoded `items` string with malformed content the cleanup can't repair). One
        // retry catches the typical "rolled bad dice" case; legitimate empty arrays don't retry.
        var parcelsUnwrap = UnwrapItems(map["Parcels"]);
        var quickBmpsUnwrap = UnwrapItems(map["QuickBMPs"]);
        var scUnwrap = UnwrapItems(map["SourceControlBMPs"]);
        if (parcelsUnwrap.FailedFallback) hiccups.Enqueue("Parcels: malformed items, empty fallback used");
        if (quickBmpsUnwrap.FailedFallback) hiccups.Enqueue("QuickBMPs: malformed items, empty fallback used");
        if (scUnwrap.FailedFallback)
        {
            _logger.LogWarning("SourceControlBMPs unwrap failed — retrying extraction once before giving up.");
            hiccups.Enqueue("SourceControlBMPs: malformed items, retried");
            try
            {
                var scConfig = categoryConfigs["SourceControlBMPs"];
                var scRetry = await ExtractCategoryAsync("SourceControlBMPs", scConfig.template, scConfig.schema, scConfig.expectArray);
                scRetry.usage.Category = "SourceControlBMPs (retry)";
                callUsage.Add(scRetry.usage);
                await LogTokenUsage(personID, scRetry.usage.InputTokens, scRetry.usage.OutputTokens, scRetry.usage.CacheReadInputTokens,
                    "WQMP Extraction - SourceControlBMPs (retry)");
                map["SourceControlBMPs"] = scRetry.output;
                var scRetryUnwrap = UnwrapItems(scRetry.output);
                if (!scRetryUnwrap.FailedFallback)
                {
                    _logger.LogInformation("SourceControlBMPs retry succeeded ({Chars} chars).", scRetryUnwrap.Output.Length);
                    scUnwrap = scRetryUnwrap;
                }
                else
                {
                    _logger.LogWarning("SourceControlBMPs retry also produced a malformed payload; staying with empty fallback.");
                    hiccups.Enqueue("SourceControlBMPs: retry also malformed, empty fallback used");
                }
            }
            catch (Exception retryEx)
            {
                _logger.LogError(retryEx, "SourceControlBMPs retry threw before completing; staying with empty fallback.");
                hiccups.Enqueue($"SourceControlBMPs: retry threw {retryEx.GetType().Name}, empty fallback used");
            }
        }

        var parcelsOutput = await NormalizeParcelNumbersAsync(parcelsUnwrap.Output);

        var finalOutput = $"{{ \"SchemaVersion\": \"{SchemaVersion}\", \"WQMP\": {map["WQMP"]}, \"Parcels\": {parcelsOutput}, \"QuickBMPs\": {quickBmpsUnwrap.Output}, \"SourceControlBMPs\": {scUnwrap.Output} }}";

        if (!IsValidJson(finalOutput))
        {
            _logger.LogError("Final consolidated JSON is invalid. Using raw parts.");
        }

        _logger.LogInformation("Completed WQMP extraction for documentID={DocumentID} in {ElapsedMs}ms",
            waterQualityManagementPlanDocumentID, totalSw.ElapsedMilliseconds);

        return new WaterQualityManagementPlanDocumentExtractionResultDto
        {
            FinalOutput = finalOutput,
            RawResults = string.Join("\n", map.Select(kvp => $"{kvp.Key}: {kvp.Value}")),
            ExtractedAt = DateTime.UtcNow,
            CallUsage = callUsage,
            Hiccups = hiccups.ToList(),
        };
    }

    /// <summary>
    /// NPT-1132: rewrites each extracted APN to the Parcel table's stored form. The review wizard
    /// saves parcels through an exact-string lookup, so an APN copied as "691-101-001" or
    /// "69110101" never matched "691-101-01" (WQMP 1419 lost all 7 parcels that way). Values with
    /// no match are left as extracted. Any parse problem returns the input unchanged.
    /// </summary>
    private async Task<string> NormalizeParcelNumbersAsync(string parcelsJson)
    {
        try
        {
            if (JsonNode.Parse(parcelsJson) is not JsonArray items) return parcelsJson;
            var valueNodes = items
                .Select(item => item?["ParcelNumber"] as JsonObject)
                .Where(x => x?["Value"] is JsonValue)
                .ToList();
            var raw = valueNodes.Select(x => x!["Value"]!.GetValue<string>()).ToList();
            if (raw.Count == 0) return parcelsJson;

            var canonical = await Parcels.CanonicalizeParcelNumbersAsync(_dbContext, raw);
            foreach (var node in valueNodes)
            {
                var value = node!["Value"]!.GetValue<string>();
                if (canonical.TryGetValue(value, out var stored) && stored != value)
                {
                    node["Value"] = stored;
                }
            }
            return items.ToJsonString();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Parcel number normalization failed; keeping APNs as extracted.");
            return parcelsJson;
        }
    }

    private async Task LogTokenUsage(int personID, long inputTokens, long outputTokens, long cachedTokens, string context)
    {
        try
        {
            _dbContext.AITokenUsages.Add(new AITokenUsage
            {
                PersonID = personID,
                Model = _configuration.ClaudeModelId,
                InputTokens = (int)inputTokens,
                CachedInputTokens = (int)cachedTokens,
                OutputTokens = (int)outputTokens,
                RequestDate = DateTime.UtcNow,
                // NPT-1133: CLI-path calls spend the developer's Claude plan, not API credits.
                RequestContext = _client.PathName == "API" ? context : $"{context} ({_client.PathName})"
            });
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to log token usage for {Context}", context);
        }
    }

    public async Task<string> BuildDomainContext()
    {
        var domainTables = new
        {
            Jurisdictions = await _dbContext.StormwaterJurisdictions.Include(x => x.Organization).Select(x => x.Organization.OrganizationName).AsNoTracking().ToListAsync(),
            TreatmentBMPTypes = await _dbContext.TreatmentBMPTypes.Select(x => x.TreatmentBMPTypeName).AsNoTracking().ToListAsync(),
            HydrologicSubareas = await _dbContext.HydrologicSubareas.Select(x => x.HydrologicSubareaName).AsNoTracking().ToListAsync(),
            WaterQualityManagementPlanLandUse = WaterQualityManagementPlanLandUse.All.Select(x => x.WaterQualityManagementPlanLandUseDisplayName),
            WaterQualityManagementPlanPriority = WaterQualityManagementPlanPriority.All.Select(x => x.WaterQualityManagementPlanPriorityDisplayName),
            WaterQualityManagementPlanStatus = WaterQualityManagementPlanStatus.All.Select(x => x.WaterQualityManagementPlanStatusDisplayName),
            WaterQualityManagementPlanDevelopmentType = WaterQualityManagementPlanDevelopmentType.All.Select(x => x.WaterQualityManagementPlanDevelopmentTypeDisplayName),
            WaterQualityManagementPlanPermitTerm = WaterQualityManagementPlanPermitTerm.All.Select(x => x.WaterQualityManagementPlanPermitTermDisplayName),
            TrashCaptureStatusType = TrashCaptureStatusType.All.Select(x => x.TrashCaptureStatusTypeDisplayName),
            // NPT-1132: was missing, so the model answered "Yes"/"No", which the review wizard
            // can't map to Applicable/Exempt; the field was always left blank (0% in the eval).
            HydromodificationAppliesType = HydromodificationAppliesType.All.Select(x => x.HydromodificationAppliesTypeDisplayName),
            // NPT-1054: send SC attribute names grouped by their category so Claude has the
            // three-bucket taxonomy when matching extracted attribute wording. The v3 prompt
            // explicitly enumerates the three categories with examples — the categorized
            // DomainContext lets Claude resolve fuzzy PDF wording against the right bucket.
            SourceControlBMPAttributes = (await _dbContext.SourceControlBMPAttributes
                .AsNoTracking()
                .Select(x => new { x.SourceControlBMPAttributeName, x.SourceControlBMPAttributeCategoryID })
                .ToListAsync())
                .GroupBy(x => x.SourceControlBMPAttributeCategoryID)
                .Select(g => new
                {
                    Category = SourceControlBMPAttributeCategory.AllLookupDictionary[g.Key].SourceControlBMPAttributeCategoryName,
                    Attributes = g.Select(a => a.SourceControlBMPAttributeName).OrderBy(n => n).ToList(),
                })
                .OrderBy(g => g.Category)
                .ToList(),
        };
        return $"SCHEMA_VERSION: {SchemaVersion}\nDOMAIN TABLES: {JsonSerializer.Serialize(domainTables)}\n";
    }

    private static bool IsValidJson(string candidate)
    {
        if (string.IsNullOrWhiteSpace(candidate)) return false;
        try { using var _ = JsonDocument.Parse(candidate); return true; } catch { return false; }
    }

    // NPT-1106 round 3: the strict tool schemas can't use nullable/union types (Anthropic caps
    // union-typed parameters at 16 per request; nullable-everywhere carried 132), so "not
    // found" arrives as sentinels: empty strings for Value/ExtractionEvidence/DocumentSource
    // and an all-zero BoundingBox (PageNumber 0). This walks the category output and restores
    // the null-based contract every downstream consumer (SPA review wizard, approve endpoint,
    // stored ExtractionResultJson) was built against. Defensive: any parse hiccup returns the
    // input unchanged — the caller already validated it as JSON.
    public static string NormalizeNotFoundSentinels(string json)
    {
        try
        {
            var root = JsonNode.Parse(json);
            if (root == null) return json;
            NormalizeNode(root);
            return root.ToJsonString();
        }
        catch (Exception)
        {
            return json;
        }
    }

    private static void NormalizeNode(JsonNode node)
    {
        switch (node)
        {
            case JsonObject obj when obj.ContainsKey("Value") && obj.ContainsKey("BoundingBox"):
                // ExtractedValue shape — apply the sentinel conversions and stop descending.
                foreach (var key in new[] { "Value", "ExtractionEvidence", "DocumentSource" })
                {
                    if (obj[key] is JsonValue v && v.TryGetValue<string>(out var s) && string.IsNullOrWhiteSpace(s))
                    {
                        obj[key] = null;
                    }
                }
                if (obj["BoundingBox"] is JsonObject boundingBox
                    && boundingBox["PageNumber"] is JsonValue pageNumber
                    && pageNumber.TryGetValue<double>(out var page) && page < 1)
                {
                    obj["BoundingBox"] = null;
                }
                break;
            case JsonObject obj:
                foreach (var property in obj.ToList())
                {
                    if (property.Value != null) NormalizeNode(property.Value);
                }
                break;
            case JsonArray array:
                foreach (var item in array.ToList())
                {
                    if (item != null) NormalizeNode(item);
                }
                break;
        }
    }

    // NPT-1106 round 3: no union types anywhere in these schemas. Strict tool use compiles
    // schemas into grammars server-side and caps union-typed parameters (type arrays / anyOf)
    // at 16 per request; the previous nullable-everywhere shape carried 132 across the four
    // tools and every extraction 400'd. "Not found" is now an empty string ("") for the three
    // strings and an all-zero BoundingBox — NormalizeNotFoundSentinels restores nulls
    // post-receive so downstream consumers see the original contract.
    private static object ExtractedValueProp(string description) => new
    {
        type = "object",
        description,
        properties = new
        {
            Value = new { type = "string", description = "Raw extracted value; empty string when not found." },
            ExtractionEvidence = new { type = "string", description = "Source snippet; empty string when not found." },
            DocumentSource = new { type = "string", description = "Page reference; empty string when not found." },
            BoundingBox = BoundingBoxProp()
        },
        required = new[] { "Value", "ExtractionEvidence", "DocumentSource", "BoundingBox" },
        additionalProperties = false
    };

    // Spatial hint for the evidence region on the page. Normalized 0-1 fractions relative
    // to the page's own width/height, top-left origin. Claude should emit a real box whenever
    // Value is non-empty — a rough estimate is more useful than none. All-zero (PageNumber 0)
    // is the not-found sentinel; NormalizeNotFoundSentinels converts it back to null.
    private static object BoundingBoxProp() => new
    {
        type = "object",
        description = "Required {PageNumber, X, Y, Width, Height} locating the evidence on its page. X/Y/Width/Height are 0-1 fractions of page size (top-left origin). Set every number to 0 only when Value is empty (field not found).",
        properties = new
        {
            PageNumber = new { type = "integer", description = "1-based page index; 0 when the field was not found." },
            X = new { type = "number", description = "Left edge, fraction of page width." },
            Y = new { type = "number", description = "Top edge, fraction of page height." },
            Width = new { type = "number", description = "Width, fraction of page width." },
            Height = new { type = "number", description = "Height, fraction of page height." }
        },
        required = new[] { "PageNumber", "X", "Y", "Width", "Height" },
        additionalProperties = false
    };

    // Schema builders — unchanged from OpenAI implementation (same JSON shape)

    private static string BuildExtractedValueJsonSchema()
    {
        var schema = new
        {
            type = "object",
            description = "ExtractedValue schema. Attribute with evidence.",
            properties = new
            {
                Value = new { type = "string", description = "Raw extracted value; empty string when not found." },
                ExtractionEvidence = new { type = "string", description = "Snippet: preceding, target, following sentence OR nearby table text; empty string when not found." },
                DocumentSource = new { type = "string", description = "Page reference (e.g. 'Page 12'); empty string when not found." },
                BoundingBox = BoundingBoxProp()
            },
            required = new[] { "Value", "ExtractionEvidence", "DocumentSource", "BoundingBox" },
            additionalProperties = false
        };
        return JsonSerializer.Serialize(schema);
    }

    private static string BuildWqmpSchemaJson()
    {
        var properties = new Dictionary<string, object>
        {
            ["WaterQualityManagementPlanName"] = ExtractedValueProp("Title of the WQMP."),
            ["Jurisdiction"] = ExtractedValueProp("Jurisdiction responsible."),
            ["WaterQualityManagementPlanLandUse"] = ExtractedValueProp("Land use classification."),
            ["WaterQualityManagementPlanPriority"] = ExtractedValueProp("Priority category."),
            ["WaterQualityManagementPlanStatus"] = ExtractedValueProp("Current status."),
            ["WaterQualityManagementPlanDevelopmentType"] = ExtractedValueProp("Development type."),
            ["ApprovalDate"] = ExtractedValueProp("Date the reviewing agency approved the WQMP (approval stamp, signature block or an 'Approval Date' field). Not a prepared, revised, updated, submitted, received or review-completed date."),
            ["MaintenanceContactName"] = ExtractedValueProp("Maintenance contact or owner name."),
            ["MaintenanceContactOrganization"] = ExtractedValueProp("Maintenance contact or owner organization."),
            ["MaintenanceContactPhone"] = ExtractedValueProp("Maintenance contact phone."),
            ["MaintenanceContactAddress1"] = ExtractedValueProp("Address line 1."),
            ["MaintenanceContactAddress2"] = ExtractedValueProp("Address line 2."),
            ["MaintenanceContactCity"] = ExtractedValueProp("Address city."),
            ["MaintenanceContactState"] = ExtractedValueProp("Address state."),
            ["MaintenanceContactZip"] = ExtractedValueProp("Address ZIP."),
            ["WaterQualityManagementPlanPermitTerm"] = ExtractedValueProp("Permit term."),
            ["DateOfConstruction"] = ExtractedValueProp("Construction completion date."),
            ["HydrologicSubarea"] = ExtractedValueProp("Hydrologic subarea."),
            ["RecordNumber"] = ExtractedValueProp("Agency record number."),
            ["RecordedWQMPAreaInAcres"] = ExtractedValueProp("Area in acres."),
            ["TrashCaptureStatusType"] = ExtractedValueProp("Trash capture status under the State Trash Provisions, as one of the TrashCaptureStatusType names in DomainContext. Catch basin / curb inlet / trench drain inserts are Partial unless the plan describes them as full capture; trash enclosures and standalone treatment BMPs are not trash capture."),
            ["HydromodificationAppliesType"] = ExtractedValueProp("Whether hydromodification (hydrologic conditions of concern) controls apply, as one of the HydromodificationAppliesType names in DomainContext.")
        };
        var schema = new
        {
            type = "object",
            description = "WQMP root schema (uses ExtractedValue objects).",
            properties,
            required = properties.Keys.ToArray(),
            additionalProperties = false
        };
        return JsonSerializer.Serialize(schema);
    }

    // NPT-1106 round 3: the array-category schemas define ExtractedValue ONCE in $defs and
    // $ref it per field. Strict grammar compilation counts every inlined object — the three
    // array tools inlined (11 ExtractedValues) exceeded the "compiled grammar is too large"
    // cap (QuickBMPs' 7 alone did), while the identical shapes with $refs compile fine
    // (verified against the live API). Per-field guidance rides as a sibling `description`
    // next to each $ref, which the strict validator accepts.
    private static string WrapAsArraySchema(string description, object itemSchema)
    {
        var schema = new Dictionary<string, object>
        {
            ["type"] = "object",
            ["description"] = description,
            ["$defs"] = new Dictionary<string, object>
            {
                ["ExtractedValue"] = ExtractedValueProp("Attribute with evidence.")
            },
            ["properties"] = new Dictionary<string, object>
            {
                ["items"] = new { type = "array", items = itemSchema }
            },
            ["required"] = new[] { "items" },
            ["additionalProperties"] = false
        };
        return JsonSerializer.Serialize(schema);
    }

    // Field property for array-category item schemas: a $ref to the shared ExtractedValue
    // definition plus the per-field guidance as a sibling description.
    private static Dictionary<string, object> ExtractedValueRef(string description) => new()
    {
        ["$ref"] = "#/$defs/ExtractedValue",
        ["description"] = description
    };

    private static object BuildParcelItemSchema()
    {
        var properties = new Dictionary<string, object>
        {
            ["ParcelNumber"] = ExtractedValueRef("APN (e.g. XXX-XX-XXX or XXX-XXX-XX)")
        };
        return new
        {
            type = "object",
            properties,
            required = properties.Keys.ToArray(),
            additionalProperties = false
        };
    }

    private static string BuildParcelSchemaJson() =>
        WrapAsArraySchema("Array of parcels (ExtractedValue objects).", BuildParcelItemSchema());

    // Targets QuickBMP (Simplified Structural BMP) records, which are jurisdiction-scoped
    // to a single WQMP — distinct from the global TreatmentBMP inventory. Field names match
    // QuickBMPUpsertDto so the approve endpoint can drop accepted entries straight into the
    // Merge call. Extraction of the global TreatmentBMP inventory is a future story.
    private static object BuildQuickBmpItemSchema()
    {
        var properties = new Dictionary<string, object>
        {
            ["QuickBMPName"] = ExtractedValueRef("Simple BMP name as written in the document."),
            ["TreatmentBMPType"] = ExtractedValueRef("BMP type/classification name; should match a TreatmentBMPType from DomainContext when possible."),
            ["NumberOfIndividualBMPs"] = ExtractedValueRef("Count of individual physical BMP units this row represents (default 1 if not stated)."),
            ["PercentOfSiteTreated"] = ExtractedValueRef("% of the WQMP site this BMP treats (0-100)."),
            ["PercentCaptured"] = ExtractedValueRef("% of design storm captured by this BMP (0-100)."),
            ["PercentRetained"] = ExtractedValueRef("% of design storm retained on-site (0-100; must be <= PercentCaptured)."),
            ["QuickBMPNote"] = ExtractedValueRef("Free-form note about this BMP (≤200 chars).")
        };
        return new
        {
            type = "object",
            properties,
            required = properties.Keys.ToArray(),
            additionalProperties = false
        };
    }

    private static string BuildQuickBmpSchemaJson() =>
        WrapAsArraySchema("Array of QuickBMPs (Simplified Structural BMPs, ExtractedValue objects).", BuildQuickBmpItemSchema());

    private static object BuildSourceControlBmpItemSchema()
    {
        var properties = new Dictionary<string, object>
        {
            ["SourceControlBMPAttribute"] = ExtractedValueRef("Source control attribute name."),
            ["IsPresent"] = ExtractedValueRef("Indicates presence (Yes/No)."),
            ["SourceControlBMPNote"] = ExtractedValueRef("Attribute notes.")
        };
        return new
        {
            type = "object",
            properties,
            required = properties.Keys.ToArray(),
            additionalProperties = false
        };
    }

    private static string BuildSourceControlBmpSchemaJson() =>
        WrapAsArraySchema("Array of source control BMPs (ExtractedValue objects).", BuildSourceControlBmpItemSchema());
}
