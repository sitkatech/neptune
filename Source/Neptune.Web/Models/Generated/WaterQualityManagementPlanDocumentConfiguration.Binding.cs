//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanDocument]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanDocumentConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanDocument>
    {
        public WaterQualityManagementPlanDocumentConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanDocumentConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanDocument", schema);
            HasKey(x => x.WaterQualityManagementPlanDocumentID);
            Property(x => x.WaterQualityManagementPlanDocumentID).HasColumnName(@"WaterQualityManagementPlanDocumentID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsRequired();
            Property(x => x.FileResourceID).HasColumnName(@"FileResourceID").HasColumnType("int").IsRequired();
            Property(x => x.DisplayName).HasColumnName(@"DisplayName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.Description).HasColumnName(@"Description").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(1000);
            Property(x => x.UploadDate).HasColumnName(@"UploadDate").HasColumnType("datetime").IsRequired();
            Property(x => x.WaterQualityManagementPlanDocumentTypeID).HasColumnName(@"WaterQualityManagementPlanDocumentTypeID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlan).WithMany(b => b.WaterQualityManagementPlanDocuments).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanDocument_WaterQualityManagementPlan_WaterQualityManagementPlanID
            HasRequired(a => a.FileResource).WithMany(b => b.WaterQualityManagementPlanDocuments).HasForeignKey(c => c.FileResourceID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanDocument_FileResource_FileResourceID
        }
    }
}