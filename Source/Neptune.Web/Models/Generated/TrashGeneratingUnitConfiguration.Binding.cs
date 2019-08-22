//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TrashGeneratingUnit]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TrashGeneratingUnitConfiguration : EntityTypeConfiguration<TrashGeneratingUnit>
    {
        public TrashGeneratingUnitConfiguration() : this("dbo"){}

        public TrashGeneratingUnitConfiguration(string schema)
        {
            ToTable("TrashGeneratingUnit", schema);
            HasKey(x => x.TrashGeneratingUnitID);
            Property(x => x.TrashGeneratingUnitID).HasColumnName(@"TrashGeneratingUnitID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.OnlandVisualTrashAssessmentAreaID).HasColumnName(@"OnlandVisualTrashAssessmentAreaID").HasColumnType("int").IsOptional();
            Property(x => x.LandUseBlockID).HasColumnName(@"LandUseBlockID").HasColumnType("int").IsOptional();
            Property(x => x.TrashGeneratingUnitGeometry).HasColumnName(@"TrashGeneratingUnitGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.LastUpdateDate).HasColumnName(@"LastUpdateDate").HasColumnType("datetime").IsOptional();
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.TrashGeneratingUnits).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_TrashGeneratingUnit_StormwaterJurisdiction_StormwaterJurisdictionID
            HasOptional(a => a.LandUseBlock).WithMany(b => b.TrashGeneratingUnits).HasForeignKey(c => c.LandUseBlockID).WillCascadeOnDelete(false); // FK_TrashGeneratingUnit_LandUseBlock_LandUseBlockID
        }
    }
}