using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("TreatmentBMPAssessmentType")]
    [Index("TreatmentBMPAssessmentTypeDisplayName", Name = "AK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeDisplayName", IsUnique = true)]
    [Index("TreatmentBMPAssessmentTypeName", Name = "AK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string TreatmentBMPAssessmentTypeName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string TreatmentBMPAssessmentTypeDisplayName { get; set; }

        [InverseProperty("TreatmentBMPAssessmentType")]
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
    }
}
