//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservationValue]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class MaintenanceRecordObservationValueConfiguration : EntityTypeConfiguration<MaintenanceRecordObservationValue>
    {
        public MaintenanceRecordObservationValueConfiguration() : this("dbo"){}

        public MaintenanceRecordObservationValueConfiguration(string schema)
        {
            ToTable("MaintenanceRecordObservationValue", schema);
            HasKey(x => x.MaintenanceRecordObservationValueID);
            Property(x => x.MaintenanceRecordObservationValueID).HasColumnName(@"MaintenanceRecordObservationValueID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.MaintenanceRecordObservationID).HasColumnName(@"MaintenanceRecordObservationID").HasColumnType("int").IsRequired();
            Property(x => x.ObservationValue).HasColumnName(@"ObservationValue").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(1000);

            // Foreign keys
            HasRequired(a => a.MaintenanceRecordObservation).WithMany(b => b.MaintenanceRecordObservationValues).HasForeignKey(c => c.MaintenanceRecordObservationID).WillCascadeOnDelete(false); // FK_MaintenanceRecordObservationValue_MaintenanceRecordObservation_MaintenanceRecordObservationID
        }
    }
}