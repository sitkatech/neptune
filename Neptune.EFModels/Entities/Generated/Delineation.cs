using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("Delineation")]
[Index("TreatmentBMPID", Name = "AK_Delineation_TreatmentBMPID", IsUnique = true)]
[Index("DelineationGeometry", Name = "SPATIAL_Delineation_DelineationGeometry")]
public partial class Delineation
{
    [Key]
    public int DelineationID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry DelineationGeometry { get; set; } = null!;

    public int DelineationTypeID { get; set; }

    public bool IsVerified { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateLastVerified { get; set; }

    public int? VerifiedByPersonID { get; set; }

    public int TreatmentBMPID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateLastModified { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? DelineationGeometry4326 { get; set; }

    public bool HasDiscrepancies { get; set; }

    [InverseProperty("Delineation")]
    public virtual ICollection<DelineationOverlap> DelineationOverlapDelineations { get; set; } = new List<DelineationOverlap>();

    [InverseProperty("OverlappingDelineation")]
    public virtual ICollection<DelineationOverlap> DelineationOverlapOverlappingDelineations { get; set; } = new List<DelineationOverlap>();

    [InverseProperty("Delineation")]
    public virtual ICollection<DirtyModelNode> DirtyModelNodes { get; set; } = new List<DirtyModelNode>();

    [InverseProperty("Delineation")]
    public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; } = new List<LoadGeneratingUnit>();

    [InverseProperty("Delineation")]
    public virtual ICollection<NereidResult> NereidResults { get; set; } = new List<NereidResult>();

    [InverseProperty("Delineation")]
    public virtual ICollection<ProjectLoadGeneratingUnit> ProjectLoadGeneratingUnits { get; set; } = new List<ProjectLoadGeneratingUnit>();

    [InverseProperty("Delineation")]
    public virtual ICollection<ProjectNereidResult> ProjectNereidResults { get; set; } = new List<ProjectNereidResult>();

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("Delineation")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    [ForeignKey("VerifiedByPersonID")]
    [InverseProperty("Delineations")]
    public virtual Person? VerifiedByPerson { get; set; }
}
