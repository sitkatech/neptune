using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentObservationPhoto")]
    public partial class OnlandVisualTrashAssessmentObservationPhoto
    {
        [Key]
        public int OnlandVisualTrashAssessmentObservationPhotoID { get; set; }
        public int FileResourceID { get; set; }
        public int OnlandVisualTrashAssessmentObservationID { get; set; }

        [ForeignKey("FileResourceID")]
        [InverseProperty("OnlandVisualTrashAssessmentObservationPhotos")]
        public virtual FileResource FileResource { get; set; }
        [ForeignKey("OnlandVisualTrashAssessmentObservationID")]
        [InverseProperty("OnlandVisualTrashAssessmentObservationPhotos")]
        public virtual OnlandVisualTrashAssessmentObservation OnlandVisualTrashAssessmentObservation { get; set; }
    }
}
