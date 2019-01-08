//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[TreatmentBMPAssessmentPhoto]
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Neptune.Web.Models
{
    public class TreatmentBMPAssessmentPhotoConfiguration : EntityTypeConfiguration<TreatmentBMPAssessmentPhoto>
    {
        public TreatmentBMPAssessmentPhotoConfiguration() : this("dbo"){}

        public TreatmentBMPAssessmentPhotoConfiguration(string schema)
        {
            ToTable("TreatmentBMPAssessmentPhoto", schema);
            HasKey(x => x.TreatmentBMPAssessmentPhotoID);
            Property(x => x.TreatmentBMPAssessmentPhotoID).HasColumnName(@"TreatmentBMPAssessmentPhotoID").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.FileResourceID).HasColumnName(@"FileResourceID").HasColumnType("int").IsRequired();
            Property(x => x.TreatmentBMPAssessmentID).HasColumnName(@"TreatmentBMPAssessmentID").HasColumnType("int").IsRequired();
            Property(x => x.Caption).HasColumnName(@"Caption").HasColumnType("varchar").IsOptional().IsUnicode(false).HasMaxLength(500);

            // Foreign keys
            HasRequired(a => a.FileResource).WithMany(b => b.TreatmentBMPAssessmentPhotos).HasForeignKey(c => c.FileResourceID).WillCascadeOnDelete(false); // FK_TreatmentBMPAssessmentPhoto_FileResource_FileResourceID
            HasRequired(a => a.TreatmentBMPAssessment).WithMany(b => b.TreatmentBMPAssessmentPhotos).HasForeignKey(c => c.TreatmentBMPAssessmentID).WillCascadeOnDelete(false); // FK_TreatmentBMPAssessmentPhoto_TreatmentBMPAssessment_TreatmentBMPAssessmentID
        }
    }
}