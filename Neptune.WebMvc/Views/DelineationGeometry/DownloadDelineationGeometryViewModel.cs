using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.DelineationGeometry
{
    public class DownloadDelineationGeometryViewModel : FormViewModel
    {
        [Required]
        [DisplayName("Stormwater Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }

        [Required]
        [DisplayName("Delineation Type")]
        public int? DelineationTypeID { get; set; }
    }
}
