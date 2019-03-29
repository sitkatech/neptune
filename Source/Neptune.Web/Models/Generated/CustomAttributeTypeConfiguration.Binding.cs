//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttributeType]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class CustomAttributeTypeConfiguration : EntityTypeConfiguration<CustomAttributeType>
    {
        public CustomAttributeTypeConfiguration() : this("dbo"){}

        public CustomAttributeTypeConfiguration(string schema)
        {
            ToTable("CustomAttributeType", schema);
            HasKey(x => x.CustomAttributeTypeID);
            Property(x => x.CustomAttributeTypeID).HasColumnName(@"CustomAttributeTypeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.CustomAttributeTypeName).HasColumnName(@"CustomAttributeTypeName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.CustomAttributeDataTypeID).HasColumnName(@"CustomAttributeDataTypeID").HasColumnType("int").IsRequired();
            Property(x => x.MeasurementUnitTypeID).HasColumnName(@"MeasurementUnitTypeID").HasColumnType("int").IsOptional();
            Property(x => x.IsRequired).HasColumnName(@"IsRequired").HasColumnType("bit").IsRequired();
            Property(x => x.CustomAttributeTypeDescription).HasColumnName(@"CustomAttributeTypeDescription").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(200);
            Property(x => x.CustomAttributeTypePurposeID).HasColumnName(@"CustomAttributeTypePurposeID").HasColumnType("int").IsRequired();
            Property(x => x.CustomAttributeTypeOptionsSchema).HasColumnName(@"CustomAttributeTypeOptionsSchema").HasColumnType("varchar").IsOptional();

            // Foreign keys

        }
    }
}