using Neptune.EFModels;
using Neptune.EFModels.Entities;

namespace Neptune.Web.Views.Shared.ManagePhotosWithPreview
{
    public class ManagePhotosWithPreviewViewData : NeptuneViewData
    {
        public IDictionary<int, IFileResourcePhoto> PhotosByID { get; }

        public ManagePhotosWithPreviewViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.TreatmentBMPAssessment treatmentBMPAssessment)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            PhotosByID = treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.ToDictionary(x => x.TreatmentBMPAssessmentPhotoID, x => (IFileResourcePhoto) x);
        }
        public ManagePhotosWithPreviewViewData(HttpContext httpContext, LinkGenerator linkGenerator, Person currentPerson, EFModels.Entities.TreatmentBMP treatmentBMP)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools)
        {
            PhotosByID = treatmentBMP.TreatmentBMPImages.ToDictionary(x => x.TreatmentBMPImageID, x => (IFileResourcePhoto) x);
        }
    }
}
