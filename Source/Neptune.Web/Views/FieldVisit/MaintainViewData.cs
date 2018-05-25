using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class MaintainViewData : FieldVisitSectionViewData
    {
        public MaintainViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit, Models.FieldVisitSection.Maintenance)
        {
        }
    }
}