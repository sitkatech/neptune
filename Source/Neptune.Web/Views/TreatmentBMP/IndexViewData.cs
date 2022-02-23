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

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage, int treatmentBmpsInExportCount, int featureClassesInExportCount, string bulkBMPUploadUrl)
            : base(currentPerson, neptunePage, NeptuneArea.OCStormwaterTools)
        {
            TreatmentBmpsInExportCount = treatmentBmpsInExportCount;
            FeatureClassesInExportCount = featureClassesInExportCount;
            PageTitle = "All Treatment BMPs";
            EntityName = $"{FieldDefinitionType.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new TreatmentBMPGridSpec(currentPerson, showDelete, showEdit) {ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true};
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(j => j.TreatmentBMPGridJsonData());
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.New());
            BulkBMPUploadUrl = bulkBMPUploadUrl;
            HasEditPermissions = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            HasAdminPermissions = new NeptuneAdminFeature().HasPermissionByPerson(currentPerson);
            DownloadBMPInventoryUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.BMPInventoryExport());
            RefreshModelBasinsUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.RefreshModelBasinsFromOCSurvey());
            RefreshPrecipitationZonesUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.RefreshPrecipitationZonesFromOCSurvey());
        }
    }
}