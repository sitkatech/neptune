using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.FieldVisit
{
    public class PostMaintenanceAssessmentViewData : FieldVisitSectionViewData
    {
        public PostMaintenanceAssessmentViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.FieldVisit fieldVisit, List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments, EFModels.Entities.MaintenanceRecord? maintenanceRecord) : base(httpContext, linkGenerator, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.PostMaintenanceAssessment, fieldVisit.TreatmentBMP.TreatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
        }
    }
}