//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentArea]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentAreaConfiguration : EntityTypeConfiguration<OnlandVisualTrashAssessmentArea>
    {
        public OnlandVisualTrashAssessmentAreaConfiguration() : this("dbo"){}

        public OnlandVisualTrashAssessmentAreaConfiguration(string schema)
        {
            ToTable("OnlandVisualTrashAssessmentArea", schema);
            HasKey(x => x.OnlandVisualTrashAssessmentAreaID);
            Property(x => x.OnlandVisualTrashAssessmentAreaID).HasColumnName(@"OnlandVisualTrashAssessmentAreaID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OnlandVisualTrashAssessmentAreaName).HasColumnName(@"OnlandVisualTrashAssessmentAreaName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.OnlandVisualTrashAssessmentAreaGeometry).HasColumnName(@"OnlandVisualTrashAssessmentAreaGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.OnlandVisualTrashAssessmentBaselineScoreID).HasColumnName(@"OnlandVisualTrashAssessmentBaselineScoreID").HasColumnType("int").IsOptional();
            Property(x => x.AssessmentAreaDescription).HasColumnName(@"AssessmentAreaDescription").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.TransectLine).HasColumnName(@"TransectLine").HasColumnType("geometry").IsOptional();
            Property(x => x.OnlandVisualTrashAssessmentProgressScoreID).HasColumnName(@"OnlandVisualTrashAssessmentProgressScoreID").HasColumnType("int").IsOptional();
            Property(x => x.OnlandVisualTrashAssessmentAreaGeometry4326).HasColumnName(@"OnlandVisualTrashAssessmentAreaGeometry4326").HasColumnType("geometry").IsOptional();
            Property(x => x.TransectLine4326).HasColumnName(@"TransectLine4326").HasColumnType("geometry").IsOptional();

            // Foreign keys
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.OnlandVisualTrashAssessmentAreas).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessmentArea_StormwaterJurisdiction_StormwaterJurisdictionID
        }
    }
}