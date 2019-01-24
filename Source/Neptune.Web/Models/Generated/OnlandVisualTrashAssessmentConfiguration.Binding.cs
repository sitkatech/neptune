//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessment]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentConfiguration : EntityTypeConfiguration<OnlandVisualTrashAssessment>
    {
        public OnlandVisualTrashAssessmentConfiguration() : this("dbo"){}

        public OnlandVisualTrashAssessmentConfiguration(string schema)
        {
            ToTable("OnlandVisualTrashAssessment", schema);
            HasKey(x => x.OnlandVisualTrashAssessmentID);
            Property(x => x.OnlandVisualTrashAssessmentID).HasColumnName(@"OnlandVisualTrashAssessmentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CreatedByPersonID).HasColumnName(@"CreatedByPersonID").HasColumnType("int").IsRequired();
            Property(x => x.CreatedDate).HasColumnName(@"CreatedDate").HasColumnType("datetime").IsRequired();
            Property(x => x.OnlandVisualTrashAssessmentAreaID).HasColumnName(@"OnlandVisualTrashAssessmentAreaID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.CreatedByPerson).WithMany(b => b.OnlandVisualTrashAssessmentsWhereYouAreTheCreatedByPerson).HasForeignKey(c => c.CreatedByPersonID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID
            HasOptional(a => a.OnlandVisualTrashAssessmentArea).WithMany(b => b.OnlandVisualTrashAssessments).HasForeignKey(c => c.OnlandVisualTrashAssessmentAreaID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID
        }
    }
}