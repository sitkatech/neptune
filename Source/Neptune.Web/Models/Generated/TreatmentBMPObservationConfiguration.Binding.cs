//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPObservation]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPObservationConfiguration : EntityTypeConfiguration<TreatmentBMPObservation>
    {
        public TreatmentBMPObservationConfiguration() : this("dbo"){}

        public TreatmentBMPObservationConfiguration(string schema)
        {
            ToTable("TreatmentBMPObservation", schema);
            HasKey(x => x.TreatmentBMPObservationID);
            Property(x => x.TreatmentBMPObservationID).HasColumnName(@"TreatmentBMPObservationID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPAssessmentID).HasColumnName(@"TreatmentBMPAssessmentID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeAssessmentObservationTypeID).HasColumnName(@"TreatmentBMPTypeAssessmentObservationTypeID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPAssessmentObservationTypeID).HasColumnName(@"TreatmentBMPAssessmentObservationTypeID").HasColumnType("int").IsRequired();
            Property(x => x.ObservationData).HasColumnName(@"ObservationData").HasColumnType("nvarchar").IsRequired();

            // Foreign keys
            HasRequired(a => a.TreatmentBMPAssessment).WithMany(b => b.TreatmentBMPObservations).HasForeignKey(c => c.TreatmentBMPAssessmentID).WillCascadeOnDelete(false); // FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID
            HasRequired(a => a.TreatmentBMPTypeAssessmentObservationType).WithMany(b => b.TreatmentBMPObservations).HasForeignKey(c => c.TreatmentBMPTypeAssessmentObservationTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPObservation_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID
            HasRequired(a => a.TreatmentBMPType).WithMany(b => b.TreatmentBMPObservations).HasForeignKey(c => c.TreatmentBMPTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPObservation_TreatmentBMPType_TreatmentBMPTypeID
            HasRequired(a => a.TreatmentBMPAssessmentObservationType).WithMany(b => b.TreatmentBMPObservations).HasForeignKey(c => c.TreatmentBMPAssessmentObservationTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPObservation_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID
        }
    }
}