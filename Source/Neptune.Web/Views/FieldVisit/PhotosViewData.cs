using Neptune.Web.Models;
using Neptune.Web.Views.Shared.ManagePhotosWithPreview;

namespace Neptune.Web.Views.FieldVisit
{
    public class PhotosViewData : FieldVisitSectionViewData
    {
        public ManagePhotosWithPreviewViewData ManagePhotosWithPreviewViewData { get; }

        public PhotosViewData(Person currentPerson, Models.FieldVisit fieldVisit,
            ManagePhotosWithPreviewViewData managePhotosWithPreviewViewData)
            : base(currentPerson, fieldVisit, Models.FieldVisitSection.Inventory)
        {
            SubsectionName = "Photos";
            SectionHeader = "Photos";
            ManagePhotosWithPreviewViewData = managePhotosWithPreviewViewData;
        }
    }
}