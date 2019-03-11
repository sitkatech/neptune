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
        }

        public int? InitialTreatmentBMPID { get; }

        public StormwaterMapInitJson MapInitJson { get; }
        public bool IsInitialTreatmentBMPProvided { get; }
        public string GeoServerUrl { get; }
    }
}