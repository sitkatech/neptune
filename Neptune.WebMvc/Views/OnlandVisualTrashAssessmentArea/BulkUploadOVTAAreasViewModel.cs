using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea;

public class BulkUploadOVTAAreasViewModel : FormViewModel
{
    [Required]
    [DisplayName("Zipped File Geodatabase to Upload")]
    [SitkaFileExtensions("zip")]
    public IFormFile FileResourceData { get; set; }

    [Required]
    [DisplayName("Stormwater Jurisdiction")]
    public int StormwaterJurisdictionID { get; set; }

    [Required]
    [DisplayName("OVTA Area Name")]
    public string AreaName { get; set; }
}