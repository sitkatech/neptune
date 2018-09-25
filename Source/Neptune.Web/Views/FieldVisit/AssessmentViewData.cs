using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentViewData : FieldVisitSectionViewData
    {
        public string MaintenanceUrl { get; }

        public AssessmentViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.Assessment)
        {
            MaintenanceUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.Maintain(fieldVisit));
        }
    }
}