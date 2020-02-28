//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[StormwaterJurisdictionGeometry]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class StormwaterJurisdictionGeometryConfiguration : EntityTypeConfiguration<StormwaterJurisdictionGeometry>
    {
        public StormwaterJurisdictionGeometryConfiguration() : this("dbo"){}

        public StormwaterJurisdictionGeometryConfiguration(string schema)
        {
            ToTable("StormwaterJurisdictionGeometry", schema);
            HasKey(x => x.StormwaterJurisdictionGeometryID);
            Property(x => x.StormwaterJurisdictionGeometryID).HasColumnName(@"StormwaterJurisdictionGeometryID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.StormwaterJurisdictionID).HasColumnName(@"StormwaterJurisdictionID").HasColumnType("int").IsRequired();
            Property(x => x.GeometryNative).HasColumnName(@"GeometryNative").HasColumnType("geometry").IsRequired();
            Property(x => x.Geometry4326).HasColumnName(@"Geometry4326").HasColumnType("geometry").IsRequired();

            // Foreign keys
            HasRequired(a => a.StormwaterJurisdiction).WithMany(b => b.StormwaterJurisdictionGeometries).HasForeignKey(c => c.StormwaterJurisdictionID).WillCascadeOnDelete(false); // FK_StormwaterJurisdictionGeometry_StormwaterJurisdiction_StormwaterJurisdictionID
        }
    }
}