using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.FieldVisit
{
    public class VisitSummaryViewData : FieldVisitSectionViewData
    {
        public List<CustomAttribute> TreatmentBMPCustomAttributes { get; }

        public VisitSummaryViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson,
            EFModels.Entities.FieldVisit fieldVisit,
            List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments,
            EFModels.Entities.MaintenanceRecord? maintenanceRecord,
            EFModels.Entities.TreatmentBMPType treatmentBMPType, List<CustomAttribute> treatmentBMPCustomAttributes) : base(httpContext, linkGenerator, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.VisitSummary, treatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
            TreatmentBMPCustomAttributes = treatmentBMPCustomAttributes;
        }
    }
}