namespace Neptune.Web.Views.Delineation
{
    public class DelineationMapConfig
    {
        public string AutoDelineateBaseUrl { get; }
        public string JurisdictionCQLFilter { get; }

        // todo: source these values from Web.config when appropriate
        public DelineationMapConfig(string jurisdictionCQLFilter)
        {
            JurisdictionCQLFilter = jurisdictionCQLFilter;

            AutoDelineateBaseUrl =
                "https://ocgis.com/arcpub/rest/services/Flood/OCPWGlobalStormwaterDelineationServiceSurfaceOnly/GPServer/Global%20Surface%20Delineation/";
        }
    }
}