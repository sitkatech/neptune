using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.WaterQualityManagementPlan
{
    public class UploadWqmpsViewModel : FormViewModel
    {
        [Required]
        [DisplayName("Stormwater Jurisdiction")]
        public int StormwaterJurisdictionID { get; set; }

        [Required]
        [SitkaFileExtensions("xlsx")]
        [DisplayName("XLSX File to Import")]
        public IFormFile UploadXLSX { get; set; }

        public UploadWqmpsViewModel()
        {
        }
    }
}