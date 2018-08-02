//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPBenchmarkAndThreshold]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPBenchmarkAndThresholdConfiguration : EntityTypeConfiguration<TreatmentBMPBenchmarkAndThreshold>
    {
        public TreatmentBMPBenchmarkAndThresholdConfiguration() : this("dbo"){}

        public TreatmentBMPBenchmarkAndThresholdConfiguration(string schema)
        {
            ToTable("TreatmentBMPBenchmarkAndThreshold", schema);
            HasKey(x => x.TreatmentBMPBenchmarkAndThresholdID);
            Property(x => x.TreatmentBMPBenchmarkAndThresholdID).HasColumnName(@"TreatmentBMPBenchmarkAndThresholdID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeAssessmentObservationTypeID).HasColumnName(@"TreatmentBMPTypeAssessmentObservationTypeID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPAssessmentObservationTypeID).HasColumnName(@"TreatmentBMPAssessmentObservationTypeID").HasColumnType("int").IsRequired();
            Property(x => x.BenchmarkValue).HasColumnName(@"BenchmarkValue").HasColumnType("float").IsRequired();
            Property(x => x.ThresholdValue).HasColumnName(@"ThresholdValue").HasColumnType("float").IsRequired();

            // Foreign keys
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.TreatmentBMPBenchmarkAndThresholds).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID
            HasRequired(a => a.TreatmentBMPTypeAssessmentObservationType).WithMany(b => b.TreatmentBMPBenchmarkAndThresholds).HasForeignKey(c => c.TreatmentBMPTypeAssessmentObservationTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID
            HasRequired(a => a.TreatmentBMPType).WithMany(b => b.TreatmentBMPBenchmarkAndThresholds).HasForeignKey(c => c.TreatmentBMPTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPType_TreatmentBMPTypeID
            HasRequired(a => a.TreatmentBMPAssessmentObservationType).WithMany(b => b.TreatmentBMPBenchmarkAndThresholds).HasForeignKey(c => c.TreatmentBMPAssessmentObservationTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID
        }
    }
}