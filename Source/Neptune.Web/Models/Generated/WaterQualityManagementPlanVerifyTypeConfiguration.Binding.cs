//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyType]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyTypeConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanVerifyType>
    {
        public WaterQualityManagementPlanVerifyTypeConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanVerifyTypeConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanVerifyType", schema);
            HasKey(x => x.WaterQualityManagementPlanVerifyTypeID);
            Property(x => x.WaterQualityManagementPlanVerifyTypeID).HasColumnName(@"WaterQualityManagementPlanVerifyTypeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.WaterQualityManagementPlanVerifyTypeName).HasColumnName(@"WaterQualityManagementPlanVerifyTypeName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);

            // Foreign keys

        }
    }
}