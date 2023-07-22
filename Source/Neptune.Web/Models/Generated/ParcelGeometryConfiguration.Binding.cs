//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelGeometry]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ParcelGeometryConfiguration : EntityTypeConfiguration<ParcelGeometry>
    {
        public ParcelGeometryConfiguration() : this("dbo"){}

        public ParcelGeometryConfiguration(string schema)
        {
            ToTable("ParcelGeometry", schema);
            HasKey(x => x.ParcelGeometryID);
            Property(x => x.ParcelGeometryID).HasColumnName(@"ParcelGeometryID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ParcelID).HasColumnName(@"ParcelID").HasColumnType("int").IsRequired();
            Property(x => x.GeometryNative).HasColumnName(@"GeometryNative").HasColumnType("geometry").IsRequired();
            Property(x => x.Geometry4326).HasColumnName(@"Geometry4326").HasColumnType("geometry").IsOptional();

            // Foreign keys
            HasRequired(a => a.Parcel).WithMany(b => b.ParcelGeometries).HasForeignKey(c => c.ParcelID).WillCascadeOnDelete(false); // FK_ParcelGeometry_Parcel_ParcelID
        }
    }
}