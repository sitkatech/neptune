using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.WaterQualityManagementPlan
{
    public class UploadWqmpsViewModel : FormViewModel
    {
        [Required]
        [SitkaFileExtensions("csv")]
        [DisplayName("CSV File to Import")]
        public HttpPostedFileBase UploadCSV { get; set; }

        public UploadWqmpsViewModel()
        {
        }
    }
}