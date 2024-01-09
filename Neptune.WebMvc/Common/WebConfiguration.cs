using Neptune.Jobs;

namespace Neptune.WebMvc.Common;

public class WebConfiguration : NeptuneJobConfiguration
{
    public string DatabaseConnectionString { get; set; }

    public string KeystoneRegisterUrl => $"{KeystoneOpenIDUrl}/Account/Register";
    public string KeystoneInviteUserUrl { get; set; }
    public string KeystoneOpenIDClientID { get; set; }
    public string KeystoneOpenIDUrl { get; set; }
    public string KeystoneOpenIDClientSecret { get; set; }
    public string MapServiceUrl { get; set; }

    public string AutoDelineateServiceUrl { get; set; }
    public string NereidUrl { get; set; }
    public string GDALAPIBaseUrl { get; set; }
    public string QGISAPIBaseUrl { get; set; }
    public string OCGISBaseUrl { get; set; }

    public string PathToFieldVisitUploadTemplate { get; set; }
    public string AzureBlobStorageConnectionString { get; set; }

    public GoogleRecaptchaV3Config GoogleRecaptchaV3Config { get; set; }
}