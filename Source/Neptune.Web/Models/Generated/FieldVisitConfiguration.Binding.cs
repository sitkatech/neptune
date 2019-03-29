//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FieldVisit]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class FieldVisitConfiguration : EntityTypeConfiguration<FieldVisit>
    {
        public FieldVisitConfiguration() : this("dbo"){}

        public FieldVisitConfiguration(string schema)
        {
            ToTable("FieldVisit", schema);
            HasKey(x => x.FieldVisitID);
            Property(x => x.FieldVisitID).HasColumnName(@"FieldVisitID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.FieldVisitStatusID).HasColumnName(@"FieldVisitStatusID").HasColumnType("int").IsRequired();
            Property(x => x.PerformedByPersonID).HasColumnName(@"PerformedByPersonID").HasColumnType("int").IsRequired();
            Property(x => x.VisitDate).HasColumnName(@"VisitDate").HasColumnType("datetime").IsRequired();
            Property(x => x.InventoryUpdated).HasColumnName(@"InventoryUpdated").HasColumnType("bit").IsRequired();
            Property(x => x.FieldVisitTypeID).HasColumnName(@"FieldVisitTypeID").HasColumnType("int").IsRequired();
            Property(x => x.IsFieldVisitVerified).HasColumnName(@"IsFieldVisitVerified").HasColumnType("bit").IsRequired();

            // Foreign keys
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.FieldVisits).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_FieldVisit_TreatmentBMP_TreatmentBMPID
            HasRequired(a => a.PerformedByPerson).WithMany(b => b.FieldVisitsWhereYouAreThePerformedByPerson).HasForeignKey(c => c.PerformedByPersonID).WillCascadeOnDelete(false); // FK_FieldVisit_Person_PerformedByPersonID_PersonID
        }
    }
}