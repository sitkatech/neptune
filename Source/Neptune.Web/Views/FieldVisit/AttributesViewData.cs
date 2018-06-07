using Neptune.Web.Models;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class AttributesViewData : FieldVisitSectionViewData
    {
        public EditAttributesViewData EditAttributesViewData { get; }

        public AttributesViewData(Person currentPerson, Models.FieldVisit fieldVisit, EditAttributesViewData editAttributesViewData) : base(currentPerson, fieldVisit,
            Models.FieldVisitSection.Inventory)
        {
            EditAttributesViewData = editAttributesViewData;
            SubsectionName = "Attributes";
            SectionHeader = "Attributes";
        }
    }
}
