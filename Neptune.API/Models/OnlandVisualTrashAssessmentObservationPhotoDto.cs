using Microsoft.AspNetCore.Http;

namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentObservationPhotoDto
{
    public IFormFile file { get; set; }
}