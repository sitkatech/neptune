//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptunePageImage]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class NeptunePageImageConfiguration : EntityTypeConfiguration<NeptunePageImage>
    {
        public NeptunePageImageConfiguration() : this("dbo"){}

        public NeptunePageImageConfiguration(string schema)
        {
            ToTable("NeptunePageImage", schema);
            HasKey(x => x.NeptunePageImageID);
            Property(x => x.NeptunePageImageID).HasColumnName(@"NeptunePageImageID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.NeptunePageID).HasColumnName(@"NeptunePageID").HasColumnType("int").IsRequired();
            Property(x => x.FileResourceID).HasColumnName(@"FileResourceID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.NeptunePage).WithMany(b => b.NeptunePageImages).HasForeignKey(c => c.NeptunePageID).WillCascadeOnDelete(false); // FK_NeptunePageImage_NeptunePage_NeptunePageID
            HasRequired(a => a.FileResource).WithMany(b => b.NeptunePageImages).HasForeignKey(c => c.FileResourceID).WillCascadeOnDelete(false); // FK_NeptunePageImage_FileResource_FileResourceID
        }
    }
}