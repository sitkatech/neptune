using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("DelineationStaging")]
[Index("TreatmentBMPName", "StormwaterJurisdictionID", "UploadedByPersonID", Name = "AK_DelineationStaging_TreatmentBMPName_StormwaterJurisdictionID", IsUnique = true)]
[Index("Geometry", Name = "SPATIAL_DelineationStaging_Geometry")]
public partial class DelineationStaging
{
    [Key]
    public int DelineationStagingID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry Geometry { get; set; } = null!;

    public int UploadedByPersonID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? TreatmentBMPName { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string? IsVerified { get; set; }

    [ForeignKey("StormwaterJurisdictionID")]
    [InverseProperty("DelineationStagings")]
    public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; } = null!;

    [ForeignKey("UploadedByPersonID")]
    [InverseProperty("DelineationStagings")]
    public virtual Person UploadedByPerson { get; set; } = null!;
}
