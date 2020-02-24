using System.Collections.Generic;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class MaintainViewData : FieldVisitSectionViewData
    {
        public bool IsNew { get; }
        public string PostMaintenanceAssessmentUrl { get; }
        public IEnumerable<SelectListItem> AllMaintenanceRecordTypes { get; }
        public string EditMaintenanceRecordUrl { get; }

        public MaintainViewData(Person currentPerson, Models.FieldVisit fieldVisit, IEnumerable<SelectListItem> allMaintenanceRecordTypes) : base(currentPerson, fieldVisit, Models.FieldVisitSection.Maintenance)
        {
            AllMaintenanceRecordTypes = allMaintenanceRecordTypes;
            PostMaintenanceAssessmentUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.PostMaintenanceAssessment(fieldVisit));
            EditMaintenanceRecordUrl =
                SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.EditMaintenanceRecord(fieldVisit));
            IsNew = fieldVisit.MaintenanceRecord == null;
        }
    }
}
