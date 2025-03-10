using Microsoft.AspNetCore.Http;
using System;

namespace Neptune.Models.DataTransferObjects;

public class OnlandVisualTrashAssessmentObservationUpsertDto
{
    public int? OnlandVisualTrashAssessmentObservationID { get; set; }
    public int OnlandVisualTrashAssessmentID { get; set; }
    public string Note { get; set; }
    public DateTime? ObservationDatetime { get; set; }
    public int? FileResourceID { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
}