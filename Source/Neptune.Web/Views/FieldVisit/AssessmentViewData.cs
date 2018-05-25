using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class AssessmentViewData : FieldVisitSectionViewData
    {
        public AssessmentViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.Assessment)
        {
        }
    }
}