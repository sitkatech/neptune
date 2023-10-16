using Neptune.Jobs;

namespace Neptune.WebMvc.Common;

public class WebConfiguration : NeptuneJobConfiguration
{
    public string DatabaseConnectionString { get; set; }
    public string RecaptchaPublicKey { get; set; }
    public string RecaptchaPrivateKey { get; set; }
    public string RecaptchaValidatorUrl { get; set; }

    public string KeystoneUrl { get; set; }
    public string KeystoneRegisterUrl { get; set; }
    public string KeystoneInviteUserUrl { get; set; }
    public string KeystoneUserProfileUrl { get; set; }
    public string KeystoneOpenIDClientID { get; set; }
    public string KeystoneOpenIDUrl { get; set; }
    public string KeystoneOpenIDClientSecret { get; set; }
    public string ParcelMapServiceUrl { get; set; }
    public string MapServiceLayerNameParcel { get; set; }
    public string MapServiceLayerNameLandUseBlock { get; set; }
    public string MapServiceLayerNameRegionalSubbasin { get; set; }

    public string AutoDelineateServiceUrl { get; set; }
    public string HRUServiceBaseUrl { get; set; }
    public string GDALAPIBaseUrl { get; set; }
    public string OCGISBaseUrl { get; set; }

    public string PathToPyqgisTestScript { get; set; }
    public string PyqgisWorkingDirectory { get; set; }
    public string PathToPyqgisLauncher { get; set; }
    public string PathToPyqgisProjData { get; set; }

    public string PyqgisUsername { get; set; }
    public string PyqgisPassword { get; set; }

    public string PathToFieldVisitUploadTemplate { get; set; }
    public string AzureBlobStorageConnectionString { get; set; }
}