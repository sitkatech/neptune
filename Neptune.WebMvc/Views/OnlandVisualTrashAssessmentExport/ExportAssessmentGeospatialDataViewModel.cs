using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentExport
{
    public class ExportAssessmentGeospatialDataViewModel : FormViewModel
    {
        [Required]
        [DisplayName("Stormwater Jurisdiction")]
        public int? StormwaterJurisdictionID { get; set; }
    }
}
