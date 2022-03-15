//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectLoadGeneratingUnit]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class PlannedProjectLoadGeneratingUnitConfiguration : EntityTypeConfiguration<PlannedProjectLoadGeneratingUnit>
    {
        public PlannedProjectLoadGeneratingUnitConfiguration() : this("dbo"){}

        public PlannedProjectLoadGeneratingUnitConfiguration(string schema)
        {
            ToTable("PlannedProjectLoadGeneratingUnit", schema);
            HasKey(x => x.PlannedProjectLoadGeneratingUnitID);
            Property(x => x.PlannedProjectLoadGeneratingUnitID).HasColumnName(@"PlannedProjectLoadGeneratingUnitID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PlannedProjectLoadGeneratingUnitGeometry).HasColumnName(@"PlannedProjectLoadGeneratingUnitGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.ProjectID).HasColumnName(@"ProjectID").HasColumnType("int").IsRequired();
            Property(x => x.ModelBasinID).HasColumnName(@"ModelBasinID").HasColumnType("int").IsOptional();
            Property(x => x.RegionalSubbasinID).HasColumnName(@"RegionalSubbasinID").HasColumnType("int").IsOptional();
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.Project).WithMany(b => b.PlannedProjectLoadGeneratingUnits).HasForeignKey(c => c.ProjectID).WillCascadeOnDelete(false); // FK_PlannedProjectLoadGeneratingUnit_Project_ProjectID
            HasOptional(a => a.ModelBasin).WithMany(b => b.PlannedProjectLoadGeneratingUnits).HasForeignKey(c => c.ModelBasinID).WillCascadeOnDelete(false); // FK_PlannedProjectLoadGeneratingUnit_ModelBasin_ModelBasinID
            HasOptional(a => a.RegionalSubbasin).WithMany(b => b.PlannedProjectLoadGeneratingUnits).HasForeignKey(c => c.RegionalSubbasinID).WillCascadeOnDelete(false); // FK_PlannedProjectLoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID
            HasOptional(a => a.Delineation).WithMany(b => b.PlannedProjectLoadGeneratingUnits).HasForeignKey(c => c.DelineationID).WillCascadeOnDelete(false); // FK_PlannedProjectLoadGeneratingUnit_Delineation_DelineationID
            HasOptional(a => a.WaterQualityManagementPlan).WithMany(b => b.PlannedProjectLoadGeneratingUnits).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_PlannedProjectLoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID
        }
    }
}