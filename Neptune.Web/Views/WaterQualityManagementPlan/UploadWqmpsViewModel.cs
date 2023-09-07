using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.Mvc;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class UploadWqmpsViewModel : FormViewModel
    {
        [Required]
        [SitkaFileExtensions("csv")]
        [DisplayName("CSV File to Import")]
        public IFormFile UploadCSV { get; set; }

        public UploadWqmpsViewModel()
        {
        }
    }
}