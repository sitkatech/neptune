//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPImage]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPImageConfiguration : EntityTypeConfiguration<TreatmentBMPImage>
    {
        public TreatmentBMPImageConfiguration() : this("dbo"){}

        public TreatmentBMPImageConfiguration(string schema)
        {
            ToTable("TreatmentBMPImage", schema);
            HasKey(x => x.TreatmentBMPImageID);
            Property(x => x.TreatmentBMPImageID).HasColumnName(@"TreatmentBMPImageID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FileResourceID).HasColumnName(@"FileResourceID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.Caption).HasColumnName(@"Caption").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.UploadDate).HasColumnName(@"UploadDate").HasColumnType("date").IsRequired();

            // Foreign keys
            HasRequired(a => a.FileResource).WithMany(b => b.TreatmentBMPImages).HasForeignKey(c => c.FileResourceID).WillCascadeOnDelete(false); // FK_TreatmentBMPImage_FileResource_FileResourceID
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.TreatmentBMPImages).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_TreatmentBMPImage_TreatmentBMP_TreatmentBMPID
        }
    }
}