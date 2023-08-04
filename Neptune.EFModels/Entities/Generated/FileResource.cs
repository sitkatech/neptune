using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("FileResource")]
[Index("FileResourceGUID", Name = "AK_FileResource_FileResourceGUID", IsUnique = true)]
public partial class FileResource
{
    [Key]
    public int FileResourceID { get; set; }

    public int FileResourceMimeTypeID { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? OriginalBaseFilename { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? OriginalFileExtension { get; set; }

    public Guid FileResourceGUID { get; set; }

    public byte[] FileResourceData { get; set; } = null!;

    public int CreatePersonID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreateDate { get; set; }

    [ForeignKey("CreatePersonID")]
    [InverseProperty("FileResources")]
    public virtual Person CreatePerson { get; set; } = null!;

    [InverseProperty("FileResource")]
    public virtual ICollection<NeptuneHomePageImage> NeptuneHomePageImages { get; set; } = new List<NeptuneHomePageImage>();

    [InverseProperty("FileResource")]
    public virtual ICollection<NeptunePageImage> NeptunePageImages { get; set; } = new List<NeptunePageImage>();

    [InverseProperty("FileResource")]
    public virtual ICollection<OnlandVisualTrashAssessmentObservationPhotoStaging> OnlandVisualTrashAssessmentObservationPhotoStagings { get; set; } = new List<OnlandVisualTrashAssessmentObservationPhotoStaging>();

    [InverseProperty("FileResource")]
    public virtual ICollection<OnlandVisualTrashAssessmentObservationPhoto> OnlandVisualTrashAssessmentObservationPhotos { get; set; } = new List<OnlandVisualTrashAssessmentObservationPhoto>();

    [InverseProperty("LogoFileResource")]
    public virtual ICollection<Organization> Organizations { get; set; } = new List<Organization>();

    [InverseProperty("FileResource")]
    public virtual ICollection<ProjectDocument> ProjectDocuments { get; set; } = new List<ProjectDocument>();

    [InverseProperty("FileResource")]
    public virtual ICollection<TreatmentBMPAssessmentPhoto> TreatmentBMPAssessmentPhotos { get; set; } = new List<TreatmentBMPAssessmentPhoto>();

    [InverseProperty("FileResource")]
    public virtual ICollection<TreatmentBMPDocument> TreatmentBMPDocuments { get; set; } = new List<TreatmentBMPDocument>();

    [InverseProperty("FileResource")]
    public virtual ICollection<TreatmentBMPImage> TreatmentBMPImages { get; set; } = new List<TreatmentBMPImage>();

    [InverseProperty("FileResource")]
    public virtual ICollection<WaterQualityManagementPlanDocument> WaterQualityManagementPlanDocuments { get; set; } = new List<WaterQualityManagementPlanDocument>();

    [InverseProperty("FileResource")]
    public virtual ICollection<WaterQualityManagementPlanPhoto> WaterQualityManagementPlanPhotos { get; set; } = new List<WaterQualityManagementPlanPhoto>();

    [InverseProperty("FileResource")]
    public virtual ICollection<WaterQualityManagementPlanVerify> WaterQualityManagementPlanVerifies { get; set; } = new List<WaterQualityManagementPlanVerify>();
}
