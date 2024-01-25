using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vProjectLoadGeneratingUnitUpdateCandidate
{
    public int ProjectID { get; set; }

    public int SpatialGridUnitID { get; set; }

    public int ProjectLoadGeneratingUnitID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry ProjectLoadGeneratingUnitGeometry { get; set; } = null!;

    public bool? IsEmptyResponseFromHRUService { get; set; }

    public int? ModelBasinID { get; set; }

    public int? RegionalSubbasinID { get; set; }

    public int? WaterQualityManagementPlanID { get; set; }

    public int? DelineationID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateHRURequested { get; set; }
}
