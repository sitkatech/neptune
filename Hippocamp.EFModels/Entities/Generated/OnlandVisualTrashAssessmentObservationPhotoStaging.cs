using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentObservationPhotoStaging")]
    public partial class OnlandVisualTrashAssessmentObservationPhotoStaging
    {
        [Key]
        public int OnlandVisualTrashAssessmentObservationPhotoStagingID { get; set; }
        public int FileResourceID { get; set; }
        public int OnlandVisualTrashAssessmentID { get; set; }

        [ForeignKey(nameof(FileResourceID))]
        [InverseProperty("OnlandVisualTrashAssessmentObservationPhotoStagings")]
        public virtual FileResource FileResource { get; set; }
        [ForeignKey(nameof(OnlandVisualTrashAssessmentID))]
        [InverseProperty("OnlandVisualTrashAssessmentObservationPhotoStagings")]
        public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; }
    }
}
