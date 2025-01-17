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
    public int StormwaterJurisdictionID { get; set; }
}