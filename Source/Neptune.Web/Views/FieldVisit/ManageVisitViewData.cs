using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class ManageVisitViewData : FieldVisitSectionViewData
    {
        public ManageVisitViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.ManageVisit)
        {
        }
    }
}