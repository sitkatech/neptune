//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[MaintenanceRecord]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class MaintenanceRecordConfiguration : EntityTypeConfiguration<MaintenanceRecord>
    {
        public MaintenanceRecordConfiguration() : this("dbo"){}

        public MaintenanceRecordConfiguration(string schema)
        {
            ToTable("MaintenanceRecord", schema);
            HasKey(x => x.MaintenanceRecordID);
            Property(x => x.MaintenanceRecordID).HasColumnName(@"MaintenanceRecordID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.FieldVisitID).HasColumnName(@"FieldVisitID").HasColumnType("int").IsRequired();
            Property(x => x.MaintenanceRecordDescription).HasColumnName(@"MaintenanceRecordDescription").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);
            Property(x => x.MaintenanceRecordTypeID).HasColumnName(@"MaintenanceRecordTypeID").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.MaintenanceRecords).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID
            HasRequired(a => a.FieldVisit).WithMany(b => b.MaintenanceRecords).HasForeignKey(c => c.FieldVisitID).WillCascadeOnDelete(false); // FK_MaintenanceRecord_FieldVisit_FieldVisitID
        }
    }
}