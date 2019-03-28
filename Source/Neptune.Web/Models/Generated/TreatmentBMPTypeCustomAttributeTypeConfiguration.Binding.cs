//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPTypeCustomAttributeType]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPTypeCustomAttributeTypeConfiguration : EntityTypeConfiguration<TreatmentBMPTypeCustomAttributeType>
    {
        public TreatmentBMPTypeCustomAttributeTypeConfiguration() : this("dbo"){}

        public TreatmentBMPTypeCustomAttributeTypeConfiguration(string schema)
        {
            ToTable("TreatmentBMPTypeCustomAttributeType", schema);
            HasKey(x => x.TreatmentBMPTypeCustomAttributeTypeID);
            Property(x => x.TreatmentBMPTypeCustomAttributeTypeID).HasColumnName(@"TreatmentBMPTypeCustomAttributeTypeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.CustomAttributeTypeID).HasColumnName(@"CustomAttributeTypeID").HasColumnType("int").IsRequired();
            Property(x => x.SortOrder).HasColumnName(@"SortOrder").HasColumnType("int").IsOptional();

            // Foreign keys
            HasRequired(a => a.TreatmentBMPType).WithMany(b => b.TreatmentBMPTypeCustomAttributeTypes).HasForeignKey(c => c.TreatmentBMPTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPType_TreatmentBMPTypeID
            HasRequired(a => a.CustomAttributeType).WithMany(b => b.TreatmentBMPTypeCustomAttributeTypes).HasForeignKey(c => c.CustomAttributeTypeID).WillCascadeOnDelete(false); // FK_TreatmentBMPTypeCustomAttributeType_CustomAttributeType_CustomAttributeTypeID
        }
    }
}