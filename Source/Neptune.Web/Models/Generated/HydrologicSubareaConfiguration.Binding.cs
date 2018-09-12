//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HydrologicSubarea]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class HydrologicSubareaConfiguration : EntityTypeConfiguration<HydrologicSubarea>
    {
        public HydrologicSubareaConfiguration() : this("dbo"){}

        public HydrologicSubareaConfiguration(string schema)
        {
            ToTable("HydrologicSubarea", schema);
            HasKey(x => x.HydrologicSubareaID);
            Property(x => x.HydrologicSubareaID).HasColumnName(@"HydrologicSubareaID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.HydrologicSubareaName).HasColumnName(@"HydrologicSubareaName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);

            // Foreign keys

        }
    }
}