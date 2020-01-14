//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPOperationMonth]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPOperationMonthConfiguration : EntityTypeConfiguration<TreatmentBMPOperationMonth>
    {
        public TreatmentBMPOperationMonthConfiguration() : this("dbo"){}

        public TreatmentBMPOperationMonthConfiguration(string schema)
        {
            ToTable("TreatmentBMPOperationMonth", schema);
            HasKey(x => x.TreatmentBMPOperationMonthID);
            Property(x => x.TreatmentBMPOperationMonthID).HasColumnName(@"TreatmentBMPOperationMonthID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.OperationMonth).HasColumnName(@"OperationMonth").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.TreatmentBMPOperationMonths).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_TreatmentBMPOperationMonth_TreatmentBMP_TreatmentBMPID
        }
    }
}