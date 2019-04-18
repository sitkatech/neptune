//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Delineation]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class DelineationConfiguration : EntityTypeConfiguration<Delineation>
    {
        public DelineationConfiguration() : this("dbo"){}

        public DelineationConfiguration(string schema)
        {
            ToTable("Delineation", schema);
            HasKey(x => x.DelineationID);
            Property(x => x.DelineationID).HasColumnName(@"DelineationID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DelineationGeometry).HasColumnName(@"DelineationGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.DelineationTypeID).HasColumnName(@"DelineationTypeID").HasColumnType("int").IsRequired();
            Property(x => x.IsVerified).HasColumnName(@"IsVerified").HasColumnType("bit").IsRequired();
            Property(x => x.DateLastVerified).HasColumnName(@"DateLastVerified").HasColumnType("datetime").IsOptional();

            // Foreign keys

        }
    }
}