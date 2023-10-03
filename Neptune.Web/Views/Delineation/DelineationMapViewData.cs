using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Security;

namespace Neptune.Web.Views.Delineation
{
    public class DelineationMapViewData : NeptuneViewData
    {
        public DelineationMapViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.NeptunePage neptunePage, StormwaterMapInitJson mapInitJson, EFModels.Entities.TreatmentBMP initialTreatmentBMP, string bulkUploadTreatmentBMPDelineationsUrl, string getStormwaterJurisdictionCqlFilter, string geoServerUrl, string autoDelineateServiceUrl) : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            MapInitJson = mapInitJson;
            IsInitialTreatmentBMPProvided = initialTreatmentBMP != null;
            InitialTreatmentBMPID = initialTreatmentBMP?.TreatmentBMPID;
            EntityName = FieldDefinitionType.Delineation.FieldDefinitionTypeDisplayName;
            PageTitle = "Delineation Map";
            GeoServerUrl = geoServerUrl;

            DelineationMapConfig = new DelineationMapConfig(linkGenerator, getStormwaterJurisdictionCqlFilter, autoDelineateServiceUrl);
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