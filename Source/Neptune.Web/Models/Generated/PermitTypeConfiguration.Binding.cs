//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PermitType]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class PermitTypeConfiguration : EntityTypeConfiguration<PermitType>
    {
        public PermitTypeConfiguration() : this("dbo"){}

        public PermitTypeConfiguration(string schema)
        {
            ToTable("PermitType", schema);
            HasKey(x => x.PermitTypeID);
            Property(x => x.PermitTypeID).HasColumnName(@"PermitTypeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.PermitTypeName).HasColumnName(@"PermitTypeName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(80);
            Property(x => x.PermitTypeDisplayName).HasColumnName(@"PermitTypeDisplayName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(80);

            // Foreign keys

        }
    }
}