using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("OnlandVisualTrashAssessment")]
    [Index(nameof(DraftGeometry), Name = "SPATIAL_OnlandVisualTrashAssessment_DraftGeometry")]
    public partial class OnlandVisualTrashAssessment
    {
        public OnlandVisualTrashAssessment()
        {
            OnlandVisualTrashAssessmentObservationPhotoStagings = new HashSet<OnlandVisualTrashAssessmentObservationPhotoStaging>();
            OnlandVisualTrashAssessmentObservations = new HashSet<OnlandVisualTrashAssessmentObservation>();
            OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes = new HashSet<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType>();
        }

        [Key]
        public int OnlandVisualTrashAssessmentID { get; set; }
        public int CreatedByPersonID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int? OnlandVisualTrashAssessmentAreaID { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        public int StormwaterJurisdictionID { get; set; }
        public bool? AssessingNewArea { get; set; }
        public int OnlandVisualTrashAssessmentStatusID { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry DraftGeometry { get; set; }
        public bool? IsDraftGeometryManuallyRefined { get; set; }
        public int? OnlandVisualTrashAssessmentScoreID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CompletedDate { get; set; }
        [StringLength(100)]
        public string DraftAreaName { get; set; }
        [StringLength(500)]
        public string DraftAreaDescription { get; set; }
        public bool IsTransectBackingAssessment { get; set; }
        public bool IsProgressAssessment { get; set; }

        [ForeignKey(nameof(CreatedByPersonID))]
        [InverseProperty(nameof(Person.OnlandVisualTrashAssessments))]
        public virtual Person CreatedByPerson { get; set; }
        [ForeignKey(nameof(OnlandVisualTrashAssessmentAreaID))]
        [InverseProperty("OnlandVisualTrashAssessmentOnlandVisualTrashAssessmentArea")]
        public virtual OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentArea { get; set; }
        public virtual OnlandVisualTrashAssessmentArea OnlandVisualTrashAssessmentAreaNavigation { get; set; }
        [ForeignKey(nameof(OnlandVisualTrashAssessmentScoreID))]
        [InverseProperty("OnlandVisualTrashAssessments")]
        public virtual OnlandVisualTrashAssessmentScore OnlandVisualTrashAssessmentScore { get; set; }
        [ForeignKey(nameof(OnlandVisualTrashAssessmentStatusID))]
        [InverseProperty("OnlandVisualTrashAssessments")]
        public virtual OnlandVisualTrashAssessmentStatus OnlandVisualTrashAssessmentStatus { get; set; }
        [ForeignKey(nameof(StormwaterJurisdictionID))]
        [InverseProperty("OnlandVisualTrashAssessments")]
        public virtual StormwaterJurisdiction StormwaterJurisdiction { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessmentObservationPhotoStaging.OnlandVisualTrashAssessment))]
        public virtual ICollection<OnlandVisualTrashAssessmentObservationPhotoStaging> OnlandVisualTrashAssessmentObservationPhotoStagings { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessmentObservation.OnlandVisualTrashAssessment))]
        public virtual ICollection<OnlandVisualTrashAssessmentObservation> OnlandVisualTrashAssessmentObservations { get; set; }
        [InverseProperty(nameof(OnlandVisualTrashAssessmentPreliminarySourceIdentificationType.OnlandVisualTrashAssessment))]
        public virtual ICollection<OnlandVisualTrashAssessmentPreliminarySourceIdentificationType> OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypes { get; set; }
    }
}
