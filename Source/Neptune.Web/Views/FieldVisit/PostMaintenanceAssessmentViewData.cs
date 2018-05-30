using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class PostMaintenanceAssessmentViewData : FieldVisitSectionViewData
    {
        public PostMaintenanceAssessmentViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.PostMaintenanceAssessment)
        {
            BeginAssessmentUrl = fieldVisit.PostMaintenanceAssessment == null
                ? SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.NewAssessment(fieldVisit, (int)FieldVisitAssessmentType.PostMaintenance))
                : SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.EditAssessment(fieldVisit, (int)FieldVisitAssessmentType.PostMaintenance));
            MaintenanceUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Maintain(fieldVisit));
        }

        public string BeginAssessmentUrl { get; }
        public string MaintenanceUrl { get; }
    }
}