//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeAssessmentObservationType]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeAssessmentObservationTypeConfiguration : EntityTypeConfiguration<TreatmentBMPTypeAssessmentObservationType>
    {
        public TreatmentBMPTypeAssessmentObservationTypeConfiguration() : this("dbo"){}

        public TreatmentBMPTypeAssessmentObservationTypeConfiguration(string schema)
        {
            ToTable("TreatmentBMPTypeAssessmentObservationType", schema);
            HasKey(x => x.TreatmentBMPTypeAssessmentObservationTypeID);
            Property(x => x.TreatmentBMPTypeAssessmentObservationTypeID).HasColumnName(@"TreatmentBMPTypeAssessmentObservationTypeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPAssessmentObservationTypeID).HasColumnName(@"TreatmentBMPAssessmentObservationTypeID").HasColumnType("int").IsRequired();
            Property(x => x.AssessmentScoreWeight).HasColumnName(@"AssessmentScoreWeight").HasColumnType("decimal").IsOptional().HasPrecision(9,6);
            Property(x => x.DefaultThresholdValue).HasColumnName(@"DefaultThresholdValue").HasColumnType("float").IsOptional();
            Property(x => x.DefaultBenchmarkValue).HasColumnName(@"DefaultBenchmarkValue").HasColumnType("float").IsOptional();
            Property(x => x.OverrideAssessmentScoreIfFailing).HasColumnName(@"OverrideAssessmentScoreIfFailing").HasColumnType("bit").IsRequired();
            Property(x => x.SortOrder).HasColumnName(@"SortOrder").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.TreatmentBMPType).WithMany(b => b.TreatmentBMPTypeAssessmentObservationTypes).HasForeignKey(c => c.TreatmentBMPTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID
            HasRequired(a => a.TreatmentBMPAssessmentObservationType).WithMany(b => b.TreatmentBMPTypeAssessmentObservationTypes).HasForeignKey(c => c.TreatmentBMPAssessmentObservationTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID
        }
    }
}