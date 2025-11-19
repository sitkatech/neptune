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

public class TreatmentBMPImageCreateDto
{
    [Required]
    public IFormFile File { get; set; }
    public string? Caption { get; set; }
}

public class TreatmentBMPImageUpdateDto
{
    // include the id so update DTO can be used in batch updates
    public int TreatmentBMPImageID { get; set; }
    public string? Caption { get; set; }
}