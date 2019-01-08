//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanPhoto]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanPhotoConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanPhoto>
    {
        public WaterQualityManagementPlanPhotoConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanPhotoConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanPhoto", schema);
            HasKey(x => x.WaterQualityManagementPlanPhotoID);
            Property(x => x.WaterQualityManagementPlanPhotoID).HasColumnName(@"WaterQualityManagementPlanPhotoID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FileResourceID).HasColumnName(@"FileResourceID").HasColumnType("int").IsRequired();
            Property(x => x.Caption).HasColumnName(@"Caption").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.UploadDate).HasColumnName(@"UploadDate").HasColumnType("datetime").IsRequired();

            // Foreign keys
            HasRequired(a => a.FileResource).WithMany(b => b.WaterQualityManagementPlanPhotos).HasForeignKey(c => c.FileResourceID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanPhoto_FileResource_FileResourceID
        }
    }
}