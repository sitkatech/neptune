using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class VisitSummaryViewData : FieldVisitSectionViewData
    {
        public List<CustomAttribute> TreatmentBMPCustomAttributes { get; }

        public VisitSummaryViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.FieldVisit fieldVisit,
            List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments,
            EFModels.Entities.MaintenanceRecord? maintenanceRecord,
            EFModels.Entities.TreatmentBMPType treatmentBMPType, List<CustomAttribute> treatmentBMPCustomAttributes) : base(httpContext, linkGenerator, webConfiguration, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.VisitSummary, treatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
            TreatmentBMPCustomAttributes = treatmentBMPCustomAttributes;
        }
    }
}