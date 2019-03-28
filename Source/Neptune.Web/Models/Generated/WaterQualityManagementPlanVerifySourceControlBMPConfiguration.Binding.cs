//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifySourceControlBMPConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanVerifySourceControlBMP>
    {
        public WaterQualityManagementPlanVerifySourceControlBMPConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanVerifySourceControlBMPConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanVerifySourceControlBMP", schema);
            HasKey(x => x.WaterQualityManagementPlanVerifySourceControlBMPID);
            Property(x => x.WaterQualityManagementPlanVerifySourceControlBMPID).HasColumnName(@"WaterQualityManagementPlanVerifySourceControlBMPID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.WaterQualityManagementPlanVerifyID).HasColumnName(@"WaterQualityManagementPlanVerifyID").HasColumnType("int").IsRequired();
            Property(x => x.SourceControlBMPID).HasColumnName(@"SourceControlBMPID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanSourceControlCondition).HasColumnName(@"WaterQualityManagementPlanSourceControlCondition").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(1000);

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlanVerify).WithMany(b => b.WaterQualityManagementPlanVerifySourceControlBMPs).HasForeignKey(c => c.WaterQualityManagementPlanVerifyID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerifySourceControlBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID
            HasRequired(a => a.SourceControlBMP).WithMany(b => b.WaterQualityManagementPlanVerifySourceControlBMPs).HasForeignKey(c => c.SourceControlBMPID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerifySourceControlBMP_SourceControlBMP_SourceControlBMPID
        }
    }
}