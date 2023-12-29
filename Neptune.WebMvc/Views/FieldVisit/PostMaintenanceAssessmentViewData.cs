using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class PostMaintenanceAssessmentViewData : FieldVisitSectionViewData
    {
        public PostMaintenanceAssessmentViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit, List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments, EFModels.Entities.MaintenanceRecord? maintenanceRecord) : base(httpContext, linkGenerator, webConfiguration, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.PostMaintenanceAssessment, fieldVisit.TreatmentBMP.TreatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
        }
    }
}