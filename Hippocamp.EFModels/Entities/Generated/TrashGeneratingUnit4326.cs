using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("TrashGeneratingUnit4326")]
    public partial class TrashGeneratingUnit4326
    {
        [Key]
        public int TrashGeneratingUnit4326ID { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        public int? LandUseBlockID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry TrashGeneratingUnit4326Geometry { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }

        [ForeignKey("LandUseBlockID")]
        [InverseProperty("TrashGeneratingUnit4326s")]
        public virtual LandUseBlock LandUseBlock { get; set; }
        [ForeignKey("OnlandVisualTrashAssessmentAreaID")]
        [InverseProperty("TrashGeneratingUnit4326s")]
        public virtual OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea { get; set; }
        [ForeignKey("StormwaterJurisdictionID")]
        [InverseProperty("TrashGeneratingUnit4326s")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [ForeignKey("WaterQualityManagementPlanID")]
        [InverseProperty("TrashGeneratingUnit4326s")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
    }
}
