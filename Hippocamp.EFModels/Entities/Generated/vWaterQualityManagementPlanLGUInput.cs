using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vWaterQualityManagementPlanLGUInput
    {
        public int WQMPID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry WaterQualityManagementPlanBoundary { get; set; }
    }
}
