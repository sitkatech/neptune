//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttributeCategory]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class SourceControlBMPAttributeCategoryConfiguration : EntityTypeConfiguration<SourceControlBMPAttributeCategory>
    {
        public SourceControlBMPAttributeCategoryConfiguration() : this("dbo"){}

        public SourceControlBMPAttributeCategoryConfiguration(string schema)
        {
            ToTable("SourceControlBMPAttributeCategory", schema);
            HasKey(x => x.SourceControlBMPAttributeCategoryID);
            Property(x => x.SourceControlBMPAttributeCategoryID).HasColumnName(@"SourceControlBMPAttributeCategoryID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.SourceControlBMPAttributeCategoryShortName).HasColumnName(@"SourceControlBMPAttributeCategoryShortName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(50);
            Property(x => x.SourceControlBMPAttributeCategoryName).HasColumnName(@"SourceControlBMPAttributeCategoryName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);

            // Foreign keys

        }
    }
}