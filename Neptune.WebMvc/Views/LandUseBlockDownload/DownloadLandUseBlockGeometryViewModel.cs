using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.LandUseBlockDownload
{
    public class DownloadLandUseBlockGeometryViewModel : FormViewModel
    {
        public int PersonID { get; set; }
        [Required]
        [DisplayName("Stormwater Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }
    }
}
