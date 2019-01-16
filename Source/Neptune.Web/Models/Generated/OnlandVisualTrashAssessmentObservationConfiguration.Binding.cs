//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservation]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentObservationConfiguration : EntityTypeConfiguration<OnlandVisualTrashAssessmentObservation>
    {
        public OnlandVisualTrashAssessmentObservationConfiguration() : this("dbo"){}

        public OnlandVisualTrashAssessmentObservationConfiguration(string schema)
        {
            ToTable("OnlandVisualTrashAssessmentObservation", schema);
            HasKey(x => x.OnlandVisualTrashAssessmentObservationID);
            Property(x => x.OnlandVisualTrashAssessmentObservationID).HasColumnName(@"OnlandVisualTrashAssessmentObservationID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.OnlandVisualTrashAssessmentID).HasColumnName(@"OnlandVisualTrashAssessmentID").HasColumnType("int").IsRequired();
            Property(x => x.LocationPoint).HasColumnName(@"LocationPoint").HasColumnType("geometry").IsRequired();
            Property(x => x.Note).HasColumnName(@"Note").HasColumnType("varchar").IsRequired().IsUnicode(false).HasMaxLength(500);
            Property(x => x.ObservationDatetime).HasColumnName(@"ObservationDatetime").HasColumnType("datetime").IsRequired();

            // Foreign keys
            HasRequired(a => a.OnlandVisualTrashAssessment).WithMany(b => b.OnlandVisualTrashAssessmentObservations).HasForeignKey(c => c.OnlandVisualTrashAssessmentID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID
        }
    }
}