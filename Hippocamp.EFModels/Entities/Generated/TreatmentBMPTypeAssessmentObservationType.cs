using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("TreatmentBMPTypeAssessmentObservationType")]
    [Index("TreatmentBMPTypeAssessmentObservationTypeID", "TreatmentBMPTypeID", "TreatmentBMPAssessmentObservationTypeID", Name = "AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTypeID_TreatmentBMPAssessme", IsUnique = true)]
    [Index("TreatmentBMPTypeID", "TreatmentBMPAssessmentObservationTypeID", Name = "AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeID_TreatmentBMPAssessmentObservationTypeID", IsUnique = true)]
    public partial class TreatmentBMPTypeAssessmentObservationType
    {
        public TreatmentBMPTypeAssessmentObservationType()
        {
            TreatmentBMPBenchmarkAndThresholdTreatmentBMP1s = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            TreatmentBMPBenchmarkAndThresholdTreatmentBMPTypeAssessmentObservationTypes = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            TreatmentBMPObservationTreatmentBMPNavigations = new HashSet<TreatmentBMPObservation>();
            TreatmentBMPObservationTreatmentBMPTypeAssessmentObservationTypes = new HashSet<TreatmentBMPObservation>();
        }

        [Key]
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        [Column(TypeName = "decimal(9, 6)")]
        public decimal? AssessmentScoreWeight { get; set; }
        public double? DefaultThresholdValue { get; set; }
        public double? DefaultBenchmarkValue { get; set; }
        public bool OverrideAssessmentScoreIfFailing { get; set; }
        public int? SortOrder { get; set; }

        [ForeignKey("TreatmentBMPAssessmentObservationTypeID")]
        [InverseProperty("TreatmentBMPTypeAssessmentObservationTypes")]
        public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; }
        [ForeignKey("TreatmentBMPTypeID")]
        [InverseProperty("TreatmentBMPTypeAssessmentObservationTypes")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholdTreatmentBMP1s { get; set; }
        [InverseProperty("TreatmentBMPTypeAssessmentObservationType")]
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholdTreatmentBMPTypeAssessmentObservationTypes { get; set; }
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservationTreatmentBMPNavigations { get; set; }
        [InverseProperty("TreatmentBMPTypeAssessmentObservationType")]
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservationTreatmentBMPTypeAssessmentObservationTypes { get; set; }
    }
}
