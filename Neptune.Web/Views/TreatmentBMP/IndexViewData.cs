using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class IndexViewData : NeptuneViewData
    {
        public int TreatmentBmpsInExportCount { get; }
        public int FeatureClassesInExportCount { get; }
        public TreatmentBMPGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public string NewUrl { get; }
        public string BulkBMPUploadUrl { get; set; }
        public bool HasEditPermissions { get; }
        public string DownloadBMPInventoryUrl { get; }
        public bool HasAdminPermissions { get; }
        public string RefreshModelBasinsUrl { get; }
        public string RefreshPrecipitationZonesUrl { get; }
        public string RefreshOCTAPrioritizationLayerUrl { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            NeptunePage neptunePage, int treatmentBmpsInExportCount, int featureClassesInExportCount,
            string bulkBMPUploadUrl)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBmpsInExportCount = treatmentBmpsInExportCount;
            FeatureClassesInExportCount = featureClassesInExportCount;
            PageTitle = "All Treatment BMPs";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new TreatmentBMPGridSpec(currentPerson, showDelete, showEdit, linkGenerator) {ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true};
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, j => j.TreatmentBMPGridJsonData());
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.New());
            BulkBMPUploadUrl = bulkBMPUploadUrl;
            HasEditPermissions = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            DownloadBMPInventoryUrl = "";//todo:SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(_linkGenerator, x => x.BMPInventoryExport());
            RefreshModelBasinsUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshModelBasinsFromOCSurvey());
            RefreshPrecipitationZonesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshPrecipitationZonesFromOCSurvey());
            RefreshOCTAPrioritizationLayerUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshOCTAPrioritizationLayerFromOCSurvey());
        }
    }
}