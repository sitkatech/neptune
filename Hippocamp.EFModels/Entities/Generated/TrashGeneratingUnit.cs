using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TrashGeneratingUnit")]
    [Index(nameof(TrashGeneratingUnitGeometry), Name = "SPATIAL_TrashGeneratingUnit_TrashGeneratingUnitGeometry")]
    public partial class TrashGeneratingUnit
    {
        [Key]
        public int TrashGeneratingUnitID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public int? LandUseBlockID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry TrashGeneratingUnitGeometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }

        [ForeignKey(nameof(LandUseBlockID))]
        [InverseProperty("TrashGeneratingUnits")]
        public virtual LandUseBlock LandUseBlock { get; set; }
        [ForeignKey(nameof(StormwaterJurisdictionID))]
        [InverseProperty("TrashGeneratingUnits")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
    }
}
