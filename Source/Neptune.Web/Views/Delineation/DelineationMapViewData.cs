using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationMapViewData : NeptuneViewData
    {
        public DelineationMapViewData(Person currentPerson, Models.NeptunePage neptunePage, StormwaterMapInitJson mapInitJson, Models.TreatmentBMP initialTreatmentBMP, string bulkUploadTreatmentBMPDelineationsUrl) : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            MapInitJson = mapInitJson;
            IsInitialTreatmentBMPProvided = initialTreatmentBMP != null;
            InitialTreatmentBMPID = initialTreatmentBMP?.TreatmentBMPID;
            EntityName = FieldDefinitionType.Delineation.FieldDefinitionTypeDisplayName;
            PageTitle = "Delineation Map";
            GeoServerUrl = NeptuneWebConfiguration.ParcelMapServiceUrl;

            DelineationMapConfig = new DelineationMapConfig(currentPerson.GetStormwaterJurisdictionCqlFilter());
            BulkUploadTreatmentBMPDelineationsUrl = bulkUploadTreatmentBMPDelineationsUrl;
            HasManagePermission = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
        }

        public int? InitialTreatmentBMPID { get; }

        public StormwaterMapInitJson MapInitJson { get; }
        public bool IsInitialTreatmentBMPProvided { get; }
        public string GeoServerUrl { get; }
        public DelineationMapConfig DelineationMapConfig { get; set; }

        public string BulkUploadTreatmentBMPDelineationsUrl { get; }

        public bool HasManagePermission { get; }
    }
}