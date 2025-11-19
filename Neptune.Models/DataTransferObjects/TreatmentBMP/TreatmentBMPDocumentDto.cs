using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Neptune.Models.DataTransferObjects;

public class TreatmentBMPDocumentDto
{
    public int TreatmentBMPDocumentID { get; set; }
    public int FileResourceID { get; set; }
    public string FileResourceGUID { get; set; }
    public string OriginalBaseFilename { get; set; }
    public string OriginalFileExtension { get; set; }
    public string? DocumentDescription { get; set; }
    public string? DisplayName { get; set; }
    public DateTime CreateDate { get; set; }
}

public class TreatmentBMPDocumentCreateDto
{
    [Required]
    public IFormFile File { get; set; }
    public string? Description { get; set; }
}

public class TreatmentBMPDocumentUpdateDto
{
    public string? Description { get; set; }
}