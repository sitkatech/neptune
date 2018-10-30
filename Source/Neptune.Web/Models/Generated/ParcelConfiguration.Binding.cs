//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[Parcel]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ParcelConfiguration : EntityTypeConfiguration<Parcel>
    {
        public ParcelConfiguration() : this("dbo"){}

        public ParcelConfiguration(string schema)
        {
            ToTable("Parcel", schema);
            HasKey(x => x.ParcelID);
            Property(x => x.ParcelID).HasColumnName(@"ParcelID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.ParcelNumber).HasColumnName(@"ParcelNumber").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(22);
            Property(x => x.ParcelGeometry).HasColumnName(@"ParcelGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.OwnerName).HasColumnName(@"OwnerName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.ParcelStreetNumber).HasColumnName(@"ParcelStreetNumber").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(10);
            Property(x => x.ParcelAddress).HasColumnName(@"ParcelAddress").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(150);
            Property(x => x.ParcelZipCode).HasColumnName(@"ParcelZipCode").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(5);
            Property(x => x.LandUse).HasColumnName(@"LandUse").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(4);
            Property(x => x.SquareFeetHome).HasColumnName(@"SquareFeetHome").HasColumnType("int").IsOptional();
            Property(x => x.SquareFeetLot).HasColumnName(@"SquareFeetLot").HasColumnType("int").IsOptional();
            Property(x => x.ParcelAreaInAcres).HasColumnName(@"ParcelAreaInAcres").HasColumnType("float").IsRequired();

            // Foreign keys

        }
    }
}