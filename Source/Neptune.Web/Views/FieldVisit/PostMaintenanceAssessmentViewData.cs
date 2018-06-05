using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class PostMaintenanceAssessmentViewData : FieldVisitSectionViewData
    {
        public PostMaintenanceAssessmentViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(
            currentPerson, fieldVisit, Models.FieldVisitSection.PostMaintenanceAssessment)
        {
            WrapupUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(x => x.WrapUpVisit(fieldVisit));
        }

        public string WrapupUrl { get; }
    }
}