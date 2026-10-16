using Neptune.Common.Recaptcha;
using Neptune.Jobs;

namespace Neptune.API.Services;

public class NeptuneConfiguration : NeptuneJobConfiguration
{
    public string DatabaseConnectionString { get; set; }
    public string AzureBlobStorageConnectionString { get; set; }
    public string NereidUrl { get; set; }
    public GoogleRecaptchaV3Config GoogleRecaptchaV3Config { get; set; }

    public string GDALAPIBaseUrl { get; set; }
    public string OverlayAPIBaseUrl { get; set; }

    // The in-process NTS overlay finishes a full TGU refresh in ~4 minutes (measured NPT-1105); 30 gives
    // generous headroom for slower prod SQL while still failing a genuinely wedged run inside the hour.
    public int OverlayAPIHttpClientTimeoutMinutes { get; set; } = 30;

    public string OCGISBaseUrl { get; set; }

    public string AnthropicApiKey { get; set; }
    public string ClaudeModelId { get; set; } = "claude-sonnet-4-6";

    /// <summary>
    /// NPT-1133: send WQMP AI extraction through the developer's own Claude Code CLI login
    /// instead of the API key. Unset means "on in Development, off elsewhere" (Startup resolves
    /// it); the API refuses to start if it's true outside Development.
    /// </summary>
    public bool? ClaudeCliEnabled { get; set; }
    /// <summary>The <c>claude</c> executable; a full path when it isn't on PATH.</summary>
    public string ClaudeCliPath { get; set; } = "claude";
    /// <summary>Per-category ceiling on a CLI call. Longer than the API path's 4 minutes: the CLI reads the PDF itself first.</summary>
    public int ClaudeCliTimeoutMinutes { get; set; } = 15;

    // NPT-998: Azure Blob paths for the bulk-upload .xlsx/.csv templates the user downloads
    // from the Data Hub upload pages. Same values as Neptune.WebMvc.Common.WebConfiguration so
    // both surfaces can serve the same template files during the MVC retirement transition.
    public string PathToBulkUploadWQMPTemplate { get; set; }
    public string PathToSimplifiedBMPTemplate { get; set; }
    public string PathToUploadWQMPBoundaryTemplate { get; set; }
    public string PathToFieldVisitUploadTemplate { get; set; }
    public string PathToOVTAUploadTemplate { get; set; }

    /// <summary>
    /// Upper bound on PDF size accepted by the WQMP upload + AI extraction pipeline.
    /// Defaults to 200 MB (Anthropic's Files API ceiling is 500 MB; 200 MB covers
    /// 99%+ of real-world scanned WQMPs with headroom). Override per environment if
    /// needed.
    /// </summary>
    public long MaxExtractablePdfSizeBytes { get; set; } = 400L * 1024 * 1024;
    public string Auth0Domain { get; set; }
    public string Auth0ClientID { get; set; }
}