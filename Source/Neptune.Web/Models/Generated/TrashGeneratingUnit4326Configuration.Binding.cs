//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit4326]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TrashGeneratingUnit4326Configuration : EntityTypeConfiguration<TrashGeneratingUnit4326>
    {
        public TrashGeneratingUnit4326Configuration() : this("dbo"){}

        public TrashGeneratingUnit4326Configuration(string schema)
        {
            ToTable("TrashGeneratingUnit4326", schema);
            HasKey(x => x.TrashGeneratingUnit4326ID);
            Property(x => x.TrashGeneratingUnit4326ID).HasColumnName(@"TrashGeneratingUnit4326ID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.OnlandVisualTrashAssessmentAreaID).HasColumnName(@"OnlandVisualTrashAssessmentAreaID").HasColumnType("int").IsOptional();
            Property(x => x.LandUseBlockID).HasColumnName(@"LandUseBlockID").HasColumnType("int").IsOptional();
            Property(x => x.TrashGeneratingUnit4326Geometry).HasColumnName(@"TrashGeneratingUnit4326Geometry").HasColumnType("geometry").IsRequired();
            Property(x => x.LastUpdateDate).HasColumnName(@"LastUpdateDate").HasColumnType("datetime").IsOptional();
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.TrashGeneratingUnit4326s).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_TrashGeneratingUnit4326_StormwaterJurisdiction_StormwaterJurisdictionID
            HasOptional(a => a.LandUseBlock).WithMany(b => b.TrashGeneratingUnit4326s).HasForeignKey(c => c.LandUseBlockID).WillCascadeOnDelete(false); // FK_TrashGeneratingUnit4326_LandUseBlock_LandUseBlockID
        }
    }
}