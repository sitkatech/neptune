using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.TreatmentBMPImage
{
    public class ManageTreatmentBMPImagesViewData : NeptuneViewData
    {
        public ManagePhotosWithPreviewViewData ManagePhotosWithPreviewViewData { get; }

        public ManageTreatmentBMPImagesViewData(Person currentPerson,
            ManagePhotosWithPreviewViewData managePhotosWithPreviewViewData) : base(currentPerson)
        {
            ManagePhotosWithPreviewViewData = managePhotosWithPreviewViewData;
        }
    }
}
