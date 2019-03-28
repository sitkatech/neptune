//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[NeptuneHomePageImage]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class NeptuneHomePageImageConfiguration : EntityTypeConfiguration<NeptuneHomePageImage>
    {
        public NeptuneHomePageImageConfiguration() : this("dbo"){}

        public NeptuneHomePageImageConfiguration(string schema)
        {
            ToTable("NeptuneHomePageImage", schema);
            HasKey(x => x.NeptuneHomePageImageID);
            Property(x => x.NeptuneHomePageImageID).HasColumnName(@"NeptuneHomePageImageID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FileResourceID).HasColumnName(@"FileResourceID").HasColumnType("int").IsRequired();
            Property(x => x.Caption).HasColumnName(@"Caption").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(300);
            Property(x => x.SortOrder).HasColumnName(@"SortOrder").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.FileResource).WithMany(b => b.NeptuneHomePageImages).HasForeignKey(c => c.FileResourceID).WillCascadeOnDelete(false); // FK_NeptuneHomePageImage_FileResource_FileResourceID
        }
    }
}