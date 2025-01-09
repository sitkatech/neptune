using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Security;

namespace Neptune.WebMvc.Views.TreatmentBMP
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
        public string RefreshOCTAPrioritizationLayerUrl { get; }

        public IndexViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.NeptunePage neptunePage, int treatmentBmpsInExportCount, int featureClassesInExportCount,
            string bulkBMPUploadUrl)
            : base(httpContext, linkGenerator, currentPerson, neptunePage, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            TreatmentBmpsInExportCount = treatmentBmpsInExportCount;
            FeatureClassesInExportCount = featureClassesInExportCount;
            PageTitle = "All Treatment BMPs";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new TreatmentBMPGridSpec(currentPerson, showDelete, showEdit, linkGenerator) {ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true};
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.TreatmentBMPGridJsonData());
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.New());
            BulkBMPUploadUrl = bulkBMPUploadUrl;
            HasEditPermissions = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            DownloadBMPInventoryUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.BMPInventoryExport());
            RefreshOCTAPrioritizationLayerUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(LinkGenerator, x => x.RefreshOCTAPrioritizationLayerFromOCSurvey());
        }
    }
}