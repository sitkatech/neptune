//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanBoundary]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanBoundaryConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanBoundary>
    {
        public WaterQualityManagementPlanBoundaryConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanBoundaryConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanBoundary", schema);
            HasKey(x => x.WaterQualityManagementPlanGeometryID);
            Property(x => x.WaterQualityManagementPlanGeometryID).HasColumnName(@"WaterQualityManagementPlanGeometryID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsRequired();
            Property(x => x.GeometryNative).HasColumnName(@"GeometryNative").HasColumnType("geometry").IsOptional();
            Property(x => x.Geometry4326).HasColumnName(@"Geometry4326").HasColumnType("geometry").IsOptional();

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlan).WithMany(b => b.WaterQualityManagementPlanBoundaries).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanBoundary_WaterQualityManagementPlan_WaterQualityManagementPlanID
        }
    }
}