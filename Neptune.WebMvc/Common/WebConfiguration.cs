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
    public string MapServiceUrl { get; set; }

    public string AutoDelineateServiceUrl { get; set; }
    public string GDALAPIBaseUrl { get; set; }
    public string OCGISBaseUrl { get; set; }

    public string PathToFieldVisitUploadTemplate { get; set; }
    public string AzureBlobStorageConnectionString { get; set; }
}