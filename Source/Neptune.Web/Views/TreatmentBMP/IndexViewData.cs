using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

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
        public bool HasManagePermissions { get; }
        public string DownloadBMPInventoryUrl { get; }

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage, int treatmentBmpsInExportCount, int featureClassesInExportCount)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP, neptunePage)
        {
            TreatmentBmpsInExportCount = treatmentBmpsInExportCount;
            FeatureClassesInExportCount = featureClassesInExportCount;
            PageTitle = "All Treatment BMPs";
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            var showDelete = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            var showEdit = new JurisdictionEditFeature().HasPermissionByPerson(currentPerson);
            GridSpec = new TreatmentBMPGridSpec(currentPerson, showDelete, showEdit) {ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true};
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(j => j.TreatmentBMPGridJsonData());
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.New());
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
            DownloadBMPInventoryUrl =
                SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.BMPInventoryExport());
        }
    }
}