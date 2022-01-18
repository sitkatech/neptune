using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPType")]
    [Index(nameof(TreatmentBMPTypeName), Name = "AK_TreatmentBMPType_TreatmentBMPTypeName", IsUnique = true)]
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
        public string TreatmentBMPTypeName { get; set; }
        [Required]
        [StringLength(1000)]
        public string TreatmentBMPTypeDescription { get; set; }
        public bool IsAnalyzedInModelingModule { get; set; }
        public int? TreatmentBMPModelingTypeID { get; set; }

        [ForeignKey(nameof(TreatmentBMPModelingTypeID))]
        [InverseProperty("TreatmentBMPTypes")]
        public virtual TreatmentBMPModelingType TreatmentBMPModelingType { get; set; }
        [InverseProperty(nameof(CustomAttribute.TreatmentBMPType))]
        public virtual ICollection<CustomAttribute> CustomAttributes { get; set; }
        [InverseProperty(nameof(MaintenanceRecordObservation.TreatmentBMPType))]
        public virtual ICollection<MaintenanceRecordObservation> MaintenanceRecordObservations { get; set; }
        [InverseProperty(nameof(MaintenanceRecord.TreatmentBMPType))]
        public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        [InverseProperty(nameof(QuickBMP.TreatmentBMPType))]
        public virtual ICollection<QuickBMP> QuickBMPs { get; set; }
        [InverseProperty(nameof(TreatmentBMPAssessment.TreatmentBMPType))]
        public virtual ICollection<TreatmentBMPAssessment> TreatmentBMPAssessments { get; set; }
        [InverseProperty(nameof(TreatmentBMPBenchmarkAndThreshold.TreatmentBMPType))]
        public virtual ICollection<TreatmentBMPBenchmarkAndThreshold> TreatmentBMPBenchmarkAndThresholds { get; set; }
        [InverseProperty(nameof(TreatmentBMPObservation.TreatmentBMPType))]
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservations { get; set; }
        [InverseProperty(nameof(TreatmentBMPTypeAssessmentObservationType.TreatmentBMPType))]
        public virtual ICollection<TreatmentBMPTypeAssessmentObservationType> TreatmentBMPTypeAssessmentObservationTypes { get; set; }
        [InverseProperty(nameof(TreatmentBMPTypeCustomAttributeType.TreatmentBMPType))]
        public virtual ICollection<TreatmentBMPTypeCustomAttributeType> TreatmentBMPTypeCustomAttributeTypes { get; set; }
        [InverseProperty(nameof(TreatmentBMP.TreatmentBMPType))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
