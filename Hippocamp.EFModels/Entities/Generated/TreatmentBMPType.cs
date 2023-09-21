using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPType")]
    [Index("TreatmentBMPTypeName", Name = "AK_TreatmentBMPType_TreatmentBMPTypeName", IsUnique = true)]
    public partial class TreatmentBMPType
    {
        public TreatmentBMPType()
        {
            CustomAttributes = new HashSet<CustomAttribute>();
            MaintenanceRecordObservations = new HashSet<MaintenanceRecordObservation>();
            MaintenanceRecords = new HashSet<MaintenanceRecord>();
            QuickBMPs = new HashSet<QuickBMP>();
            TreatmentBMPAssessments = new HashSet<TreatmentBMPAssessment>();
            TreatmentBMPBenchmarkAndThresholds = new HashSet<TreatmentBMPBenchmarkAndThreshold>();
            TreatmentBMPObservations = new HashSet<TreatmentBMPObservation>();
            TreatmentBMPTypeAssessmentObservationTypes = new HashSet<TreatmentBMPTypeAssessmentObservationType>();
            TreatmentBMPTypeCustomAttributeTypes = new HashSet<TreatmentBMPTypeCustomAttributeType>();
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int TreatmentBMPTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string TreatmentBMPTypeName { get; set; }
        [Required]
        [StringLength(1000)]
        [Unicode(false)]
        public string TreatmentBMPTypeDescription { get; set; }
        public bool IsAnalyzedInModelingModule { get; set; }
        public int? TreatmentBMPModelingTypeID { get; set; }

        [ForeignKey("TreatmentBMPModelingTypeID")]
        [InverseProperty("TreatmentBMPTypes")]
        public virtual TreatmentBMPModelingType TreatmentBMPModelingType { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<QuickBMP> QuickBMPs { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; }
        [InverseProperty("TreatmentBMPType")]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
