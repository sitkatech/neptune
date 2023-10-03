using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.TreatmentBMP
{
    public class UploadTreatmentBMPsViewModel : FormViewModel
    {
        [Required]
        [SitkaFileExtensions("csv")]
        [DisplayName("CSV File to Import")]
        public IFormFile UploadCSV { get; set; }

        [Required]
        [DisplayName("BMP Type")]
        public int? TreatmentBMPTypeID { get; set; }

        public UploadTreatmentBMPsViewModel()
        {
        }
    }
}