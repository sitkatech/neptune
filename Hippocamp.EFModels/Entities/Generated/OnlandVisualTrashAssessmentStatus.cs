using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentStatus")]
    [Index("OnlandVisualTrashAssessmentStatusDisplayName", Name = "AK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusDisplayName", IsUnique = true)]
    [Index("OnlandVisualTrashAssessmentStatusName", Name = "AK_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusName", IsUnique = true)]
    public partial class OnlandVisualTrashAssessmentStatus
    {
        public OnlandVisualTrashAssessmentStatus()
        {
            OnlandVisualTrashAssessments = new HashSet<OnlandVisualTrashAssessment>();
        }

        [Key]
        public int OnlandVisualTrashAssessmentStatusID { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string OnlandVisualTrashAssessmentStatusName { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string OnlandVisualTrashAssessmentStatusDisplayName { get; set; }

        [InverseProperty("OnlandVisualTrashAssessmentStatus")]
        public virtual ICollection<OnlandVisualTrashAssessment> OnlandVisualTrashAssessments { get; set; }
    }
}
