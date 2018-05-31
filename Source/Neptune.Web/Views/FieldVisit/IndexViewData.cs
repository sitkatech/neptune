using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.Shared;

namespace Neptune.Web.Views.FieldVisit
{
    public class IndexViewData : NeptuneViewData
    {
        public FieldVisitGridSpec GridSpec { get; }
        public string GridName { get; }
        public string GridDataUrl { get; }
        public bool HasManagePermissions { get; }

        public IndexViewData(Person currentPerson, Models.NeptunePage neptunePage)
            : base(currentPerson, StormwaterBreadCrumbEntity.FieldRecords, neptunePage)
        {
            PageTitle = "All Field Records";
            EntityName = "Field Records";
            GridSpec = new FieldVisitGridSpec(currentPerson) {ObjectNameSingular = "Field Record", ObjectNamePlural = "Field Records", SaveFiltersInCookie = true};
            GridName = "fieldVisitsGrid";
            GridDataUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(j => j.AllFieldVisitsGridJsonData());
            HasManagePermissions = new JurisdictionManageFeature().HasPermissionByPerson(currentPerson);
        }
    }
}