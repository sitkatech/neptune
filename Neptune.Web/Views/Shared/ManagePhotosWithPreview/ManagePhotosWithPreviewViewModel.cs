using System.ComponentModel;
using Neptune.Models.DataTransferObjects;
using Neptune.Web.Common.Mvc;
using Neptune.Web.Views.FieldVisit;

namespace Neptune.Web.Views.Shared.ManagePhotosWithPreview
{
    public abstract class ManagePhotosWithPreviewViewModel : FieldVisitViewModel
    {
        public virtual IList<ManagePhotoWithPreviewPhotoDto> PhotoSimples { get; set; }

        [DisplayName("Upload a photo")]
        [SitkaFileExtensions("jpg|jpeg|gif|png")]
        public virtual IFormFile Photo { get; set; }

        [DisplayName("Caption")]
        public virtual string Caption { get; set; }
    }
}
