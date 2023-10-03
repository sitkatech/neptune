using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;

namespace Neptune.Web.Views.Shared.ManagePhotosWithPreview
{
    public class ManagePhotosWithPreviewViewData : NeptuneViewData
    {
        public IDictionary<int, IFileResourcePhoto> PhotosByID { get; }

        public ManagePhotosWithPreviewViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, IEnumerable<TreatmentBMPAssessmentPhoto> treatmentBMPAssessmentPhotos)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PhotosByID = treatmentBMPAssessmentPhotos.ToDictionary(x => x.TreatmentBMPAssessmentPhotoID, x => (IFileResourcePhoto) x);
        }
        public ManagePhotosWithPreviewViewData(HttpContext httpContext, LinkGenerator linkGenerator, WebConfiguration webConfiguration, Person currentPerson, IEnumerable<EFModels.Entities.TreatmentBMPImage> treatmentBMPImages)
            : base(httpContext, linkGenerator, currentPerson, NeptuneArea.OCStormwaterTools, webConfiguration)
        {
            PhotosByID = treatmentBMPImages.ToDictionary(x => x.TreatmentBMPImageID, x => (IFileResourcePhoto) x);
        }
    }
}
