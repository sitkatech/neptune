using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentObservation")]
    [Index(nameof(LocationPoint), Name = "SPATIAL_OnlandVisualTrashAssessmentObservation_LocationPoint")]
    public partial class OnlandVisualTrashAssessmentObservation
    {
        public OnlandVisualTrashAssessmentObservation()
        {
            OnlandVisualTrashAssessmentObservationPhotos = new HashSet<OnlandVisualTrashAssessmentObservationPhoto>();
        }

        [Key]
        public int OnlandVisualTrashAssessmentObservationID { get; set; }
        public int OnlandVisualTrashAssessmentID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LocationPoint { get; set; }
        [StringLength(500)]
        public string Note { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ObservationDatetime { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry LocationPoint4326 { get; set; }

        [ForeignKey(nameof(OnlandVisualTrashAssessmentID))]
        [InverseProperty("OnlandVisualTrashAssessmentObservations")]
        public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessmentObservationPhoto.OnlandVisualTrashAssessmentObservation))]
        public virtual ICollection<OnlandVisualTrashAssessmentObservationPhoto> OnlandVisualTrashAssessmentObservationPhotos { get; set; }
    }
}
