using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentPreliminarySourceIdentificationType")]
    [Index(nameof(OnlandVisualTrashAssessmentID), nameof(PreliminarySourceIdentificationTypeID), Name = "AK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessmentID_PreliminarySourceIdentificationT", IsUnique = true)]
    public partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationType
    {
        [Key]
        public int OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID { get; set; }
        public int OnlandVisualTrashAssessmentID { get; set; }
        public int PreliminarySourceIdentificationTypeID { get; set; }
        [StringLength(500)]
        public string ExplanationIfTypeIsOther { get; set; }

        [ForeignKey(nameof(OnlandVisualTrashAssessmentID))]
        [InverseProperty("OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes")]
        public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; }
        [ForeignKey(nameof(PreliminarySourceIdentificationTypeID))]
        [InverseProperty("OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes")]
        public virtual PreliminarySourceIdentificationType PreliminarySourceIdentificationType { get; set; }
    }
}
