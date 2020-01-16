//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PrecipitationZoneStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class PrecipitationZoneStagingConfiguration : EntityTypeConfiguration<PrecipitationZoneStaging>
    {
        public PrecipitationZoneStagingConfiguration() : this("dbo"){}

        public PrecipitationZoneStagingConfiguration(string schema)
        {
            ToTable("PrecipitationZoneStaging", schema);
            HasKey(x => x.PrecipitationZoneStagingID);
            Property(x => x.PrecipitationZoneStagingID).HasColumnName(@"PrecipitationZoneStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PrecipitationZoneKey).HasColumnName(@"PrecipitationZoneKey").HasColumnType("int").IsRequired();
            Property(x => x.DesignStormwaterDepthInInches).HasColumnName(@"DesignStormwaterDepthInInches").HasColumnType("float").IsRequired();
            Property(x => x.PrecipitationZoneGeometry).HasColumnName(@"PrecipitationZoneGeometry").HasColumnType("geometry").IsRequired();

        }
    }
}