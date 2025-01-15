using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Common.Mvc;

namespace Neptune.WebMvc.Views.OnlandVisualTrashAssessmentArea;

public class BulkUploadOVTAAreasViewModel : FormViewModel
{
    [Required]
    [SitkaFileExtensions("xlsx")]
    [DisplayName("XLSX File to Import")]
    public IFormFile UploadXLSX { get; set; }

    [Required]
    public int StormwaterJurisdictionID { get; set; }
}