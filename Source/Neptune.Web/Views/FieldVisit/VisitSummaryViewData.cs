using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class VisitSummaryViewData : FieldVisitSectionViewData
    {
        public VisitSummaryViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.VisitSummary)
        {
        }
    }
}