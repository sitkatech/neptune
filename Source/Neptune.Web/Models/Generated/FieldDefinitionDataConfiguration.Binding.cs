//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinitionData]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class FieldDefinitionDataConfiguration : EntityTypeConfiguration<FieldDefinitionData>
    {
        public FieldDefinitionDataConfiguration() : this("dbo"){}

        public FieldDefinitionDataConfiguration(string schema)
        {
            ToTable("FieldDefinitionData", schema);
            HasKey(x => x.FieldDefinitionDataID);
            Property(x => x.FieldDefinitionDataID).HasColumnName(@"FieldDefinitionDataID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FieldDefinitionID).HasColumnName(@"FieldDefinitionID").HasColumnType("int").IsRequired();
            Property(x => x.FieldDefinitionDataValue).HasColumnName(@"FieldDefinitionDataValue").HasColumnType("varchar").IsOptional();
            Property(x => x.FieldDefinitionLabel).HasColumnName(@"FieldDefinitionLabel").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(300);

            // Foreign keys

        }
    }
}