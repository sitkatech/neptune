using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationMapViewData : NeptuneViewData
    {
        public DelineationMapViewData(Person currentPerson, Models.NeptunePage neptunePage, StormwaterMapInitJson mapInitJson, Models.TreatmentBMP initialTreatmentBMP) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            MapInitJson = mapInitJson;
            IsInitialTreatmentBMPProvided = initialTreatmentBMP != null;
            InitialTreatmentBMPID = initialTreatmentBMP?.TreatmentBMPID;
            EntityName = "Delineation";
            PageTitle = "Delineation Map";
            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;

            DelineationMapConfig = new DelineationMapConfig();
            
        }

        public int? InitialTreatmentBMPID { get; }

        public StormwaterMapInitJson MapInitJson { get; }
        public bool IsInitialTreatmentBMPProvided { get; }
        public string GeoServerUrl { get; }
        public DelineationMapConfig DelineationMapConfig { get; set; }
    }

    
    public class DelineationMapConfig
    {
        public string AutoDelineateBaseUrl { get; set; }

        // todo: source these values from Web.config when appropriate
        public DelineationMapConfig()
        {
            AutoDelineateBaseUrl =
                "https://ocgis.com/arcpub/rest/services/Flood/OCPWGlobalStormwaterDelineationServiceSurfaceOnly/GPServer/Global%20Surface%20Delineation/";
        }
    }
}