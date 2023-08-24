using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Web.Common.Models;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class UploadTreatmentBMPsViewModel : FormViewModel
    {
        [Required]
        //todo: [SitkaFileExtensions("csv")]
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