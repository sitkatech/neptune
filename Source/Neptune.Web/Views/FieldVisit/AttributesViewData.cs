using Neptune.Web.Models;

namespace Neptune.Web.Views.FieldVisit
{
    public class AttributesViewData : FieldVisitSectionViewData
    {
        public AttributesViewData(Person currentPerson, Models.FieldVisit fieldVisit) : base(currentPerson, fieldVisit,
            Models.FieldVisitSection.Inventory)
        {
            SubsectionName = "Attributes";
        }
    }
}