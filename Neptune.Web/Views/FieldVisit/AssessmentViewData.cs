using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Controllers;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentViewData : FieldVisitSectionViewData
    {
        public string MaintenanceUrl { get; }

        public AssessmentViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit) : base(httpContext, linkGenerator, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.Assessment)
        {
            MaintenanceUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Maintain(fieldVisit));
        }
    }
}