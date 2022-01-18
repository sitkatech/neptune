using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentArea")]
    [Index(nameof(OnlandVisualTrashAssessmentAreaID), nameof(StormwaterJurisdictionID), Name = "AK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID_StormwaterJurisdictionID", IsUnique = true)]
    [Index(nameof(OnlandVisualTrashAssessmentAreaName), nameof(StormwaterJurisdictionID), Name = "AK_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaName_StormwaterJurisdictionID", IsUnique = true)]
    [Index(nameof(OnlandVisualTrashAssessmentAreaGeometry), Name = "SPATIAL_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaGeometry")]
    [Index(nameof(TransectLine), Name = "SPATIAL_OnlandVisualTrashAssessmentArea_TransectLine")]
    public partial class OnlandVisualTrashAssessmentArea
    {
        public OnlandVisualTrashAssessmentArea()
        {
            OnlandVisualTrashAssessmentOnlandVisualTrashAssessmentAreaNavigations = new HashSet<OnlandVisualTrashAssessment>();
        }

        [Key]
        public int OnlandVisualTrashAssessmentAreaID { get; set; }
        [Required]
        [StringLength(100)]
        public string OnlandVisualTrashAssessmentAreaName { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry OnlandVisualTrashAssessmentAreaGeometry { get; set; }
        public int? OnlandVisualTrashAssessmentBaselineScoreID { get; set; }
        [StringLength(500)]
        public string AssessmentAreaDescription { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry TransectLine { get; set; }
        public int? OnlandVisualTrashAssessmentProgressScoreID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry OnlandVisualTrashAssessmentAreaGeometry4326 { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry TransectLine4326 { get; set; }

        [ForeignKey(nameof(OnlandVisualTrashAssessmentBaselineScoreID))]
        [InverseProperty(nameof(OnlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentBaselineScores))]
        public virtual OnlandVisualTrashAssessmentScore OnlandVisualTrashAssessmentBaselineScore { get; set; }
        [ForeignKey(nameof(OnlandVisualTrashAssessmentProgressScoreID))]
        [InverseProperty(nameof(OnlandVisualTrashAssessmentScore.OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentProgressScores))]
        public virtual OnlandVisualTrashAssessmentScore OnlandVisualTrashAssessmentProgressScore { get; set; }
        [ForeignKey(nameof(StormwaterJurisdictionID))]
        [InverseProperty("OnlandVisualTrashAssessmentAreas")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea))]
        public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessmentOnlandVisualTrashAssessmentArea { get; set; }
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessmentOnlandVisualTrashAssessmentAreaNavigations { get; set; }
    }
}
