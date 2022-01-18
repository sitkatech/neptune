using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentScore")]
    [Index(nameof(OnlandVisualTrashAssessmentScoreDisplayName), Name = "AK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreDisplayName", IsUnique = true)]
    [Index(nameof(OnlandVisualTrashAssessmentScoreName), Name = "AK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreName", IsUnique = true)]
    public partial class OnlandVisualTrashAssessmentScore
    {
        public OnlandVisualTrashAssessmentScore()
        {
            OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentBaselineScores = new HashSet<OnlandVisualTrashAssessmentArea>();
            OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentProgressScores = new HashSet<OnlandVisualTrashAssessmentArea>();
            OnlandVisualTrashAssessments = new HashSet<OnlandVisualTrashAssessment>();
        }

        [Key]
        public int OnlandVisualTrashAssessmentScoreID { get; set; }
        [StringLength(1)]
        public string OnlandVisualTrashAssessmentScoreName { get; set; }
        [StringLength(1)]
        public string OnlandVisualTrashAssessmentScoreDisplayName { get; set; }
        public int NumericValue { get; set; }
        [Column(TypeName = "decimal(4, 1)")]
        public decimal TrashGenerationRate { get; set; }

        [InverseProperty(nameof(OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentBaselineScore))]
        public virtual ICollection<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentBaselineScores { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentProgressScore))]
        public virtual ICollection<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentProgressScores { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentScore))]
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
    }
}
