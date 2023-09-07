using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.FieldVisit
{
    public class MaintainViewData : FieldVisitSectionViewData
    {
        public bool IsNew { get; }
        public string PostMaintenanceAssessmentUrl { get; }
        public IEnumerable<SelectListItem> AllMaintenanceRecordTypes { get; }
        public string EditMaintenanceRecordUrl { get; }

        public MaintainViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit, IEnumerable<SelectListItem> allMaintenanceRecordTypes) : base(httpContext, linkGenerator, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.Maintenance)
        {
            AllMaintenanceRecordTypes = allMaintenanceRecordTypes;
            PostMaintenanceAssessmentUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.PostMaintenanceAssessment(fieldVisit));
            EditMaintenanceRecordUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.EditMaintenanceRecord(fieldVisit));
            IsNew = fieldVisit.MaintenanceRecord == null;
        }
    }
}
