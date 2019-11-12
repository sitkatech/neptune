using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class UploadTreatmentBMPsViewModel : FormViewModel
    {
        [Required]
        [SitkaFileExtensions("csv")]
        [DisplayName("CSV File to Import")]
        public HttpPostedFileBase UploadCSV { get; set; }

        [Required]
        [DisplayName("BMP Type")]
        public int? BMPType { get; set; }

        public UploadTreatmentBMPsViewModel()
        {
        }

    }




}