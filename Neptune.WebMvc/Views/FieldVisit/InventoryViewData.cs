using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class InventoryViewData : FieldVisitSectionViewData
    {
        public string AssessUrl { get; }
        public string LocationUrl { get; }

        public InventoryViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit, List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments, EFModels.Entities.MaintenanceRecord? maintenanceRecord) : base(httpContext, linkGenerator, webConfiguration, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.Inventory, fieldVisit.TreatmentBMP.TreatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
            LocationUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Location(fieldVisit));
            AssessUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Assessment(fieldVisit));
        }
    }
}