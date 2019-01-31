//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhoto]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentObservationPhotoConfiguration : EntityTypeConfiguration<OnlandVisualTrashAssessmentObservationPhoto>
    {
        public OnlandVisualTrashAssessmentObservationPhotoConfiguration() : this("dbo"){}

        public OnlandVisualTrashAssessmentObservationPhotoConfiguration(string schema)
        {
            ToTable("OnlandVisualTrashAssessmentObservationPhoto", schema);
            HasKey(x => x.OnlandVisualTrashAssessmentObservationPhotoID);
            Property(x => x.OnlandVisualTrashAssessmentObservationPhotoID).HasColumnName(@"OnlandVisualTrashAssessmentObservationPhotoID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FileResourceID).HasColumnName(@"FileResourceID").HasColumnType("int").IsRequired();
            Property(x => x.OnlandVisualTrashAssessmentObservationID).HasColumnName(@"OnlandVisualTrashAssessmentObservationID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.FileResource).WithMany(b => b.OnlandVisualTrashAssessmentObservationPhotos).HasForeignKey(c => c.FileResourceID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessmentObservationPhoto_FileResource_FileResourceID
            HasRequired(a => a.OnlandVisualTrashAssessmentObservation).WithMany(b => b.OnlandVisualTrashAssessmentObservationPhotos).HasForeignKey(c => c.OnlandVisualTrashAssessmentObservationID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessmentObservationPhoto_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessmentObservationID
        }
    }
}