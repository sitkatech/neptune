//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessment]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentConfiguration : EntityTypeConfiguration<TreatmentBMPAssessment>
    {
        public TreatmentBMPAssessmentConfiguration() : this("dbo"){}

        public TreatmentBMPAssessmentConfiguration(string schema)
        {
            ToTable("TreatmentBMPAssessment", schema);
            HasKey(x => x.TreatmentBMPAssessmentID);
            Property(x => x.TreatmentBMPAssessmentID).HasColumnName(@"TreatmentBMPAssessmentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.FieldVisitID).HasColumnName(@"FieldVisitID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPAssessmentTypeID).HasColumnName(@"TreatmentBMPAssessmentTypeID").HasColumnType("int").IsRequired();
            Property(x => x.Notes).HasColumnName(@"Notes").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(1000);

            // Foreign keys
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.TreatmentBMPAssessments).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID
            HasRequired(a => a.TreatmentBMPType).WithMany(b => b.TreatmentBMPAssessments).HasForeignKey(c => c.TreatmentBMPTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPAssessment_TreatmentBMPType_TreatmentBMPTypeID
            HasRequired(a => a.FieldVisit).WithMany(b => b.TreatmentBMPAssessments).HasForeignKey(c => c.FieldVisitID).WillCascadeOnDelete(false); // FK_TreatmentBMPAssessment_FieldVisit_FieldVisitID
        }
    }
}