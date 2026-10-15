using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Logging;
using Neptune.Common.Services;
using Neptune.EFModels.Entities;

namespace Neptune.API.Services.AI;

/// <summary>
/// Opens a WQMP document's PDF from blob storage with any junk ahead of the %PDF signature
/// skipped (NPT-1120). Shared by the Files API upload and the Claude CLI path (NPT-1133), which
/// both hand the bytes to a parser that requires the signature at byte 0.
/// </summary>
public class WqmpPdfBlobReader
{
    /// <summary>
    /// Number of leading bytes scanned for the %PDF signature. Matches the tolerance window
    /// used by mainstream readers, which is where these files pick up their reputation for
    /// being fine.
    /// </summary>
    private const int PdfHeaderScanBytes = 1024;

    private static readonly byte[] PdfSignature = "%PDF"u8.ToArray();

    private readonly AzureBlobStorageService _blobService;
    private readonly ILogger<WqmpPdfBlobReader> _logger;

    public WqmpPdfBlobReader(AzureBlobStorageService blobService, ILogger<WqmpPdfBlobReader> logger)
    {
        _blobService = blobService;
        _logger = logger;
    }

    /// <summary>The PDF stream (starting at %PDF) and its length in bytes. Dispose the download.</summary>
    public sealed record NormalizedPdf(BlobDownloadStreamingResult Download, long ContentLength) : System.IDisposable
    {
        public Stream Content => Download.Content;
        public void Dispose() => Download.Dispose();
    }

    public async Task<NormalizedPdf> OpenAsync(WaterQualityManagementPlanDocument document, CancellationToken cancellationToken)
    {
        var canonicalName = document.FileResource.GetFileResourceGUIDAsString().ToLower();

        // Skip any junk ahead of the %PDF signature — see FindPdfHeaderOffsetAsync.
        var headerOffset = await FindPdfHeaderOffsetAsync(
            canonicalName, document.WaterQualityManagementPlanDocumentID, cancellationToken);

        var download = headerOffset == 0
            ? await _blobService.DownloadBlobFromBlobStorageAsStream(canonicalName, cancellationToken)
            : await _blobService.DownloadBlobRangeFromBlobStorageAsStream(
                canonicalName, headerOffset, cancellationToken: cancellationToken);

        return new NormalizedPdf(download, document.FileResource.ContentLength - headerOffset);
    }

    /// <summary>
    /// Returns the offset of the %PDF signature within the first <see cref="PdfHeaderScanBytes"/>
    /// bytes of the blob, or 0 if it is already at byte 0 (or cannot be found).
    ///
    /// A valid PDF starts with %PDF at byte 0, but real-world files sometimes carry junk ahead
    /// of it — one WQMP arrived with a single stray 0x01 byte. Acrobat and browsers scan the
    /// leading kilobyte for the signature and open such files without complaint, so they look
    /// healthy to the uploader. Anthropic's parser requires the signature at byte 0; without it
    /// the document is sniffed as application/octet-stream and the extraction call fails with
    /// "Unsupported document file format", which names the wrong problem entirely and sends you
    /// hunting for a MIME-type bug. Trimming the leading bytes makes those documents extractable.
    /// </summary>
    private async Task<int> FindPdfHeaderOffsetAsync(
        string canonicalName, int documentID, CancellationToken cancellationToken)
    {
        using var head = await _blobService.DownloadBlobRangeFromBlobStorageAsStream(
            canonicalName, 0, PdfHeaderScanBytes, cancellationToken);
        using var buffer = new MemoryStream();
        await head.Content.CopyToAsync(buffer, cancellationToken);
        var bytes = buffer.ToArray();

        var offset = IndexOfSignature(bytes);
        if (offset < 0)
        {
            // Not a PDF as far as we can tell. Pass it on unchanged rather than inventing a new
            // failure mode — Anthropic will reject it, and that rejection is the honest answer.
            _logger.LogWarning(
                "No %PDF signature in the first {ScanBytes} bytes for documentID={DocumentID}; using it unchanged. Extraction will likely fail — the file may not be a PDF.",
                PdfHeaderScanBytes, documentID);
            return 0;
        }

        if (offset > 0)
        {
            _logger.LogWarning(
                "PDF for documentID={DocumentID} has {Offset} junk byte(s) before the %PDF signature; skipping them so Anthropic can parse the document.",
                documentID, offset);
        }

        return offset;
    }

    private static int IndexOfSignature(byte[] bytes)
    {
        for (var i = 0; i + PdfSignature.Length <= bytes.Length; i++)
        {
            var matched = true;
            for (var j = 0; j < PdfSignature.Length; j++)
            {
                if (bytes[i + j] != PdfSignature[j])
                {
                    matched = false;
                    break;
                }
            }
            if (matched)
            {
                return i;
            }
        }
        return -1;
    }
}
