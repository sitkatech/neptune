using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("PreliminarySourceIdentificationType")]
    [Index(nameof(PreliminarySourceIdentificationTypeDisplayName), Name = "AK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeDisplayName", IsUnique = true)]
    [Index(nameof(PreliminarySourceIdentificationTypeName), Name = "AK_PreliminarySourceIdentificationType_PreliminarySourceIdentificationTypeName", IsUnique = true)]
    public partial class PreliminarySourceIdentificationType
    {
        public PreliminarySourceIdentificationType()
        {
            OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes = new HashSet<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType>();
        }

        [Key]
        public int PreliminarySourceIdentificationTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string PreliminarySourceIdentificationTypeName { get; set; }
        [Required]
        [StringLength(500)]
        public string PreliminarySourceIdentificationTypeDisplayName { get; set; }
        public int PreliminarySourceIdentificationCategoryID { get; set; }

        [ForeignKey(nameof(PreliminarySourceIdentificationCategoryID))]
        [InverseProperty("PreliminarySourceIdentificationTypes")]
        public virtual PreliminarySourceIdentificationCategory PreliminarySourceIdentificationCategory { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType.PreliminarySourceIdentificationType))]
        public virtual ICollection<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes { get; set; }
    }
}
