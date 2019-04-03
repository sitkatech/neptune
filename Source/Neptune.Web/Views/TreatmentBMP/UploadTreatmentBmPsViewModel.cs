using System.Web;
using LtInfo.Common.Models;
using LtInfo.Common.Mvc;

namespace Neptune.Web.Views.TreatmentBMP
{
    public class UploadTreatmentBMPsViewModel : FormViewModel
    {
        [SitkaFileExtensions("csv|tsv|xls|xlsx")]
        public  HttpPostedFileBase UploadCSV { get; set; }

        public UploadTreatmentBMPsViewModel()
        {
        }

    }




}