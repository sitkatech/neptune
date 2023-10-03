using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.FieldVisit
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