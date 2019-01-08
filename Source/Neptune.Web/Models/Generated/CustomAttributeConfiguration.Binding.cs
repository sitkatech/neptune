//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[CustomAttribute]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class CustomAttributeConfiguration : EntityTypeConfiguration<CustomAttribute>
    {
        public CustomAttributeConfiguration() : this("dbo"){}

        public CustomAttributeConfiguration(string schema)
        {
            ToTable("CustomAttribute", schema);
            HasKey(x => x.CustomAttributeID);
            Property(x => x.CustomAttributeID).HasColumnName(@"CustomAttributeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TreatmentBMPID).HasColumnName(@"TreatmentBMPID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeCustomAttributeTypeID).HasColumnName(@"TreatmentBMPTypeCustomAttributeTypeID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPTypeID).HasColumnName(@"TreatmentBMPTypeID").HasColumnType("int").IsRequired();
            Property(x => x.CustomAttributeTypeID).HasColumnName(@"CustomAttributeTypeID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.TreatmentBMP).WithMany(b => b.CustomAttributes).HasForeignKey(c => c.TreatmentBMPID).WillCascadeOnDelete(false); // FK_CustomAttribute_TreatmentBMP_TreatmentBMPID
            HasRequired(a => a.TreatmentBMPTypeCustomAttributeType).WithMany(b => b.CustomAttributes).HasForeignKey(c => c.TreatmentBMPTypeCustomAttributeTypeID).WillCascadeOnDelete(false); // FK_CustomAttribute_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID
            HasRequired(a => a.TreatmentBMPType).WithMany(b => b.CustomAttributes).HasForeignKey(c => c.TreatmentBMPTypeID).WillCascadeOnDelete(false); // FK_CustomAttribute_TreatmentBMPType_TreatmentBMPTypeID
            HasRequired(a => a.CustomAttributeType).WithMany(b => b.CustomAttributes).HasForeignKey(c => c.CustomAttributeTypeID).WillCascadeOnDelete(false); // FK_CustomAttribute_CustomAttributeType_CustomAttributeTypeID
        }
    }
}