using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.LandUseBlockGeometry
{
    public class DownloadLandUseBlockGeometryViewModel : FormViewModel
    {
        public int PersonID { get; set; }
        [Required]
        [DisplayName("Stormwater Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }
    }
}
