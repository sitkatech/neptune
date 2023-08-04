using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vGeoServerWaterQualityManagementPlan
{
    public int PrimaryKey { get; set; }

    public int WaterQualityManagementPlanID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? WaterQualityManagementPlanGeometry { get; set; }

    public int StormwaterJurisdictionID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? OrganizationName { get; set; }

    public int TrashCaptureEffectiveness { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? TrashCaptureStatusTypeDisplayName { get; set; }
}
