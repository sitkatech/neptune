using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.WebMvc.Views.FieldVisit
{
    public class PhotosViewData : FieldVisitSectionViewData
    {
        public ManagePhotosWithPreviewViewData ManagePhotosWithPreviewViewData { get; }

        public PhotosViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson,
            EFModels.Entities.FieldVisit fieldVisit,
            List<EFModels.Entities.TreatmentBMPAssessment> treatmentBMPAssessments,
            ManagePhotosWithPreviewViewData managePhotosWithPreviewViewData)
            : base(httpContext, linkGenerator, webConfiguration, currentPerson, fieldVisit, EFModels.Entities.FieldVisitSection.Inventory, fieldVisit.TreatmentBMP.TreatmentBMPType, fieldVisit.MaintenanceRecord, treatmentBMPAssessments)
        {
            SubsectionName = "Photos";
            SectionHeader = "Photos";
            ManagePhotosWithPreviewViewData = managePhotosWithPreviewViewData;
        }
    }
}