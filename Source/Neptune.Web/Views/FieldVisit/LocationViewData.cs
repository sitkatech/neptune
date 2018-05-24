using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class LocationViewData : FieldVisitSectionViewData
    {
        public LocationViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit,
            Models.FieldVisitSection.Inventory.FieldVisitSectionName) //todo: handle subsection names elegantly
        {
            SubsectionName = "Location";
        }
    }
}