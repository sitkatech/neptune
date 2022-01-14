//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldDefinition]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class FieldDefinitionConfiguration : EntityTypeConfiguration<FieldDefinition>
    {
        public FieldDefinitionConfiguration() : this("dbo"){}

        public FieldDefinitionConfiguration(string schema)
        {
            ToTable("FieldDefinition", schema);
            HasKey(x => x.FieldDefinitionID);
            Property(x => x.FieldDefinitionID).HasColumnName(@"FieldDefinitionID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FieldDefinitionTypeID).HasColumnName(@"FieldDefinitionTypeID").HasColumnType("int").IsRequired();
            Property(x => x.FieldDefinitionValue).HasColumnName(@"FieldDefinitionValue").HasColumnType("varchar").IsOptional();

            // Foreign keys

        }
    }
}