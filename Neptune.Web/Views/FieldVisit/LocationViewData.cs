using Neptune.EFModels.Entities;
using Neptune.Web.Views.Shared.Location;

namespace Neptune.Web.Views.FieldVisit
{
    public class LocationViewData : FieldVisitSectionViewData
    {
        public EditLocationViewData EditLocationViewData { get; }

        public LocationViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            EFModels.Entities.FieldVisit fieldVisit,
            List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments,
            EditLocationViewData editLocationViewData, EFModels.Entities.MaintenanceRecord? maintenanceRecord) : base(httpContext, linkGenerator, currentPerson, fieldVisit,
            EFModels.Entities.FieldVisitSection.Inventory, fieldVisit.TreatmentBMP.TreatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
            EditLocationViewData = editLocationViewData;
            SubsectionName = "Location";
            SectionHeader = "Location";
        }
    }
}