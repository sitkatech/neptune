//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyTreatmentBMP]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyTreatmentBMPConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanVerifyTreatmentBMP>
    {
        public WaterQualityManagementPlanVerifyTreatmentBMPConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanVerifyTreatmentBMPConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanVerifyTreatmentBMP", schema);
            HasKey(x => x.WaterQualityManagementPlanVerifyTreatmentBMPID);
            Property(x => x.WaterQualityManagementPlanVerifyTreatmentBMPID).HasColumnName(@"WaterQualityManagementPlanVerifyTreatmentBMPID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanVerifyID).HasColumnName(@"WaterQualityManagementPlanVerifyID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.IsAdequate).HasColumnName(@"IsAdequate").HasColumnType("bit").IsOptional();
            Property(x => x.WaterQualityManagementPlanVerifyQuickBMPNote).HasColumnName(@"WaterQualityManagementPlanVerifyQuickBMPNote").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlanVerify).WithMany(b => b.WaterQualityManagementPlanVerifyTreatmentBMPs).HasForeignKey(c => c.WaterQualityManagementPlanVerifyID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerifyTreatmentBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.WaterQualityManagementPlanVerifyTreatmentBMPs).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerifyTreatmentBMP_TreatmentBMP_TreatmentBMPID
        }
    }
}