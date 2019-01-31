using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.ManagePhotosWithPreview
{
    public class ManagePhotosWithPreviewViewData : NeptuneViewData
    {
        public IDictionary<int, IFileResourcePhoto> PhotosByID { get; }

        public ManagePhotosWithPreviewViewData(Person currentPerson, Models.TreatmentBMPAssessment treatmentBMPAssessment)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            PhotosByID = treatmentBMPAssessment.TreatmentBMPAssessmentPhotos.ToDictionary(x => x.TreatmentBMPAssessmentPhotoID, x => (IFileResourcePhoto) x);
        }

        public ManagePhotosWithPreviewViewData(Person currentPerson, Models.TreatmentBMP treatmentBMP)
            : base(currentPerson, NeptuneArea.OCStormwaterTools)
        {
            PhotosByID = treatmentBMP.TreatmentBMPImages.ToDictionary(x => x.TreatmentBMPImageID, x => (IFileResourcePhoto) x);
        }
    }
}
