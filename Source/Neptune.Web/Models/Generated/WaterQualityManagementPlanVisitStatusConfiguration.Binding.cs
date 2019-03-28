//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVisitStatus]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVisitStatusConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanVisitStatus>
    {
        public WaterQualityManagementPlanVisitStatusConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanVisitStatusConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanVisitStatus", schema);
            HasKey(x => x.WaterQualityManagementPlanVisitStatusID);
            Property(x => x.WaterQualityManagementPlanVisitStatusID).HasColumnName(@"WaterQualityManagementPlanVisitStatusID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.WaterQualityManagementPlanVisitStatusName).HasColumnName(@"WaterQualityManagementPlanVisitStatusName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);

            // Foreign keys

        }
    }
}