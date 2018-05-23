using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class LocationViewData : FieldVisitSectionViewData
    {
        public LocationViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit,
            "Location") //todo: handle subsection names elegantly
        {
        }
    }
}