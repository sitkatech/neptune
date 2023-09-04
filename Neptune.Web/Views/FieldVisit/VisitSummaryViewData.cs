using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.FieldVisit
{
    public class VisitSummaryViewData : FieldVisitSectionViewData
    {
        public VisitSummaryViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit) : base(httpContext, linkGenerator, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.VisitSummary)
        {
        }
    }
}