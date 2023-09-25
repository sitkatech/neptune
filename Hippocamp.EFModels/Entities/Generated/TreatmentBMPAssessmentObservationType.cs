using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("TreatmentBMPAssessmentObservationType")]
    [Index("TreatmentBMPAssessmentObservationTypeName", Name = "AK_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeName", IsUnique = true)]
    public partial class TreatmentBMPAssessmentObservationType
    {
        public TreatmentBMPAssessmentObservationType()
        {
            TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
            TreatmentBMPTypeAssessmentObservationTypes = new HashSet<TreatmentBMPTypeAssessmentObservationType>();
        }

        [Key]
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string TreatmentBMPAssessmentObservationTypeName { get; set; }
        public int ObservationTypeSpecificationID { get; set; }
        [Required]
        public string TreatmentBMPAssessmentObservationTypeSchema { get; set; }

        [ForeignKey("ObservationTypeSpecificationID")]
        [InverseProperty("TreatmentBMPAssessmentObservationTypes")]
        public virtual ObservationTypeSpecification ObservationTypeSpecification { get; set; }
        [InverseProperty("TreatmentBMPAssessmentObservationType")]
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        [InverseProperty("TreatmentBMPAssessmentObservationType")]
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        [InverseProperty("TreatmentBMPAssessmentObservationType")]
        public virtual ICollection<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get; set; }
    }
}
