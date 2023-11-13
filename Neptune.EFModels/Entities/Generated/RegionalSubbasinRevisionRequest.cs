using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("RegionalSubbasinRevisionRequest")]
[Index("RegionalSubbasinRevisionRequestGeometry", Name = "SPATIAL_RegionalSubbasinRevisionRequest_RegionalSubbasinRevisionRequestGeometry")]
public partial class RegionalSubbasinRevisionRequest
{
    [Key]
    public int RegionalSubbasinRevisionRequestID { get; set; }

    public int TreatmentBMPID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry RegionalSubbasinRevisionRequestGeometry { get; set; } = null!;

    public int RequestPersonID { get; set; }

    public int RegionalSubbasinRevisionRequestStatusID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RequestDate { get; set; }

    public int? ClosedByPersonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ClosedDate { get; set; }

    [Unicode(false)]
    public string? Notes { get; set; }

    [Unicode(false)]
    public string? CloseNotes { get; set; }

    [ForeignKey("ClosedByPersonID")]
    [InverseProperty("RegionalSubbasinRevisionRequestClosedByPeople")]
    public virtual Person? ClosedByPerson { get; set; }

    [ForeignKey("RequestPersonID")]
    [InverseProperty("RegionalSubbasinRevisionRequestRequestPeople")]
    public virtual Person RequestPerson { get; set; } = null!;

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("RegionalSubbasinRevisionRequests")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;
}
