using System.ComponentModel.DataAnnotations;

namespace Neptune.Common.Services.GDAL;

public class GenerateProjectLoadGeneratingUnitRequestDto
{
    [Required]
    public int ProjectID { get; set; }

    [Required] 
    public List<int> RegionalSubbasinIDs { get; set; }
}