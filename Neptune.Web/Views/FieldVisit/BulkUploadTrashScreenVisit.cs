using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.FieldVisit
{
    public abstract class BulkUploadTrashScreenVisit : TypedWebViewPage<BulkUploadTrashScreenVisitViewData, BulkUploadTrashScreenVisitViewModel>
    {
    }

    public class BulkUploadTrashScreenVisitViewModel : FormViewModel
    {
        [Required]
        [SitkaFileExtensions("xlsx")]
        [DisplayName("XLSX File to Import")]
        public IFormFile UploadXLSX { get; set; }

        public BulkUploadTrashScreenVisitViewModel()
        {

        }
    }
}