//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Watershed]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WatershedConfiguration : EntityTypeConfiguration<Watershed>
    {
        public WatershedConfiguration() : this("dbo"){}

        public WatershedConfiguration(string schema)
        {
            ToTable("Watershed", schema);
            HasKey(x => x.WatershedID);
            Property(x => x.WatershedID).HasColumnName(@"WatershedID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.WatershedGeometry).HasColumnName(@"WatershedGeometry").HasColumnType("geometry").IsOptional();
            Property(x => x.WatershedName).HasColumnName(@"WatershedName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(50);

        }
    }
}