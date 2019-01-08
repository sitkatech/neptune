//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeValue]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class CustomAttributeValueConfiguration : EntityTypeConfiguration<CustomAttributeValue>
    {
        public CustomAttributeValueConfiguration() : this("dbo"){}

        public CustomAttributeValueConfiguration(string schema)
        {
            ToTable("CustomAttributeValue", schema);
            HasKey(x => x.CustomAttributeValueID);
            Property(x => x.CustomAttributeValueID).HasColumnName(@"CustomAttributeValueID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CustomAttributeID).HasColumnName(@"CustomAttributeID").HasColumnType("int").IsRequired();
            Property(x => x.AttributeValue).HasColumnName(@"AttributeValue").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(1000);

            // Foreign keys
            HasRequired(a => a.CustomAttribute).WithMany(b => b.CustomAttributeValues).HasForeignKey(c => c.CustomAttributeID).WillCascadeOnDelete(false); // FK_CustomAttributeValue_CustomAttribute_CustomAttributeID
        }
    }
}