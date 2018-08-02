//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPDocument]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPDocumentConfiguration : EntityTypeConfiguration<TreatmentBMPDocument>
    {
        public TreatmentBMPDocumentConfiguration() : this("dbo"){}

        public TreatmentBMPDocumentConfiguration(string schema)
        {
            ToTable("TreatmentBMPDocument", schema);
            HasKey(x => x.TreatmentBMPDocumentID);
            Property(x => x.TreatmentBMPDocumentID).HasColumnName(@"TreatmentBMPDocumentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.FileResourceID).HasColumnName(@"FileResourceID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.DisplayName).HasColumnName(@"DisplayName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(200);
            Property(x => x.UploadDate).HasColumnName(@"UploadDate").HasColumnType("date").IsRequired();
            Property(x => x.DocumentDescription).HasColumnName(@"DocumentDescription").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.FileResource).WithMany(b => b.TreatmentBMPDocuments).HasForeignKey(c => c.FileResourceID).WillCascadeOnDelete(false); // FK_TreatmentBMPDocument_FileResource_FileResourceID
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.TreatmentBMPDocuments).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_TreatmentBMPDocument_TreatmentBMP_TreatmentBMPID
        }
    }
}