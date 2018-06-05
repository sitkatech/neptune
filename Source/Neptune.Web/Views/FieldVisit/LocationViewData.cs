using Neptune.Web.Models;
using Neptune.Web.Views.Shared.Location;

namespace Neptune.Web.Views.FieldVisit
{
    public class LocationViewData : FieldVisitSectionViewData
    {
        public LocationViewData(Person currentPerson, Models.FieldVisit fieldVisit, EditLocationViewData editLocationViewData) : base(currentPerson, fieldVisit,
            Models.FieldVisitSection.Inventory)
        {
            EditLocationViewData = editLocationViewData;
            SubsectionName = "Location";
            SectionHeader = "Location";
        }

        public EditLocationViewData EditLocationViewData { get; }
    }
}