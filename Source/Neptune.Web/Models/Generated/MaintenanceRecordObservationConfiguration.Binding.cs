//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecordObservation]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class MaintenanceRecordObservationConfiguration : EntityTypeConfiguration<MaintenanceRecordObservation>
    {
        public MaintenanceRecordObservationConfiguration() : this("dbo"){}

        public MaintenanceRecordObservationConfiguration(string schema)
        {
            ToTable("MaintenanceRecordObservation", schema);
            HasKey(x => x.MaintenanceRecordObservationID);
            Property(x => x.MaintenanceRecordObservationID).HasColumnName(@"MaintenanceRecordObservationID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.MaintenanceRecordID).HasColumnName(@"MaintenanceRecordID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeCustomAttributeTypeID).HasColumnName(@"TreatmentBMPTypeCustomAttributeTypeID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.CustomAttributeTypeID).HasColumnName(@"CustomAttributeTypeID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.MaintenanceRecord).WithMany(b => b.MaintenanceRecordObservations).HasForeignKey(c => c.MaintenanceRecordID).WillCascadeOnDelete(false); // FK_MaintenanceRecordObservation_MaintenanceRecord_MaintenanceRecordID
            HasRequired(a => a.TreatmentBMPTypeCustomAttributeType).WithMany(b => b.MaintenanceRecordObservations).HasForeignKey(c => c.TreatmentBMPTypeCustomAttributeTypeID).WillCascadeOnDelete(false); // FK_MaintenanceRecordObservation_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID
            HasRequired(a => a.TreatmentBMPType).WithMany(b => b.MaintenanceRecordObservations).HasForeignKey(c => c.TreatmentBMPTypeID).WillCascadeOnDelete(false); // FK_MaintenanceRecordObservation_TreatmentBMPType_TreatmentBMPTypeID
            HasRequired(a => a.CustomAttributeType).WithMany(b => b.MaintenanceRecordObservations).HasForeignKey(c => c.CustomAttributeTypeID).WillCascadeOnDelete(false); // FK_MaintenanceRecordObservation_CustomAttributeType_CustomAttributeTypeID
        }
    }
}