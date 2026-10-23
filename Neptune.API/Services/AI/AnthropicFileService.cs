using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Neptune.API.Services;
using Neptune.Common.Services;
using Neptune.EFModels.Entities;

namespace Neptune.API.Services.AI;

/// <summary>
/// Uploads PDFs to Anthropic's Files API and caches the resulting <c>file_id</c> on the
/// owning <see cref="WaterQualityManagementPlanDocument"/> so subsequent extraction and
/// chat calls can reference the same uploaded document without re-uploading.
///
/// The Files-API source path on the Beta Messages API has a 500 MB ceiling per Anthropic
/// (vs. 32 MB on the URL-source path we used previously), which lifts the constraint that
/// rejected ~50% of real-world scanned WQMPs. NPT-1044.
/// </summary>
public class AnthropicFileService
{
    private const string AnthropicFilesUrl = "https://api.anthropic.com/v1/files";

    /// <summary>
    /// Ceiling on the best-effort cleanup DELETE in <see cref="DeleteRemoteFileAsync"/>.
    /// Short on purpose: it runs inline on user-facing delete and file-swap requests, and
    /// abandoning an orphaned file is much cheaper than making someone wait on it.
    /// </summary>
    private static readonly TimeSpan DeleteTimeout = TimeSpan.FromSeconds(10);

    // Serializes upload attempts per documentID so concurrent callers (extract+chat
    // hitting the same doc, or two users on the same doc) don't both upload the same
    // PDF when the cache is empty. Same gate covers the stale-id refresh path so
    // refresh and ensure can't deadlock waiting on each other. Static — the file
    // service is scoped, but races span requests/replicas (within a replica, anyway).
    private static readonly ConcurrentDictionary<int, SemaphoreSlim> UploadGates = new();

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly WqmpPdfBlobReader _pdfBlobReader;
    private readonly NeptuneDbContext _dbContext;
    private readonly ILogger<AnthropicFileService> _logger;
    private readonly NeptuneConfiguration _configuration;

    public AnthropicFileService(
        IHttpClientFactory httpClientFactory,
        WqmpPdfBlobReader pdfBlobReader,
        NeptuneDbContext dbContext,
        ILogger<AnthropicFileService> logger,
        IOptions<NeptuneConfiguration> configuration)
    {
        _httpClientFactory = httpClientFactory;
        _pdfBlobReader = pdfBlobReader;
        _dbContext = dbContext;
        _logger = logger;
        _configuration = configuration.Value;
    }

    private sealed class FileUploadResponse
    {
        [JsonPropertyName("id")] public string Id { get; set; }
        [JsonPropertyName("filename")] public string Filename { get; set; }
        [JsonPropertyName("size_bytes")] public long SizeBytes { get; set; }
    }

    /// <summary>
    /// Thrown by <see cref="EnsureUploadedFileIDAsync"/> when the document's PDF exceeds
    /// <see cref="NeptuneConfiguration.MaxExtractablePdfSizeBytes"/>. Controllers should
    /// pre-check size for a clean 400 response; this exception is the defense-in-depth
    /// guard so we never spend bandwidth pushing an oversized blob to Anthropic only
    /// to be rejected at the upstream.
    /// </summary>
    public sealed class PdfTooLargeForUploadException : InvalidOperationException
    {
        public long ContentLengthBytes { get; }
        public long MaxBytes { get; }
        public PdfTooLargeForUploadException(long contentLengthBytes, long maxBytes)
            : base($"PDF is {contentLengthBytes / (1024.0 * 1024.0):F0} MB; the AI extraction upload cap is {maxBytes / (1024.0 * 1024.0):F0} MB.")
        {
            ContentLengthBytes = contentLengthBytes;
            MaxBytes = maxBytes;
        }
    }

    /// <summary>
    /// Thrown when the Files API rejects an upload. The upload goes through a raw HttpClient
    /// (not the SDK), so this carries the HTTP status that the SDK's typed exceptions would —
    /// <see cref="AnthropicAccountIssue"/> uses it to tell account-level failures (bad key,
    /// no access, billing) from per-document ones. The message keeps the response body so
    /// the controller can pull out Anthropic's readable <c>error.message</c>.
    /// </summary>
    public sealed class AnthropicFileUploadException : InvalidOperationException
    {
        public int StatusCode { get; }
        public AnthropicFileUploadException(int statusCode, string body)
            : base($"Anthropic Files API upload returned {statusCode}: {body}")
        {
            StatusCode = statusCode;
        }
    }

    /// <summary>
    /// Returns the cached Anthropic <c>file_id</c> for a document, uploading the PDF
    /// once if not already cached. Idempotent — repeated calls for the same document
    /// return the same id without re-uploading. Concurrent callers for the same doc
    /// are serialized so only one upload occurs.
    /// </summary>
    public async Task<string> EnsureUploadedFileIDAsync(
        int waterQualityManagementPlanDocumentID, CancellationToken cancellationToken)
    {
        var document = await _dbContext.WaterQualityManagementPlanDocuments
            .Include(x => x.FileResource)
            .SingleAsync(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID, cancellationToken);

        if (!string.IsNullOrEmpty(document.AnthropicFileID))
        {
            _logger.LogInformation("Anthropic file cache hit for documentID={DocumentID}, fileID={FileID}",
                waterQualityManagementPlanDocumentID, document.AnthropicFileID);
            return document.AnthropicFileID;
        }

        // Defense-in-depth: refuse to spend memory + bandwidth uploading a PDF that
        // Anthropic will reject anyway. Controllers pre-check this for a friendly 400;
        // this guard catches any caller that bypasses the controller pre-check.
        if (document.FileResource.ContentLength > _configuration.MaxExtractablePdfSizeBytes)
        {
            throw new PdfTooLargeForUploadException(
                document.FileResource.ContentLength, _configuration.MaxExtractablePdfSizeBytes);
        }

        // Serialize concurrent uploads of the same document. Without this, two callers
        // observing AnthropicFileID == null both proceed to upload, resulting in two
        // file_ids on Anthropic (last-write-wins persisted, the loser orphans).
        var gate = UploadGates.GetOrAdd(waterQualityManagementPlanDocumentID, _ => new SemaphoreSlim(1, 1));
        await gate.WaitAsync(cancellationToken);
        try
        {
            return await UploadAndCacheAsync(document, cancellationToken);
        }
        finally
        {
            gate.Release();
        }
    }

    /// <summary>
    /// Uploads the document and persists the resulting file_id. Caller is responsible
    /// for holding the per-document gate from <see cref="UploadGates"/>. Re-checks the
    /// cached id on entry so a concurrent caller that won the race doesn't trigger a
    /// duplicate upload.
    /// </summary>
    private async Task<string> UploadAndCacheAsync(
        WaterQualityManagementPlanDocument document, CancellationToken cancellationToken)
    {
        // Re-check inside the gate. The cheap-path read in the public entry point
        // happens outside the lock, so by the time we acquire it another caller may
        // have already populated the id.
        await _dbContext.Entry(document).ReloadAsync(cancellationToken);
        if (!string.IsNullOrEmpty(document.AnthropicFileID))
        {
            _logger.LogInformation("Anthropic file populated by concurrent caller for documentID={DocumentID}, fileID={FileID}",
                document.WaterQualityManagementPlanDocumentID, document.AnthropicFileID);
            return document.AnthropicFileID;
        }

        _logger.LogInformation("Anthropic file cache miss for documentID={DocumentID} — uploading to Files API...",
            document.WaterQualityManagementPlanDocumentID);

        // Bypass the SDK for upload. Anthropic.SDK 12.17.0's Beta.Files.Upload still
        // throws AnthropicIOException with inner ObjectDisposedException on real
        // payloads — we hit it with both byte[] and Azure streaming inputs. The bug
        // is internal SDK body-handling; documented in the NPT-1044 card with the
        // raw HttpClient fallback we're using here. Extraction and chat (which
        // reference the file_id, not upload it) keep using the SDK normally.
        //
        // Stream straight from blob storage instead of materializing the PDF as
        // byte[]. The earlier byte[] buffer was a workaround for the SDK encoder
        // refusing chunked length-unknown streams; on raw HttpClient we set the
        // inner part's Content-Length explicitly from FileResource.ContentLength,
        // so the multipart envelope is well-formed without a server-side buffer.
        // Cuts peak memory from O(file size) to a small read window per upload.
        // Skips any junk ahead of the %PDF signature (NPT-1120).
        using var pdf = await _pdfBlobReader.OpenAsync(document, cancellationToken);

        var filename = document.FileResource.GetOriginalCompleteFileName();
        if (string.IsNullOrWhiteSpace(filename))
        {
            filename = $"{document.WaterQualityManagementPlanDocumentID}.pdf";
        }

        var fileID = await UploadViaHttpClientAsync(pdf.Content, pdf.ContentLength, filename, cancellationToken);

        document.AnthropicFileID = fileID;
        document.AnthropicFileUploadedDate = DateTime.UtcNow;
        await _dbContext.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Uploaded documentID={DocumentID} to Anthropic Files API, cached fileID={FileID}",
            document.WaterQualityManagementPlanDocumentID, fileID);

        return fileID;
    }

    private async Task<string> UploadViaHttpClientAsync(
        Stream pdfStream, long contentLength, string filename, CancellationToken cancellationToken)
    {
        using var client = _httpClientFactory.CreateClient();
        client.Timeout = TimeSpan.FromMinutes(5);

        using var form = new MultipartFormDataContent();
        var fileContent = new StreamContent(pdfStream);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
        // Explicit Content-Length on the inner part lets the multipart encoder finalize
        // a deterministic outer Content-Length — the previous failure mode with chunked
        // Azure streams was the encoder couldn't compute total length without it.
        fileContent.Headers.ContentLength = contentLength;
        form.Add(fileContent, "file", filename);

        using var request = new HttpRequestMessage(HttpMethod.Post, AnthropicFilesUrl) { Content = form };
        request.Headers.Add("x-api-key", _configuration.AnthropicApiKey);
        request.Headers.Add("anthropic-version", "2023-06-01");
        request.Headers.Add("anthropic-beta", "files-api-2025-04-14");

        using var response = await client.SendAsync(request, cancellationToken);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError("Anthropic Files API upload failed (status={Status}): {Body}", (int)response.StatusCode, body);
            throw new AnthropicFileUploadException((int)response.StatusCode, body);
        }

        var parsed = JsonSerializer.Deserialize<FileUploadResponse>(body)
                     ?? throw new InvalidOperationException("Anthropic Files API returned an empty response body.");
        if (string.IsNullOrEmpty(parsed.Id))
        {
            throw new InvalidOperationException(
                $"Anthropic Files API response missing 'id' field: {body}");
        }
        return parsed.Id;
    }

    /// <summary>
    /// Recovers from a stale-<c>file_id</c> 404 by invalidating the cached id and
    /// re-uploading. Pass the id you observed the 404 against as <paramref name="staleFileID"/>;
    /// if another concurrent caller already refreshed past it (different id now in the DB),
    /// this returns that fresher id without re-uploading. Serialized per documentID via
    /// the same gate as <see cref="EnsureUploadedFileIDAsync"/> so the 4 parallel
    /// extraction calls don't each upload their own copy and so refresh and ensure
    /// can't deadlock on each other.
    /// </summary>
    public async Task<string> RefreshFileIDAsync(
        int waterQualityManagementPlanDocumentID, string staleFileID, CancellationToken cancellationToken)
    {
        var gate = UploadGates.GetOrAdd(waterQualityManagementPlanDocumentID, _ => new SemaphoreSlim(1, 1));
        await gate.WaitAsync(cancellationToken);
        try
        {
            var document = await _dbContext.WaterQualityManagementPlanDocuments
                .Include(x => x.FileResource)
                .SingleAsync(x => x.WaterQualityManagementPlanDocumentID == waterQualityManagementPlanDocumentID, cancellationToken);

            // Already-refreshed check: another caller may have re-uploaded while we
            // were queued on the gate. If the persisted id has moved on from the one
            // we 404'd against, just use it.
            if (!string.IsNullOrEmpty(document.AnthropicFileID) && document.AnthropicFileID != staleFileID)
            {
                _logger.LogInformation("Anthropic file cache already refreshed for documentID={DocumentID} (was {Stale}, now {Current}); skipping re-upload.",
                    waterQualityManagementPlanDocumentID, staleFileID, document.AnthropicFileID);
                return document.AnthropicFileID;
            }

            // Clear the stale id and force the upload path. UploadAndCacheAsync expects
            // the caller to hold the gate, which we do.
            document.AnthropicFileID = null;
            document.AnthropicFileUploadedDate = null;
            await _dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogWarning("Invalidated Anthropic file cache for documentID={DocumentID} prior to refresh.",
                waterQualityManagementPlanDocumentID);

            return await UploadAndCacheAsync(document, cancellationToken);
        }
        finally
        {
            gate.Release();
        }
    }

    /// <summary>
    /// Best-effort delete of a remote Anthropic upload. Call this whenever a cached
    /// <c>file_id</c> stops being referenced — the document row is deleted, or its
    /// PDF is replaced — otherwise the upload is orphaned on the account permanently
    /// and nothing ever reclaims it (NPT-1121).
    ///
    /// Never throws. A leftover remote file is not worth failing (or masking) the
    /// caller's operation over — same posture as
    /// <c>GDALAPIService.DeleteStagedBlobs</c>. Callers should invoke this *after*
    /// the database change commits, so a cleanup failure can't leave the row
    /// pointing at a file we already deleted.
    ///
    /// Do not call this on the stale-id 404 path in <see cref="RefreshFileIDAsync"/>:
    /// upstream has already reported the file missing, so there is nothing to delete
    /// and the extra round trip only adds latency to an error path.
    /// </summary>
    public async Task DeleteRemoteFileAsync(string fileID, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(fileID))
        {
            return;
        }

        // NPT-1133: on the CLI path there is no key, and the file_ids in a restored QA/prod
        // database belong to the shared account, so local development must not delete them.
        if (_configuration.ClaudeCliEnabled == true)
        {
            _logger.LogInformation("Claude CLI path: leaving Anthropic file {FileID} in place (local development never deletes shared-account uploads).", fileID);
            return;
        }

        try
        {
            // Raw HttpClient rather than the SDK, matching the upload path — see the
            // NPT-1044 note in UploadAndCacheAsync.
            using var client = _httpClientFactory.CreateClient();
            // Callers pass CancellationToken.None so a client disconnect can't strand the
            // file, which means nothing else bounds this wait. Without an explicit timeout
            // an unreachable Anthropic would hang every document delete and file swap for
            // HttpClient's 100s default. Cleanup is best-effort, so give up quickly and
            // leave the orphan behind rather than making the user wait on it.
            client.Timeout = DeleteTimeout;
            // fileID is Anthropic-generated and read back from the database; escape it
            // rather than trusting that round trip to keep it URL-safe.
            using var request = new HttpRequestMessage(
                HttpMethod.Delete, $"{AnthropicFilesUrl}/{Uri.EscapeDataString(fileID)}");
            request.Headers.Add("x-api-key", _configuration.AnthropicApiKey);
            request.Headers.Add("anthropic-version", "2023-06-01");
            request.Headers.Add("anthropic-beta", "files-api-2025-04-14");

            using var response = await client.SendAsync(request, cancellationToken);

            // Already gone upstream is the outcome we wanted, not a failure.
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _logger.LogInformation("Anthropic file {FileID} was already absent upstream; nothing to delete.", fileID);
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                _logger.LogWarning("Failed to delete Anthropic file {FileID} (status={Status}): {Body}",
                    fileID, (int)response.StatusCode, body);
                return;
            }

            _logger.LogInformation("Deleted Anthropic file {FileID}.", fileID);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to delete Anthropic file {FileID}", fileID);
        }
    }
}
