//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyQuickBMP]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyQuickBMPConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanVerifyQuickBMP>
    {
        public WaterQualityManagementPlanVerifyQuickBMPConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanVerifyQuickBMPConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanVerifyQuickBMP", schema);
            HasKey(x => x.WaterQualityManagementPlanVerifyQuickBMPID);
            Property(x => x.WaterQualityManagementPlanVerifyQuickBMPID).HasColumnName(@"WaterQualityManagementPlanVerifyQuickBMPID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.WaterQualityManagementPlanVerifyID).HasColumnName(@"WaterQualityManagementPlanVerifyID").HasColumnType("int").IsRequired();
            Property(x => x.QuickBMPID).HasColumnName(@"QuickBMPID").HasColumnType("int").IsRequired();
            Property(x => x.IsAdequate).HasColumnName(@"IsAdequate").HasColumnType("bit").IsOptional();
            Property(x => x.WaterQualityManagementPlanVerifyQuickBMPNote).HasColumnName(@"WaterQualityManagementPlanVerifyQuickBMPNote").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlanVerify).WithMany(b => b.WaterQualityManagementPlanVerifyQuickBMPs).HasForeignKey(c => c.WaterQualityManagementPlanVerifyID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerifyQuickBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID
            HasRequired(a => a.QuickBMP).WithMany(b => b.WaterQualityManagementPlanVerifyQuickBMPs).HasForeignKey(c => c.QuickBMPID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerifyQuickBMP_QuickBMP_QuickBMPID
        }
    }
}