//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZone]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class PrecipitationZoneConfiguration : EntityTypeConfiguration<PrecipitationZone>
    {
        public PrecipitationZoneConfiguration() : this("dbo"){}

        public PrecipitationZoneConfiguration(string schema)
        {
            ToTable("PrecipitationZone", schema);
            HasKey(x => x.PrecipitationZoneID);
            Property(x => x.PrecipitationZoneID).HasColumnName(@"PrecipitationZoneID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PrecipitationZoneKey).HasColumnName(@"PrecipitationZoneKey").HasColumnType("int").IsRequired();
            Property(x => x.DesignStormwaterDepthInInches).HasColumnName(@"DesignStormwaterDepthInInches").HasColumnType("float").IsRequired();
            Property(x => x.PrecipitationZoneGeometry).HasColumnName(@"PrecipitationZoneGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.LastUpdate).HasColumnName(@"LastUpdate").HasColumnType("datetime").IsRequired();

            // Foreign keys

        }
    }
}