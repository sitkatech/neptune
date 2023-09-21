using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vWaterQualityManagementPlanTGUInput
    {
        public int PrimaryKey { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry WaterQualityManagementPlanGeometry { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string OrganizationName { get; set; }
        public int TrashCaptureEffectiveness { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string TrashCaptureStatusTypeDisplayName { get; set; }
    }
}
