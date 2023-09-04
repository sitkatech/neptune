using Neptune.EFModels.Entities;
using Neptune.Web.Views.Shared.Location;

namespace Neptune.Web.Views.FieldVisit
{
    public class LocationViewData : FieldVisitSectionViewData
    {
        public LocationViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit, EditLocationViewData editLocationViewData) : base(httpContext, linkGenerator, currentPerson, fieldVisit,
            EFModels.Entities.FieldVisitSection.Inventory)
        {
            EditLocationViewData = editLocationViewData;
            SubsectionName = "Location";
            SectionHeader = "Location";
        }

        public EditLocationViewData EditLocationViewData { get; }
    }
}