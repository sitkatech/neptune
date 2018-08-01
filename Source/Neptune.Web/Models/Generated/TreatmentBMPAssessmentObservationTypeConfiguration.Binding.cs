//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentObservationType]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentObservationTypeConfiguration : EntityTypeConfiguration<TreatmentBMPAssessmentObservationType>
    {
        public TreatmentBMPAssessmentObservationTypeConfiguration() : this("dbo"){}

        public TreatmentBMPAssessmentObservationTypeConfiguration(string schema)
        {
            ToTable("TreatmentBMPAssessmentObservationType", schema);
            HasKey(x => x.TreatmentBMPAssessmentObservationTypeID);
            Property(x => x.TreatmentBMPAssessmentObservationTypeID).HasColumnName(@"TreatmentBMPAssessmentObservationTypeID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.TenantID).HasColumnName(@"TenantID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPAssessmentObservationTypeName).HasColumnName(@"TreatmentBMPAssessmentObservationTypeName").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(100);
            Property(x => x.ObservationTypeSpecificationID).HasColumnName(@"ObservationTypeSpecificationID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPAssessmentObservationTypeSchema).HasColumnName(@"TreatmentBMPAssessmentObservationTypeSchema").HasColumnType("nvarchar").IsRequired();

            // Foreign keys

        }
    }
}