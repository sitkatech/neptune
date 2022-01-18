using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPObservation")]
    public partial class TreatmentBMPObservation
    {
        [Key]
        public int TreatmentBMPObservationID { get; set; }
        public int TreatmentBMPAssessmentID { get; set; }
        public int TreatmentBMPTypeAssessmentObservationTypeID { get; set; }
        public int TreatmentBMPTypeID { get; set; }
        public int TreatmentBMPAssessmentObservationTypeID { get; set; }
        [Required]
        public string ObservationData { get; set; }

        public virtual TreatmentBMPAssessment TreatmentBMP { get; set; }
        [ForeignKey(nameof(TreatmentBMPAssessmentID))]
        [InverseProperty("TreatmentBMPObservationTreatmentBMPAssessments")]
        public virtual TreatmentBMPAssessment TreatmentBMPAssessment { get; set; }
        [ForeignKey(nameof(TreatmentBMPAssessmentObservationTypeID))]
        [InverseProperty("TreatmentBMPObservations")]
        public virtual TreatmentBMPAssessmentObservationType TreatmentBMPAssessmentObservationType { get; set; }
        public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMPNavigation { get; set; }
        [ForeignKey(nameof(TreatmentBMPTypeID))]
        [InverseProperty("TreatmentBMPObservations")]
        public virtual TreatmentBMPType TreatmentBMPType { get; set; }
        [ForeignKey(nameof(TreatmentBMPTypeAssessmentObservationTypeID))]
        [InverseProperty("TreatmentBMPObservationTreatmentBMPTypeAssessmentObservationTypes")]
        public virtual TreatmentBMPTypeAssessmentObservationType TreatmentBMPTypeAssessmentObservationType { get; set; }
    }
}
