﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Table("LandUseBlockStaging")]
public partial class LandUseBlockStaging
{
    [Key]
    public int LandUseBlockStagingID { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? PriorityLandUseType { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? LandUseDescription { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry Geometry { get; set; } = null!;

    [Column(TypeName = "decimal(4, 1)")]
    public decimal? TrashGenerationRate { get; set; }

    [StringLength(80)]
    [Unicode(false)]
    public string? LandUseForTGR { get; set; }

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? MedianHouseholdIncomeResidential { get; set; }

    [Column(TypeName = "numeric(18, 0)")]
    public decimal? MedianHouseholdIncomeRetail { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? PermitType { get; set; }

    public int UploadedByPersonID { get; set; }

    [ForeignKey("StormwaterJurisdictionID")]
    [InverseProperty("LandUseBlockStagings")]
    public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; } = null!;

    [ForeignKey("UploadedByPersonID")]
    [InverseProperty("LandUseBlockStagings")]
    public virtual Person UploadedByPerson { get; set; } = null!;
}
