//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[ProjectHRUCharacteristic]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class ProjectHRUCharacteristicConfiguration : EntityTypeConfiguration<ProjectHRUCharacteristic>
    {
        public ProjectHRUCharacteristicConfiguration() : this("dbo"){}

        public ProjectHRUCharacteristicConfiguration(string schema)
        {
            ToTable("ProjectHRUCharacteristic", schema);
            HasKey(x => x.ProjectHRUCharacteristicID);
            Property(x => x.ProjectHRUCharacteristicID).HasColumnName(@"ProjectHRUCharacteristicID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ProjectID).HasColumnName(@"ProjectID").HasColumnType("int").IsRequired();
            Property(x => x.HydrologicSoilGroup).HasColumnName(@"HydrologicSoilGroup").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(5);
            Property(x => x.SlopePercentage).HasColumnName(@"SlopePercentage").HasColumnType("int").IsRequired();
            Property(x => x.ImperviousAcres).HasColumnName(@"ImperviousAcres").HasColumnType("float").IsRequired();
            Property(x => x.LastUpdated).HasColumnName(@"LastUpdated").HasColumnType("datetime").IsRequired();
            Property(x => x.Area).HasColumnName(@"Area").HasColumnType("float").IsRequired();
            Property(x => x.HRUCharacteristicLandUseCodeID).HasColumnName(@"HRUCharacteristicLandUseCodeID").HasColumnType("int").IsRequired();
            Property(x => x.ProjectLoadGeneratingUnitID).HasColumnName(@"ProjectLoadGeneratingUnitID").HasColumnType("int").IsRequired();
            Property(x => x.BaselineImperviousAcres).HasColumnName(@"BaselineImperviousAcres").HasColumnType("float").IsRequired();
            Property(x => x.BaselineHRUCharacteristicLandUseCodeID).HasColumnName(@"BaselineHRUCharacteristicLandUseCodeID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.Project).WithMany(b => b.ProjectHRUCharacteristics).HasForeignKey(c => c.ProjectID).WillCascadeOnDelete(false); // FK_ProjectHRUCharacteristic_Project_ProjectID
            HasRequired(a => a.ProjectLoadGeneratingUnit).WithMany(b => b.ProjectHRUCharacteristics).HasForeignKey(c => c.ProjectLoadGeneratingUnitID).WillCascadeOnDelete(false); // FK_ProjectHRUCharacteristic_ProjectLoadGeneratingUnit_ProjectLoadGeneratingUnitID
        }
    }
}