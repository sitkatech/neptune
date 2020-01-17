//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[DroolToolWatershed]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class DroolToolWatershedConfiguration : EntityTypeConfiguration<DroolToolWatershed>
    {
        public DroolToolWatershedConfiguration() : this("dbo"){}

        public DroolToolWatershedConfiguration(string schema)
        {
            ToTable("DroolToolWatershed", schema);
            HasKey(x => x.DroolToolWatershedID);
            Property(x => x.DroolToolWatershedID).HasColumnName(@"DroolToolWatershedID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DroolToolWatershedGeometry).HasColumnName(@"DroolToolWatershedGeometry").HasColumnType("geometry").IsOptional();
            Property(x => x.DroolToolWatershedName).HasColumnName(@"DroolToolWatershedName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(50);
            Property(x => x.DroolToolWatershedGeometry4326).HasColumnName(@"DroolToolWatershedGeometry4326").HasColumnType("geometry").IsOptional();

        }
    }
}