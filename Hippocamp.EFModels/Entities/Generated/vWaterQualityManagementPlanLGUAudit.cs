﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vWaterQualityManagementPlanLGUAudit
    {
        public int PrimaryKey { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        [Required]
        [StringLength(100)]
        public string WaterQualityManagementPlanName { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry WaterQualityManagementPlanBoundary { get; set; }
        public bool? LoadGeneratingUnitsPopulated { get; set; }
        public bool? BoundaryIsDefined { get; set; }
        public int CountOfIntersectingLSPCBasins { get; set; }
    }
}
