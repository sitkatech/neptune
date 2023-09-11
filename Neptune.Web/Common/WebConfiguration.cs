using Neptune.Common.DesignByContract;
using System.Reflection;

namespace Neptune.Web.Common;

public class WebConfiguration
{
    public string DatabaseConnectionString { get; set; }
    public string RecaptchaPublicKey { get; set; }
    public string RecaptchaPrivateKey { get; set; }
    public string RecaptchaValidatorUrl { get; set; }
    public string SitkaSupportEmail { get; set; }
    public string DoNotReplyEmail { get; set; }
    public string Ogr2OgrExecutable { get; set; }
    public string OgrInfoExecutable { get; set; }

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
    public string ModelBasinServiceUrl { get; set; }
    public string PrecipitationZoneServiceUrl { get; set; }
    public string OCTAPrioritizationServiceUrl { get; set; }
    public string RegionalSubbasinServiceUrl { get; set; }

    public string PathToPyqgisTestScript { get; set; }
    public string PyqgisWorkingDirectory { get; set; }
    public string PathToPyqgisLauncher { get; set; }
    public string PathToPyqgisProjData { get; set; }

    public string NereidUrl { get; set; }
    public string PyqgisUsername { get; set; }
    public string PyqgisPassword { get; set; }

    public string PathToFieldVisitUploadTemplate { get; set; }
    public string LogFileFolder { get; set; }
    public string NereidLogFileFolder { get; set; }

    public string SitkaEmailRedirect { get; set; }
    public string MailLogBcc { get; set; }
    public string BlobStorageConnectionString { get; set; }

    //public static readonly string CanonicalHostName = CanonicalHostNames.FirstOrDefault();

    //public static readonly string CanonicalHostNameRoot =
    //    SitkaConfiguration.GetRequiredAppSetting("CanonicalHostNameRoot");
    //public static readonly string CanonicalHostNameTrash =
    //    SitkaConfiguration.GetRequiredAppSetting("CanonicalHostNameTrash");
    //public static readonly string CanonicalHostNameModeling =
    //    SitkaConfiguration.GetRequiredAppSetting("CanonicalHostNameModeling");
    //public static readonly string CanonicalHostNamePlanning =
    //    SitkaConfiguration.GetRequiredAppSetting("CanonicalHostNamePlanning");

    //public static List<string> CanonicalHostNames => new List<string>(SitkaConfiguration.GetRequiredAppSettingList("CanonicalHostName"));



    //public static string GetCanonicalHost(string hostName, bool useApproximateMatch)
    //{
    //    //First search for perfect match
    //    var canonicalHostNames = CanonicalHostNames;
    //    var result = canonicalHostNames.FirstOrDefault(h => string.Equals(h, hostName, StringComparison.InvariantCultureIgnoreCase));

    //    if (!string.IsNullOrWhiteSpace(result) || !useApproximateMatch)
    //    {
    //        return result;
    //    }

    //    //Use the domain name  (laketahoeinfo.org -->  should use www.laketahoeinfo.org for the match)
    //    return canonicalHostNames.FirstOrDefault(h => h.EndsWith(hostName, StringComparison.InvariantCultureIgnoreCase)) ?? CanonicalHostName;
    //}
}