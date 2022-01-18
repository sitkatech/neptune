using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPAssessmentType")]
    [Index(nameof(TreatmentBMPAssessmentTypeDisplayName), Name = "AK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeDisplayName", IsUnique = true)]
    [Index(nameof(TreatmentBMPAssessmentTypeName), Name = "AK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeName", IsUnique = true)]
    public partial class TreatmentBMPAssessmentType
    {
        public TreatmentBMPAssessmentType()
        {
            TreatmentBMPAssessments = new HashSet<TreatmentBMPAssessment>();
        }

        [Key]
        public int TreatmentBMPAssessmentTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string TreatmentBMPAssessmentTypeName { get; set; }
        [Required]
        [StringLength(50)]
        public string TreatmentBMPAssessmentTypeDisplayName { get; set; }

        [InverseProperty(nameof(TreatmentBMPAssessment.TreatmentBMPAssessmentType))]
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
    }
}
