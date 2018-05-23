using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class WrapUpVisitViewData : FieldVisitSectionViewData
    {
        public WrapUpVisitViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.WrapUpVisit.FieldVisitSectionName)
        {
        }
    }
}