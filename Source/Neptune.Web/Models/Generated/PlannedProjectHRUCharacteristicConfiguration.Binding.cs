//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[PlannedProjectHRUCharacteristic]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class PlannedProjectHRUCharacteristicConfiguration : EntityTypeConfiguration<PlannedProjectHRUCharacteristic>
    {
        public PlannedProjectHRUCharacteristicConfiguration() : this("dbo"){}

        public PlannedProjectHRUCharacteristicConfiguration(string schema)
        {
            ToTable("PlannedProjectHRUCharacteristic", schema);
            HasKey(x => x.PlannedProjectHRUCharacteristicID);
            Property(x => x.PlannedProjectHRUCharacteristicID).HasColumnName(@"PlannedProjectHRUCharacteristicID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProjectID).HasColumnName(@"ProjectID").HasColumnType("int").IsRequired();
            Property(x => x.HydrologicSoilGroup).HasColumnName(@"HydrologicSoilGroup").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(5);
            Property(x => x.SlopePercentage).HasColumnName(@"SlopePercentage").HasColumnType("int").IsRequired();
            Property(x => x.ImperviousAcres).HasColumnName(@"ImperviousAcres").HasColumnType("float").IsRequired();
            Property(x => x.LastUpdated).HasColumnName(@"LastUpdated").HasColumnType("datetime").IsRequired();
            Property(x => x.Area).HasColumnName(@"Area").HasColumnType("float").IsRequired();
            Property(x => x.HRUCharacteristicLandUseCodeID).HasColumnName(@"HRUCharacteristicLandUseCodeID").HasColumnType("int").IsRequired();
            Property(x => x.PlannedProjectLoadGeneratingUnitID).HasColumnName(@"PlannedProjectLoadGeneratingUnitID").HasColumnType("int").IsRequired();
            Property(x => x.BaselineImperviousAcres).HasColumnName(@"BaselineImperviousAcres").HasColumnType("float").IsRequired();
            Property(x => x.BaselineHRUCharacteristicLandUseCodeID).HasColumnName(@"BaselineHRUCharacteristicLandUseCodeID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.Project).WithMany(b => b.PlannedProjectHRUCharacteristics).HasForeignKey(c => c.ProjectID).WillCascadeOnDelete(false); // FK_PlannedProjectHRUCharacteristic_Project_ProjectID
            HasRequired(a => a.PlannedProjectLoadGeneratingUnit).WithMany(b => b.PlannedProjectHRUCharacteristics).HasForeignKey(c => c.PlannedProjectLoadGeneratingUnitID).WillCascadeOnDelete(false); // FK_PlannedProjectHRUCharacteristic_PlannedProjectLoadGeneratingUnit_PlannedProjectLoadGeneratingUnitID
        }
    }
}