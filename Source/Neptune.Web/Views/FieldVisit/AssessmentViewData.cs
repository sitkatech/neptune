using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentViewData : FieldVisitSectionViewData
    {
        public AssessmentViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.Assessment)
        {
            // TODO DO DO DO
            BeginAssessmentUrl = string.Empty;
            MaintenanceUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Maintain(fieldVisit));
        }

        public string BeginAssessmentUrl { get; }
        public string MaintenanceUrl { get; }
    }
}