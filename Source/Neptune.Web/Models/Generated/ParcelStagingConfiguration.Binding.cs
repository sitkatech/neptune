//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ParcelStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ParcelStagingConfiguration : EntityTypeConfiguration<ParcelStaging>
    {
        public ParcelStagingConfiguration() : this("dbo"){}

        public ParcelStagingConfiguration(string schema)
        {
            ToTable("ParcelStaging", schema);
            HasKey(x => x.ParcelStagingID);
            Property(x => x.ParcelStagingID).HasColumnName(@"ParcelStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ParcelNumber).HasColumnName(@"ParcelNumber").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(22);
            Property(x => x.ParcelStagingGeometry).HasColumnName(@"ParcelStagingGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.ParcelStagingAreaSquareFeet).HasColumnName(@"ParcelStagingAreaSquareFeet").HasColumnType("float").IsOptional();
            Property(x => x.OwnerName).HasColumnName(@"OwnerName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.ParcelStreetNumber).HasColumnName(@"ParcelStreetNumber").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(10);
            Property(x => x.ParcelAddress).HasColumnName(@"ParcelAddress").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(150);
            Property(x => x.ParcelZipCode).HasColumnName(@"ParcelZipCode").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(5);
            Property(x => x.LandUse).HasColumnName(@"LandUse").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(4);
            Property(x => x.SquareFeetHome).HasColumnName(@"SquareFeetHome").HasColumnType("int").IsOptional();
            Property(x => x.SquareFeetLot).HasColumnName(@"SquareFeetLot").HasColumnType("int").IsOptional();
            Property(x => x.UploadedByPersonID).HasColumnName(@"UploadedByPersonID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.UploadedByPerson).WithMany(b => b.ParcelStagingsWhereYouAreTheUploadedByPerson).HasForeignKey(c => c.UploadedByPersonID).WillCascadeOnDelete(false); // FK_ParcelStaging_Person_UploadedByPersonID_PersonID
        }
    }
}