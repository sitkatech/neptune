using Neptune.EFModels.Entities;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class AttributesViewData : FieldVisitSectionViewData
    {
        public EditAttributesViewData EditAttributesViewData { get; }

        public AttributesViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit, EditAttributesViewData editAttributesViewData) : base(httpContext, linkGenerator, currentPerson, fieldVisit,
            EFModels.Entities.FieldVisitSection.Inventory)
        {
            EditAttributesViewData = editAttributesViewData;
            SubsectionName = "Attributes";
            SectionHeader = "Attributes";
        }
    }
}
