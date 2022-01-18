using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPAssessment")]
    [Index(nameof(TreatmentBMPAssessmentID), nameof(TreatmentBMPID), Name = "AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPID", IsUnique = true)]
    [Index(nameof(TreatmentBMPAssessmentID), nameof(TreatmentBMPTypeID), Name = "AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID", IsUnique = true)]
    public partial class TreatmentBMPAssessment
    {
        public TreatmentBMPAssessment()
        {
            TreatmentBMPAssessmentPhotos = new HashSet<TreatmentBMPAssessmentPhoto>();
            TreatmentBMPObservationTreatmentBMPAssessments = new HashSet<TreatmentBMPObservation>();
            TreatmentBMPObservationTreatmentBMPs = new HashSet<TreatmentBMPObservation>();
        }

        [Key]
        public int TreatmentBMPAssessmentID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int FieldVisitID { get; set; }
        public int TreatmentBMPAssessmentTypeID { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        public double? AssessmentScore { get; set; }
        public bool IsAssessmentComplete { get; set; }

        [ForeignKey(nameof(FieldVisitID))]
        [InverseProperty("TreatmentBMPAssessmentFieldVisits")]
        public virtual FieldVisit FieldVisit { get; set; }
        public virtual FieldVisit FieldVisitNavigation { get; set; }
        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("TreatmentBMPAssessmentTreatmentBMPs")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        [ForeignKey(nameof(TreatmentBMPAssessmentTypeID))]
        [InverseProperty("TreatmentBMPAssessments")]
        public virtual TreatmentBMPAssessmentType TreatmentBMPAssessmentType { get; set; }
        public virtual TreatmentBMP TreatmentBMPNavigation { get; set; }
        [ForeignKey(nameof(TreatmentBMPTypeID))]
        [InverseProperty("TreatmentBMPAssessments")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        [InverseProperty(nameof(TreatmentBMPAssessmentPhoto.TreatmentBMPAssessment))]
        public virtual ICollection<TreatmentBMPAssessmentPhoto> TreatmentBMPAssessmentPhotos { get; set; }
        [InverseProperty(nameof(TreatmentBMPObservation.TreatmentBMPAssessment))]
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservationTreatmentBMPAssessments { get; set; }
        public virtual ICollection<TreatmentBMPObservation> TreatmentBMPObservationTreatmentBMPs { get; set; }
    }
}
