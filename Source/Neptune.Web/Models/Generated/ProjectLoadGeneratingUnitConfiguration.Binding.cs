//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectLoadGeneratingUnit]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ProjectLoadGeneratingUnitConfiguration : EntityTypeConfiguration<ProjectLoadGeneratingUnit>
    {
        public ProjectLoadGeneratingUnitConfiguration() : this("dbo"){}

        public ProjectLoadGeneratingUnitConfiguration(string schema)
        {
            ToTable("ProjectLoadGeneratingUnit", schema);
            HasKey(x => x.ProjectLoadGeneratingUnitID);
            Property(x => x.ProjectLoadGeneratingUnitID).HasColumnName(@"ProjectLoadGeneratingUnitID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProjectLoadGeneratingUnitGeometry).HasColumnName(@"ProjectLoadGeneratingUnitGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.ProjectID).HasColumnName(@"ProjectID").HasColumnType("int").IsRequired();
            Property(x => x.ModelBasinID).HasColumnName(@"ModelBasinID").HasColumnType("int").IsOptional();
            Property(x => x.RegionalSubbasinID).HasColumnName(@"RegionalSubbasinID").HasColumnType("int").IsOptional();
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();
            Property(x => x.IsEmptyResponseFromHRUService).HasColumnName(@"IsEmptyResponseFromHRUService").HasColumnType("bit").IsOptional();

            // Foreign keys
            HasRequired(a => a.Project).WithMany(b => b.ProjectLoadGeneratingUnits).HasForeignKey(c => c.ProjectID).WillCascadeOnDelete(false); // FK_ProjectLoadGeneratingUnit_Project_ProjectID
            HasOptional(a => a.ModelBasin).WithMany(b => b.ProjectLoadGeneratingUnits).HasForeignKey(c => c.ModelBasinID).WillCascadeOnDelete(false); // FK_ProjectLoadGeneratingUnit_ModelBasin_ModelBasinID
            HasOptional(a => a.RegionalSubbasin).WithMany(b => b.ProjectLoadGeneratingUnits).HasForeignKey(c => c.RegionalSubbasinID).WillCascadeOnDelete(false); // FK_ProjectLoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID
            HasOptional(a => a.Delineation).WithMany(b => b.ProjectLoadGeneratingUnits).HasForeignKey(c => c.DelineationID).WillCascadeOnDelete(false); // FK_ProjectLoadGeneratingUnit_Delineation_DelineationID
            HasOptional(a => a.WaterQualityManagementPlan).WithMany(b => b.ProjectLoadGeneratingUnits).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_ProjectLoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID
        }
    }
}