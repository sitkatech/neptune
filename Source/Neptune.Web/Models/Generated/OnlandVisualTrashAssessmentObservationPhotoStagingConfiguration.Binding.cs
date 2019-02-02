//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentObservationPhotoStaging]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class OnlandVisualTrashAssessmentObservationPhotoStagingConfiguration : EntityTypeConfiguration<OnlandVisualTrashAssessmentObservationPhotoStaging>
    {
        public OnlandVisualTrashAssessmentObservationPhotoStagingConfiguration() : this("dbo"){}

        public OnlandVisualTrashAssessmentObservationPhotoStagingConfiguration(string schema)
        {
            ToTable("OnlandVisualTrashAssessmentObservationPhotoStaging", schema);
            HasKey(x => x.OnlandVisualTrashAssessmentObservationPhotoStagingID);
            Property(x => x.OnlandVisualTrashAssessmentObservationPhotoStagingID).HasColumnName(@"OnlandVisualTrashAssessmentObservationPhotoStagingID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FileResourceID).HasColumnName(@"FileResourceID").HasColumnType("int").IsRequired();
            Property(x => x.OnlandVisualTrashAssessmentID).HasColumnName(@"OnlandVisualTrashAssessmentID").HasColumnType("int").IsRequired();

            // Foreign keys
            HasRequired(a => a.FileResource).WithMany(b => b.OnlandVisualTrashAssessmentObservationPhotoStagings).HasForeignKey(c => c.FileResourceID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessmentObservationPhotoStaging_FileResource_FileResourceID
            HasRequired(a => a.OnlandVisualTrashAssessment).WithMany(b => b.OnlandVisualTrashAssessmentObservationPhotoStagings).HasForeignKey(c => c.OnlandVisualTrashAssessmentID).WillCascadeOnDelete(false); // FK_OnlandVisualTrashAssessmentObservationPhotoStaging_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID
        }
    }
}