//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMP]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class SourceControlBMPConfiguration : EntityTypeConfiguration<SourceControlBMP>
    {
        public SourceControlBMPConfiguration() : this("dbo"){}

        public SourceControlBMPConfiguration(string schema)
        {
            ToTable("SourceControlBMP", schema);
            HasKey(x => x.SourceControlBMPID);
            Property(x => x.SourceControlBMPID).HasColumnName(@"SourceControlBMPID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsRequired();
            Property(x => x.SourceControlBMPAttributeID).HasColumnName(@"SourceControlBMPAttributeID").HasColumnType("int").IsRequired();
            Property(x => x.IsPresent).HasColumnName(@"IsPresent").HasColumnType("bit").IsOptional();
            Property(x => x.SourceControlBMPNote).HasColumnName(@"SourceControlBMPNote").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(200);

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlan).WithMany(b => b.SourceControlBMPs).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_SourceControlBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID
            HasRequired(a => a.SourceControlBMPAttribute).WithMany(b => b.SourceControlBMPs).HasForeignKey(c => c.SourceControlBMPAttributeID).WillCascadeOnDelete(false); // FK_SourceControlBMP_SourceControlBMPAttribute_SourceControlBMPAttributeID
        }
    }
}