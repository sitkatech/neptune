//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[HRUCharacteristic]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class HRUCharacteristicConfiguration : EntityTypeConfiguration<HRUCharacteristic>
    {
        public HRUCharacteristicConfiguration() : this("dbo"){}

        public HRUCharacteristicConfiguration(string schema)
        {
            ToTable("HRUCharacteristic", schema);
            HasKey(x => x.HRUCharacteristicID);
            Property(x => x.HRUCharacteristicID).HasColumnName(@"HRUCharacteristicID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.HydrologicSoilGroup).HasColumnName(@"HydrologicSoilGroup").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(5);
            Property(x => x.SlopePercentage).HasColumnName(@"SlopePercentage").HasColumnType("int").IsRequired();
            Property(x => x.ImperviousAcres).HasColumnName(@"ImperviousAcres").HasColumnType("float").IsRequired();
            Property(x => x.LastUpdated).HasColumnName(@"LastUpdated").HasColumnType("datetime").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsOptional();
            Property(x => x.WaterQualityManagementPlanID).HasColumnName(@"WaterQualityManagementPlanID").HasColumnType("int").IsOptional();
            Property(x => x.NetworkCatchmentID).HasColumnName(@"NetworkCatchmentID").HasColumnType("int").IsOptional();
            Property(x => x.Area).HasColumnName(@"Area").HasColumnType("float").IsRequired();
            Property(x => x.HRUCharacteristicLandUseCodeID).HasColumnName(@"HRUCharacteristicLandUseCodeID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasOptional(a => a.TreatmentBMP).WithMany(b => b.HRUCharacteristics).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_HRUCharacteristic_TreatmentBMP_TreatmentBMPID
            HasOptional(a => a.WaterQualityManagementPlan).WithMany(b => b.HRUCharacteristics).HasForeignKey(c => c.WaterQualityManagementPlanID).WillCascadeOnDelete(false); // FK_HRUCharacteristic_WaterQualityManagementPlan_WaterQualityManagementPlanID
            HasOptional(a => a.NetworkCatchment).WithMany(b => b.HRUCharacteristics).HasForeignKey(c => c.NetworkCatchmentID).WillCascadeOnDelete(false); // FK_HRUCharacteristic_NetworkCatchment_NetworkCatchmentID
        }
    }
}