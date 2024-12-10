using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.DelineationGeometry
{
    public class UpdateDelineationGeometryViewModel : FormViewModel
    {
        [Required]
        [DisplayName("Zipped File Geodatabase to Upload")]
        [SitkaFileExtensions("zip")]
        public IFormFile FileResourceData { get; set; }

        [Required]
        [DisplayName("Treatment BMP Name Field")]
        public string TreatmentBMPNameField { get; set; }

        [Required]
        [DisplayName("Stormwater Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }
    }
}
