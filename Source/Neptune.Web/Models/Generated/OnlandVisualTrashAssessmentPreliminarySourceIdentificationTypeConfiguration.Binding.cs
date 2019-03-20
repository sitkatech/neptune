//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeConfiguration : EntityTypeConfiguration<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType>
    {
        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeConfiguration() : this("dbo"){}

        public OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeConfiguration(string schema)
        {
            ToTable("OnlandVisualTrashAssessmentPreliminarySourceIdentificationType", schema);
            HasKey(x => x.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID);
            Property(x => x.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID).HasColumnName(@"OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OnlandVisualTrashAssessmentID).HasColumnName(@"OnlandVisualTrashAssessmentID").HasColumnType("int").IsRequired();
            Property(x => x.PreliminarySourceIdentificationTypeID).HasColumnName(@"PreliminarySourceIdentificationTypeID").HasColumnType("int").IsRequired();
            Property(x => x.ExplanationIfTypeIsOther).HasColumnName(@"ExplanationIfTypeIsOther").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.OnlandVisualTrashAssessment).WithMany(b => b.OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes).HasForeignKey(c => c.OnlandVisualTrashAssessmentID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID
        }
    }
}