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
            Property(x => x.Area).HasColumnName(@"Area").HasColumnType("float").IsRequired();
            Property(x => x.HRUCharacteristicLandUseCodeID).HasColumnName(@"HRUCharacteristicLandUseCodeID").HasColumnType("int").IsRequired();
            Property(x => x.LoadGeneratingUnitID).HasColumnName(@"LoadGeneratingUnitID").HasColumnType("int").IsRequired();
            Property(x => x.BaselineImperviousAcres).HasColumnName(@"BaselineImperviousAcres").HasColumnType("float").IsOptional();
            Property(x => x.BaselineHRUCharacteristicLandUseCodeID).HasColumnName(@"BaselineHRUCharacteristicLandUseCodeID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.LoadGeneratingUnit).WithMany(b => b.HRUCharacteristics).HasForeignKey(c => c.LoadGeneratingUnitID).WillCascadeOnDelete(false); // FK_HRUCharacteristic_LoadGeneratingUnit_LoadGeneratingUnitID
        }
    }
}