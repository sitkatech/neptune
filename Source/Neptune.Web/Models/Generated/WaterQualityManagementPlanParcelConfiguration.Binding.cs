//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[WaterQualityManagementPlanParcel]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class WaterQualityManagementPlanParcelConfiguration : EntityTypeConfiguration<WaterQualityManagementPlanParcel>
    {
        public WaterQualityManagementPlanParcelConfiguration() : this("dbo"){}

        public WaterQualityManagementPlanParcelConfiguration(string schema)
        {
            ToTable("WaterQualityManagementPlanParcel", schema);
            HasKey(x => x.WaterQualityManagementPlanParcelID);
            Property(x => x.WaterQualityManagementPlanParcelID).HasColumnName(@"WaterQualityManagementPlanParcelID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsRequired();
            Property(x => x.ParcelID).HasColumnName(@"ParcelID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.WaterQualityManagementPlan).WithMany(b => b.WaterQualityManagementPlanParcels).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanParcel_WaterQualityManagementPlan_WaterQualityManagementPlanID
            HasRequired(a => a.Parcel).WithMany(b => b.WaterQualityManagementPlanParcels).HasForeignKey(c => c.ParcelID).WillCascadeOnDelete(false); // FK_WaterQualityManagementPlanParcel_Parcel_ParcelID
        }
    }
}