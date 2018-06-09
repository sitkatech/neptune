using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using LtInfo.Common;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.Shared.ManagePhotosWithPreview
{
    public abstract class ManagePhotosWithPreviewViewModel : FormViewModel
    {
        public virtual IList<ManagePhotoWithPreviewPhotoSimple> PhotoSimples { get; set; }

        [DisplayName("Upload a photo")]
        [SitkaFileExtensions("jpg|jpeg|gif|png")]
        public virtual HttpPostedFileBase Photo { get; set; }

        [DisplayName("Caption")]
        public virtual string Caption { get; set; }
    }
}
