using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("OnlandVisualTrashAssessmentAreaStaging")]
[Index("AreaName", "StormwaterJurisdictionID", Name = "AK_OnlandVisualTrashAssessmentAreaStaging_OnlandVisualTrashAssessmentAreaStagingName_StormwaterJurisdictionID", IsUnique = true)]
public partial class OnlandVisualTrashAssessmentAreaStaging
{
    [Key]
    public int OnlandVisualTrashAssessmentAreaStagingID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string AreaName { get; set; } = null!;

    public int StormwaterJurisdictionID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry Geometry { get; set; } = null!;

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    public int UploadedByPersonID { get; set; }

    [ForeignKey("StormwaterJurisdictionID")]
    [InverseProperty("OnlandVisualTrashAssessmentAreaStagings")]
    public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; } = null!;

    [ForeignKey("UploadedByPersonID")]
    [InverseProperty("OnlandVisualTrashAssessmentAreaStagings")]
    public virtual Person UploadedByPerson { get; set; } = null!;
}
