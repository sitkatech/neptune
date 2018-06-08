using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.ManagePhotosWithPreview
{
    public class ManagePhotosWithPreviewViewModel : FormViewModel
    {
        public ManagePhotosWithPreviewViewModel()
        {
            PhotoSimples = new List<ManagePhotoWithPreviewPhotoSimple>();
        }

        public IList<ManagePhotoWithPreviewPhotoSimple> PhotoSimples { get; set; }

        [DisplayName("Upload a photo")]
        [SitkaFileExtensions("jpg|jpeg|gif|png")]
        public HttpPostedFileBase Photo { get; set; }

        [DisplayName("Caption")]
        public string Caption { get; set; }
    }
}
