using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("FileResource")]
    [Index(nameof(FileResourceGUID), Name = "AK_FileResource_FileResourceGUID", IsUnique = true)]
    public partial class FileResource
    {
        public FileResource()
        {
            NeptuneHomePageImages = new HashSet<NeptuneHomePageImage>();
            NeptunePageImages = new HashSet<NeptunePageImage>();
            OnlandVisualTrashAssessmentObservationPhotoStagings = new HashSet<OnlandVisualTrashAssessmentObservationPhotoStaging>();
            OnlandVisualTrashAssessmentObservationPhotos = new HashSet<OnlandVisualTrashAssessmentObservationPhoto>();
            Organizations = new HashSet<Organization>();
            ProjectDocuments = new HashSet<ProjectDocument>();
            TreatmentBMPAssessmentPhotos = new HashSet<TreatmentBMPAssessmentPhoto>();
            TreatmentBMPDocuments = new HashSet<TreatmentBMPDocument>();
            TreatmentBMPImages = new HashSet<TreatmentBMPImage>();
            WaterQualityManagementPlanDocuments = new HashSet<WaterQualityManagementPlanDocument>();
            WaterQualityManagementPlanPhotos = new HashSet<WaterQualityManagementPlanPhoto>();
            WaterQualityManagementPlanVerifies = new HashSet<WaterQualityManagementPlanVerify>();
        }

        [Key]
        public int FileResourceID { get; set; }
        public int FileResourceMimeTypeID { get; set; }
        [Required]
        [StringLength(255)]
        public string OriginalBaseFilename { get; set; }
        [Required]
        [StringLength(255)]
        public string OriginalFileExtension { get; set; }
        public Guid FileResourceGUID { get; set; }
        [Required]
        public byte[] FileResourceData { get; set; }
        public int CreatePersonID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [ForeignKey(nameof(CreatePersonID))]
        [InverseProperty(nameof(Person.FileResources))]
        public virtual Person CreatePerson { get; set; }
        [ForeignKey(nameof(FileResourceMimeTypeID))]
        [InverseProperty("FileResources")]
        public virtual FileResourceMimeType FileResourceMimeType { get; set; }
        [InverseProperty(nameof(NeptuneHomePageImage.FileResource))]
        public virtual ICollection<NeptuneHomePageImage> NeptuneHomePageImages { get; set; }
        [InverseProperty(nameof(NeptunePageImage.FileResource))]
        public virtual ICollection<NeptunePageImage> NeptunePageImages { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessmentObservationPhotoStaging.FileResource))]
        public virtual ICollection<OnlandVisualTrashAssessmentObservationPhotoStaging> OnlandVisualTrashAssessmentObservationPhotoStagings { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessmentObservationPhoto.FileResource))]
        public virtual ICollection<OnlandVisualTrashAssessmentObservationPhoto> OnlandVisualTrashAssessmentObservationPhotos { get; set; }
        [InverseProperty(nameof(Organization.LogoFileResource))]
        public virtual ICollection<Organization> Organizations { get; set; }
        [InverseProperty(nameof(ProjectDocument.FileResource))]
        public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; }
        [InverseProperty(nameof(TreatmentBMPAssessmentPhoto.FileResource))]
        public virtual ICollection<TreatmentBMPAssessmentPhoto> TreatmentBMPAssessmentPhotos { get; set; }
        [InverseProperty(nameof(TreatmentBMPDocument.FileResource))]
        public virtual ICollection<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; }
        [InverseProperty(nameof(TreatmentBMPImage.FileResource))]
        public virtual ICollection<TreatmentBMPImage> TreatmentBMPImages { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanDocument.FileResource))]
        public virtual ICollection<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanPhoto.FileResource))]
        public virtual ICollection<WaterQualityManagementPlanPhoto> WaterQualityManagementPlanPhotos { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanVerify.FileResource))]
        public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; }
    }
}
