using System.ComponentModel;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common.Mvc;
using Neptune.Web.Views.FieldVisit;

namespace Neptune.Web.Views.Shared.ManagePhotosWithPreview
{
    public abstract class ManagePhotosWithPreviewViewModel : FieldVisitViewModel
    {
        public IList<ManagePhotoWithPreviewPhotoDto>? PhotoSimples { get; set; }

        [DisplayName("Upload a photo")]
        [SitkaFileExtensions("jpg|jpeg|gif|png")]
        public IFormFile? Photo { get; set; }

        [DisplayName("Caption")]
        public string? Caption { get; set; }
    }
}
