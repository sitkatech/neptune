using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class UploadWqmpBoundaryFromAPNsViewModel : FormViewModel
    {
        [Required]
        [SitkaFileExtensions("csv")]
        [DisplayName("CSV File to Import")]
        public IFormFile UploadCSV { get; set; }

        [Required]
        [DisplayName("Stormwater Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }

        public UploadWqmpBoundaryFromAPNsViewModel()
        {
        }
    }
}