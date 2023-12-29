using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("OnlandVisualTrashAssessmentObservationPhotoStaging")]
public partial class OnlandVisualTrashAssessmentObservationPhotoStaging
{
    [Key]
    public int OnlandVisualTrashAssessmentObservationPhotoStagingID { get; set; }

    public int FileResourceID { get; set; }

    public int OnlandVisualTrashAssessmentID { get; set; }

    [ForeignKey("FileResourceID")]
    [InverseProperty("OnlandVisualTrashAssessmentObservationPhotoStagings")]
    public virtual FileResource FileResource { get; set; } = null!;

    [ForeignKey("OnlandVisualTrashAssessmentID")]
    [InverseProperty("OnlandVisualTrashAssessmentObservationPhotoStagings")]
    public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; } = null!;
}
