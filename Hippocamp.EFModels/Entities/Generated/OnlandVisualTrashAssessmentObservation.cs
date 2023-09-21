using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessmentObservation")]
    [Index("LocationPoint", Name = "SPATIAL_OnlandVisualTrashAssessmentObservation_LocationPoint")]
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
        [Unicode(false)]
        public string Note { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ObservationDatetime { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry LocationPoint4326 { get; set; }

        [ForeignKey("OnlandVisualTrashAssessmentID")]
        [InverseProperty("OnlandVisualTrashAssessmentObservations")]
        public virtual OnlandVisualTrashAssessment OnlandVisualTrashAssessment { get; set; }
        [InverseProperty("OnlandVisualTrashAssessmentObservation")]
        public virtual ICollection<OnlandVisualTrashAssessmentObservationPhoto> OnlandVisualTrashAssessmentObservationPhotos { get; set; }
    }
}
