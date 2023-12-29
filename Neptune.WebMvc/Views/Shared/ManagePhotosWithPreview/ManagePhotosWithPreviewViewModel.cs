using System.ComponentModel;
using Neptune.Models.DataTransferObjects;
using Neptune.WebMvc.Common.Mvc;
using Neptune.WebMvc.Views.FieldVisit;

namespace Neptune.WebMvc.Views.Shared.ManagePhotosWithPreview
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
