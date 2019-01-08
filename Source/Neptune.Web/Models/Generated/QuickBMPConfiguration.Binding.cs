//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[QuickBMP]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class QuickBMPConfiguration : EntityTypeConfiguration<QuickBMP>
    {
        public QuickBMPConfiguration() : this("dbo"){}

        public QuickBMPConfiguration(string schema)
        {
            ToTable("QuickBMP", schema);
            HasKey(x => x.QuickBMPID);
            Property(x => x.QuickBMPID).HasColumnName(@"QuickBMPID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.QuickBMPName).HasColumnName(@"QuickBMPName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.QuickBMPNote).HasColumnName(@"QuickBMPNote").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(200);

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlan).WithMany(b => b.QuickBMPs).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_QuickBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID
            HasRequired(a => a.TreatmentBMPType).WithMany(b => b.QuickBMPs).HasForeignKey(c => c.TreatmentBMPTypeID).WillCascadeOnDelete(false); // FK_QuickBMP_TreatmentBMPType_TreatmentBMPTypeID
        }
    }
}