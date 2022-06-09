using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentScore")]
    [Index("OnlandVisualTrashAssessmentScoreDisplayName", Name = "AK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreDisplayName", IsUnique = true)]
    [Index("OnlandVisualTrashAssessmentScoreName", Name = "AK_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreName", IsUnique = true)]
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
        [Unicode(false)]
        public string OnlandVisualTrashAssessmentScoreName { get; set; }
        [StringLength(1)]
        [Unicode(false)]
        public string OnlandVisualTrashAssessmentScoreDisplayName { get; set; }
        public int NumericValue { get; set; }
        [Column(TypeName = "decimal(4, 1)")]
        public decimal TrashGenerationRate { get; set; }

        [InverseProperty("OnlandVisualTrashAssessmentBaselineScore")]
        public virtual ICollection<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentBaselineScores { get; set; }
        [InverseProperty("OnlandVisualTrashAssessmentProgressScore")]
        public virtual ICollection<OnlandVisualTrashAssessmentArea> OnlandVisualTrashAssessmentAreaOnlandVisualTrashAssessmentProgressScores { get; set; }
        [InverseProperty("OnlandVisualTrashAssessmentScore")]
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
    }
}
