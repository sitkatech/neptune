using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentPreliminarySourceIdentificationType")]
    [Index("OnlandVisualTrashAssessmentID", "PreliminarySourceIdentificationTypeID", Name = "AK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessmentID_PreliminarySourceIdentificationT", IsUnique = true)]
    public partial class OnlandVisualTrashAssessmentPreliminarySourceIdentificationType
    {
        [Key]
        public int OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID { get; set; }
        public int OnlandVisualTrashAssessmentID { get; set; }
        public int PreliminarySourceIdentificationTypeID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string ExplanationIfTypeIsOther { get; set; }

        [ForeignKey("OnlandVisualTrashAssessmentID")]
        [InverseProperty("OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes")]
        public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; }
        [ForeignKey("PreliminarySourceIdentificationTypeID")]
        [InverseProperty("OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes")]
        public virtual PreliminarySourceIdentificationType PreliminarySourceIdentificationType { get; set; }
    }
}
