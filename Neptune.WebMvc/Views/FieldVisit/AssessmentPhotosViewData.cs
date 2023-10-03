using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class AssessmentPhotosViewData : FieldVisitSectionViewData
    {
        public ManagePhotosWithPreviewViewData ManagePhotosWithPreviewViewData { get; set; }

        public AssessmentPhotosViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment,
            EFModels.Entities.FieldVisitSection fieldVisitSection,
            ManagePhotosWithPreviewViewData managePhotosWithPreviewViewData,
            EFModels.Entities.TreatmentBMPType treatmentBMPType,
            EFModels.Entities.MaintenanceRecord? maintenanceRecord,
            List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments)
            : base(httpContext, linkGenerator, webConfiguration, currentPerson, treatmentBMPAssessment.FieldVisit, fieldVisitSection, treatmentBMPType, maintenanceRecord, treatmentBMPAssessments)
        {
            SubsectionName = "Photos";
            SectionHeader = $"{SectionHeader} - {SubsectionName}";
            ManagePhotosWithPreviewViewData = managePhotosWithPreviewViewData;
        }
    }
}
