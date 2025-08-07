using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Neptune.EFModels.Entities;

[Keyless]
public partial class vGeoServerLoadGeneratingUnit
{
    public int LoadGeneratingUnitID { get; set; }

    [Column(TypeName = "geometry")]
    public Geometry? LoadGeneratingUnitGeometry4326 { get; set; }

    public int? ModelBasinID { get; set; }

    public int? RegionalSubbasinID { get; set; }

    public int? DelineationID { get; set; }

    public int? WaterQualityManagementPlanID { get; set; }

    public bool? IsEmptyResponseFromHRUService { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? DateHRURequested { get; set; }
}
