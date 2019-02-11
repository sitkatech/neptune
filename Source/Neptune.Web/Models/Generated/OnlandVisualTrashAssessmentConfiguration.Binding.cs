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
            Property(x => x.Notes).HasColumnName(@"Notes").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsOptional();
            Property(x => x.AssessingNewArea).HasColumnName(@"AssessingNewArea").HasColumnType("bit").IsOptional();
            Property(x => x.OnlandVisualTrashAssessmentStatusID).HasColumnName(@"OnlandVisualTrashAssessmentStatusID").HasColumnType("int").IsRequired();
            Property(x => x.DraftGeometry).HasColumnName(@"DraftGeometry").HasColumnType("geometry").IsOptional();
            Property(x => x.IsDraftGeometryManuallyRefined).HasColumnName(@"IsDraftGeometryManuallyRefined").HasColumnType("bit").IsOptional();
            Property(x => x.OnlandVisualTrashAssessmentScoreID).HasColumnName(@"OnlandVisualTrashAssessmentScoreID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.CreatedByPerson).WithMany(b => b.OnlandVisualTrashAssessmentsWhereYouAreTheCreatedByPerson).HasForeignKey(c => c.CreatedByPersonID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID
            HasOptional(a => a.OnlandVisualTrashAssessmentArea).WithMany(b => b.OnlandVisualTrashAssessments).HasForeignKey(c => c.OnlandVisualTrashAssessmentAreaID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID
            HasOptional(a => a.StormwaterJurisdiction).WithMany(b => b.OnlandVisualTrashAssessments).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessment_StormwaterJurisdiction_StormwaterJurisdictionID
        }
    }
}