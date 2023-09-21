using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPAssessmentPhoto")]
    public partial class TreatmentBMPAssessmentPhoto
    {
        [Key]
        public int TreatmentBMPAssessmentPhotoID { get; set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPAssessmentID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Caption { get; set; }

        [ForeignKey("FileResourceID")]
        [InverseProperty("TreatmentBMPAssessmentPhotos")]
        public virtual FileResource FileResource { get; set; }
        [ForeignKey("TreatmentBMPAssessmentID")]
        [InverseProperty("TreatmentBMPAssessmentPhotos")]
        public virtual TreatmentBMPAssessment TreatmentBMPAssessment { get; set; }
    }
}
