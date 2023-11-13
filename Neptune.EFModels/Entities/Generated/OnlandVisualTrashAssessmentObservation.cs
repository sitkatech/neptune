using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("OnlandVisualTrashAssessmentObservation")]
[Index("LocationPoint", Name = "SPATIAL_OnlandVisualTrashAssessmentObservation_LocationPoint")]
[Index("LocationPoint4326", Name = "SPATIAL_OnlandVisualTrashAssessmentObservation_LocationPoint4326")]
public partial class OnlandVisualTrashAssessmentObservation
{
    [Key]
    public int OnlandVisualTrashAssessmentObservationID { get; set; }

    public int OnlandVisualTrashAssessmentID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry LocationPoint { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Note { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ObservationDatetime { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? LocationPoint4326 { get; set; }

    [ForeignKey("OnlandVisualTrashAssessmentID")]
    [InverseProperty("OnlandVisualTrashAssessmentObservations")]
    public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; } = null!;

    [InverseProperty("OnlandVisualTrashAssessmentObservation")]
    public virtual ICollection<OnlandVisualTrashAssessmentObservationPhoto> OnlandVisualTrashAssessmentObservationPhotos { get; set; } = new List<OnlandVisualTrashAssessmentObservationPhoto>();
}
