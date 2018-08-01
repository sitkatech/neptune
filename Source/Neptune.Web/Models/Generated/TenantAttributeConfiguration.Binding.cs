//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TenantAttribute]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TenantAttributeConfiguration : EntityTypeConfiguration<TenantAttribute>
    {
        public TenantAttributeConfiguration() : this("dbo"){}

        public TenantAttributeConfiguration(string schema)
        {
            ToTable("TenantAttribute", schema);
            HasKey(x => x.TenantAttributeID);
            Property(x => x.TenantAttributeID).HasColumnName(@"TenantAttributeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.DefaultBoundingBox).HasColumnName(@"DefaultBoundingBox").HasColumnType("geometry").IsRequired();
            Property(x => x.MinimumYear).HasColumnName(@"MinimumYear").HasColumnType("int").IsRequired();
            Property(x => x.PrimaryContactPersonID).HasColumnName(@"PrimaryContactPersonID").HasColumnType("int").IsOptional();
            Property(x => x.TenantSquareLogoFileResourceID).HasColumnName(@"TenantSquareLogoFileResourceID").HasColumnType("int").IsOptional();
            Property(x => x.TenantBannerLogoFileResourceID).HasColumnName(@"TenantBannerLogoFileResourceID").HasColumnType("int").IsOptional();
            Property(x => x.TenantStyleSheetFileResourceID).HasColumnName(@"TenantStyleSheetFileResourceID").HasColumnType("int").IsOptional();
            Property(x => x.TenantDisplayName).HasColumnName(@"TenantDisplayName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.ToolDisplayName).HasColumnName(@"ToolDisplayName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.RecaptchaPublicKey).HasColumnName(@"RecaptchaPublicKey").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.RecaptchaPrivateKey).HasColumnName(@"RecaptchaPrivateKey").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(100);
            Property(x => x.MapServiceUrl).HasColumnName(@"MapServiceUrl").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);
            Property(x => x.ParcelLayerName).HasColumnName(@"ParcelLayerName").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(255);

            // Foreign keys
            HasOptional(a => a.PrimaryContactPerson).WithMany(b => b.TenantAttributesWhereYouAreThePrimaryContactPerson).HasForeignKey(c => c.PrimaryContactPersonID).WillCascadeOnDelete(false); // FK_TenantAttribute_Person_PrimaryContactPersonID_PersonID
            HasOptional(a => a.TenantSquareLogoFileResource).WithMany(b => b.TenantAttributesWhereYouAreTheTenantSquareLogoFileResource).HasForeignKey(c => c.TenantSquareLogoFileResourceID).WillCascadeOnDelete(false); // FK_TenantAttribute_FileResource_TenantSquareLogoFileResourceID_FileResourceID
            HasOptional(a => a.TenantBannerLogoFileResource).WithMany(b => b.TenantAttributesWhereYouAreTheTenantBannerLogoFileResource).HasForeignKey(c => c.TenantBannerLogoFileResourceID).WillCascadeOnDelete(false); // FK_TenantAttribute_FileResource_TenantBannerLogoFileResourceID_FileResourceID
            HasOptional(a => a.TenantStyleSheetFileResource).WithMany(b => b.TenantAttributesWhereYouAreTheTenantStyleSheetFileResource).HasForeignKey(c => c.TenantStyleSheetFileResourceID).WillCascadeOnDelete(false); // FK_TenantAttribute_FileResource_TenantStyleSheetFileResourceID_FileResourceID
        }
    }
}