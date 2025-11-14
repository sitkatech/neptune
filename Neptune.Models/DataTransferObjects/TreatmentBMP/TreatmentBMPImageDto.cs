using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPImageDto
{
    public int TreatmentBMPImageID { get; set; }
    public int TreatmentBMPID { get; set; }
    public int FileResourceID { get; set; }
    public string FileResourceGUID { get; set; }
    public string? Caption { get; set; }
    public DateOnly UploadDate { get; set; }
}

public class TreatmentBMPImageCreateDto : TreatmentBMPImageUpdateDto
{
    [Required]
    public IFormFile File { get; set; }
}

public class TreatmentBMPImageUpdateDto
{
    public string? Caption { get; set; }
}