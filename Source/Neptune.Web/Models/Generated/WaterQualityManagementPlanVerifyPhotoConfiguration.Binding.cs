//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyPhoto]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanVerifyPhotoConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanVerifyPhoto>
    {
        public WaterQualityManagementPlanVerifyPhotoConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanVerifyPhotoConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanVerifyPhoto", schema);
            HasKey(x => x.WaterQualityManagementPlanVerifyPhotoID);
            Property(x => x.WaterQualityManagementPlanVerifyPhotoID).HasColumnName(@"WaterQualityManagementPlanVerifyPhotoID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.WaterQualityManagementPlanVerifyID).HasColumnName(@"WaterQualityManagementPlanVerifyID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanPhotoID).HasColumnName(@"WaterQualityManagementPlanPhotoID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlanVerify).WithMany(b => b.WaterQualityManagementPlanVerifyPhotos).HasForeignKey(c => c.WaterQualityManagementPlanVerifyID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID
            HasRequired(a => a.WaterQualityManagementPlanPhoto).WithMany(b => b.WaterQualityManagementPlanVerifyPhotos).HasForeignKey(c => c.WaterQualityManagementPlanPhotoID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanPhoto_WaterQualityManagementPlanPhotoID
        }
    }
}