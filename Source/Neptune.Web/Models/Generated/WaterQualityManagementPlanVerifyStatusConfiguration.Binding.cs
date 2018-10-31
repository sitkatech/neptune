//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyStatus]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyStatusConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanVerifyStatus>
    {
        public WaterQualityManagementPlanVerifyStatusConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanVerifyStatusConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanVerifyStatus", schema);
            HasKey(x => x.WaterQualityManagementPlanVerifyStatusID);
            Property(x => x.WaterQualityManagementPlanVerifyStatusID).HasColumnName(@"WaterQualityManagementPlanVerifyStatusID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanVerifyStatusName).HasColumnName(@"WaterQualityManagementPlanVerifyStatusName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);

            // Foreign keys

        }
    }
}