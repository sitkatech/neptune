//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequest]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class RegionalSubbasinRevisionRequestConfiguration : EntityTypeConfiguration<RegionalSubbasinRevisionRequest>
    {
        public RegionalSubbasinRevisionRequestConfiguration() : this("dbo"){}

        public RegionalSubbasinRevisionRequestConfiguration(string schema)
        {
            ToTable("RegionalSubbasinRevisionRequest", schema);
            HasKey(x => x.RegionalSubbasinRevisionRequestID);
            Property(x => x.RegionalSubbasinRevisionRequestID).HasColumnName(@"RegionalSubbasinRevisionRequestID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.RegionalSubbasinRevisionRequestGeometry).HasColumnName(@"RegionalSubbasinRevisionRequestGeometry").HasColumnType("geometry").IsRequired();
            Property(x => x.RequestPersonID).HasColumnName(@"RequestPersonID").HasColumnType("int").IsRequired();
            Property(x => x.RegionalSubbasinRevisionRequestStatusID).HasColumnName(@"RegionalSubbasinRevisionRequestStatusID").HasColumnType("int").IsRequired();
            Property(x => x.RequestDate).HasColumnName(@"RequestDate").HasColumnType("datetime").IsRequired();
            Property(x => x.ClosedByPersonID).HasColumnName(@"ClosedByPersonID").HasColumnType("int").IsOptional();
            Property(x => x.ClosedDate).HasColumnName(@"ClosedDate").HasColumnType("datetime").IsOptional();
            Property(x => x.Notes).HasColumnName(@"Notes").HasColumnType("varchar").IsOptional();
            Property(x => x.CloseNotes).HasColumnName(@"CloseNotes").HasColumnType("varchar").IsOptional();

            // Foreign keys
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.RegionalSubbasinRevisionRequests).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_RegionalSubbasinRevisionRequest_TreatmentBMP_TreatmentBMPID
            HasRequired(a => a.RequestPerson).WithMany(b => b.RegionalSubbasinRevisionRequestsWhereYouAreTheRequestPerson).HasForeignKey(c => c.RequestPersonID).WillCascadeOnDelete(false); // FK_RegionalSubbasinRevisionRequest_Person_RequestPersonID_PersonID
            HasOptional(a => a.ClosedByPerson).WithMany(b => b.RegionalSubbasinRevisionRequestsWhereYouAreTheClosedByPerson).HasForeignKey(c => c.ClosedByPersonID).WillCascadeOnDelete(false); // FK_RegionalSubbasinRevisionRequest_Person_ClosedByPersonID_PersonID
        }
    }
}