//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[LoadGeneratingUnit]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class LoadGeneratingUnitConfiguration : EntityTypeConfiguration<LoadGeneratingUnit>
    {
        public LoadGeneratingUnitConfiguration() : this("dbo"){}

        public LoadGeneratingUnitConfiguration(string schema)
        {
            ToTable("LoadGeneratingUnit", schema);
            HasKey(x => x.LoadGeneratingUnitID);
            Property(x => x.LoadGeneratingUnitID).HasColumnName(@"LoadGeneratingUnitID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.LoadGeneratingUnitGeometry).HasColumnName(@"LoadGeneratingUnitGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.LSPCBasinID).HasColumnName(@"LSPCBasinID").HasColumnType("int").IsOptional();
            Property(x => x.RegionalSubbasinID).HasColumnName(@"RegionalSubbasinID").HasColumnType("int").IsOptional();
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasOptional(a => a.LSPCBasin).WithMany(b => b.LoadGeneratingUnits).HasForeignKey(c => c.LSPCBasinID).WillCascadeOnDelete(false); // FK_LoadGeneratingUnit_LSPCBasin_LSPCBasinID
            HasOptional(a => a.RegionalSubbasin).WithMany(b => b.LoadGeneratingUnits).HasForeignKey(c => c.RegionalSubbasinID).WillCascadeOnDelete(false); // FK_LoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID
            HasOptional(a => a.Delineation).WithMany(b => b.LoadGeneratingUnits).HasForeignKey(c => c.DelineationID).WillCascadeOnDelete(false); // FK_LoadGeneratingUnit_Delineation_DelineationID
            HasOptional(a => a.WaterQualityManagementPlan).WithMany(b => b.LoadGeneratingUnits).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_LoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID
        }
    }
}