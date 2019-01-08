//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEvent]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class FundingEventConfiguration : EntityTypeConfiguration<FundingEvent>
    {
        public FundingEventConfiguration() : this("dbo"){}

        public FundingEventConfiguration(string schema)
        {
            ToTable("FundingEvent", schema);
            HasKey(x => x.FundingEventID);
            Property(x => x.FundingEventID).HasColumnName(@"FundingEventID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.FundingEventTypeID).HasColumnName(@"FundingEventTypeID").HasColumnType("int").IsRequired();
            Property(x => x.Year).HasColumnName(@"Year").HasColumnType("int").IsRequired();
            Property(x => x.Description).HasColumnName(@"Description").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.FundingEvents).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_FundingEvent_TreatmentBMP_TreatmentBMPID
        }
    }
}