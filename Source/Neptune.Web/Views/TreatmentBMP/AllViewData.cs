using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class AllViewData : NeptuneViewData
    {
        public TreatmentBMPGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public string NewUrl { get; }
        public bool HasManagePermissions { get; }

        public AllViewData(Person currentPerson, Models.NeptunePage neptunePage)
            : base(currentPerson, StormwaterBreadCrumbEntity.TreatmentBMP, neptunePage)
        {
            PageTitle = "All Treatment BMPs";
            EntityName = $"{Models.FieldDefinition.TreatmentBMP.GetFieldDefinitionLabelPluralized()}";
            GridSpec = new TreatmentBMPGridSpec(currentPerson) {ObjectNameSingular = "Treatment BMP", ObjectNamePlural = "Treatment BMPs", SaveFiltersInCookie = true};
            GridName = "treatmentBMPsGrid";
            GridDataUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(j => j.TreatmentBMPGridJsonData());
            NewUrl = SitkaRoute<TreatmentBMPController>.BuildUrlFromExpression(x => x.New());
            HasManagePermissions = new NeptuneEditFeature().HasPermissionByPerson(currentPerson);
        }
    }
}