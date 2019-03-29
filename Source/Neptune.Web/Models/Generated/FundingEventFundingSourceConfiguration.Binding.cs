//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[FundingEventFundingSource]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class FundingEventFundingSourceConfiguration : EntityTypeConfiguration<FundingEventFundingSource>
    {
        public FundingEventFundingSourceConfiguration() : this("dbo"){}

        public FundingEventFundingSourceConfiguration(string schema)
        {
            ToTable("FundingEventFundingSource", schema);
            HasKey(x => x.FundingEventFundingSourceID);
            Property(x => x.FundingEventFundingSourceID).HasColumnName(@"FundingEventFundingSourceID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FundingSourceID).HasColumnName(@"FundingSourceID").HasColumnType("int").IsRequired();
            Property(x => x.FundingEventID).HasColumnName(@"FundingEventID").HasColumnType("int").IsRequired();
            Property(x => x.Amount).HasColumnName(@"Amount").HasColumnType("money").IsOptional().HasPrecision(19,4);

            // Foreign keys
            HasRequired(a => a.FundingSource).WithMany(b => b.FundingEventFundingSources).HasForeignKey(c => c.FundingSourceID).WillCascadeOnDelete(false); // FK_FundingEventFundingSource_FundingSource_FundingSourceID
            HasRequired(a => a.FundingEvent).WithMany(b => b.FundingEventFundingSources).HasForeignKey(c => c.FundingEventID).WillCascadeOnDelete(false); // FK_FundingEventFundingSource_FundingEvent_FundingEventID
        }
    }
}