using Neptune.EFModels.Entities;
using Neptune.Web.Views.Shared.EditAttributes;

namespace Neptune.Web.Views.FieldVisit
{
    public class AttributesViewData : FieldVisitSectionViewData
    {
        public EditAttributesViewData EditAttributesViewData { get; }

        public AttributesViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            EFModels.Entities.FieldVisit fieldVisit,
            List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments,
            EFModels.Entities.MaintenanceRecord? maintenanceRecord,
            EditAttributesViewData editAttributesViewData) : base(httpContext, linkGenerator, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.Inventory, fieldVisit.TreatmentBMP.TreatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
            EditAttributesViewData = editAttributesViewData;
            SubsectionName = "Attributes";
            SectionHeader = "Attributes";
        }
    }
}
