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
            Property(x => x.OnlandVisualTrashAssessmentAreaGeometry).HasColumnName(@"OnlandVisualTrashAssessmentAreaGeometry").HasColumnType("geometry").IsRequired();

            // Foreign keys

        }
    }
}