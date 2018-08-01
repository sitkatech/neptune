//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPType]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeConfiguration : EntityTypeConfiguration<TreatmentBMPType>
    {
        public TreatmentBMPTypeConfiguration() : this("dbo"){}

        public TreatmentBMPTypeConfiguration(string schema)
        {
            ToTable("TreatmentBMPType", schema);
            HasKey(x => x.TreatmentBMPTypeID);
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeName).HasColumnName(@"TreatmentBMPTypeName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.TreatmentBMPTypeDescription).HasColumnName(@"TreatmentBMPTypeDescription").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(1000);

            // Foreign keys

        }
    }
}