//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[SourceControlBMPAttribute]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class SourceControlBMPAttributeConfiguration : EntityTypeConfiguration<SourceControlBMPAttribute>
    {
        public SourceControlBMPAttributeConfiguration() : this("dbo"){}

        public SourceControlBMPAttributeConfiguration(string schema)
        {
            ToTable("SourceControlBMPAttribute", schema);
            HasKey(x => x.SourceControlBMPAttributeID);
            Property(x => x.SourceControlBMPAttributeID).HasColumnName(@"SourceControlBMPAttributeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.SourceControlBMPAttributeCategoryID).HasColumnName(@"SourceControlBMPAttributeCategoryID").HasColumnType("int").IsRequired();
            Property(x => x.SourceControlBMPAttributeName).HasColumnName(@"SourceControlBMPAttributeName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.SourceControlBMPAttributeCategory).WithMany(b => b.SourceControlBMPAttributes).HasForeignKey(c => c.SourceControlBMPAttributeCategoryID).WillCascadeOnDelete(false); // FK_SourceControlBMPAttribute_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryID
        }
    }
}