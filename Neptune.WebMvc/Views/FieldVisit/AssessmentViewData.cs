using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Controllers;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class AssessmentViewData : FieldVisitSectionViewData
    {
        public string MaintenanceUrl { get; }

        public AssessmentViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.FieldVisit fieldVisit, List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments, EFModels.Entities.MaintenanceRecord? maintenanceRecord) : base(httpContext, linkGenerator, webConfiguration, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.Assessment, fieldVisit.TreatmentBMP.TreatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
            MaintenanceUrl = SitkaRoute<FieldVisitController>.BuildUrlFromExpression(linkGenerator, x => x.Maintain(fieldVisit.FieldVisitID));
        }
    }
}