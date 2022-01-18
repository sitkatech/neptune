using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ObservationTypeSpecification")]
    [Index(nameof(ObservationTypeSpecificationDisplayName), Name = "AK_ObservationTypeSpecification_ObservationTypeSpecificationDisplayName", IsUnique = true)]
    [Index(nameof(ObservationTypeSpecificationName), Name = "AK_ObservationTypeSpecification_ObservationTypeSpecificationName", IsUnique = true)]
    public partial class ObservationTypeSpecification
    {
        public ObservationTypeSpecification()
        {
            TreatmentBMPAssessmentObservationTypes = new HashSet<TreatmentBMPAssessmentObservationType>();
        }

        [Key]
        public int ObservationTypeSpecificationID { get; set; }
        [Required]
        [StringLength(100)]
        public string ObservationTypeSpecificationName { get; set; }
        [Required]
        [StringLength(100)]
        public string ObservationTypeSpecificationDisplayName { get; set; }
        public int SortOrder { get; set; }
        public int ObservationTypeCollectionMethodID { get; set; }
        public int ObservationTargetTypeID { get; set; }
        public int ObservationThresholdTypeID { get; set; }

        [ForeignKey(nameof(ObservationTargetTypeID))]
        [InverseProperty("ObservationTypeSpecifications")]
        public virtual ObservationTargetType ObservationTargetType { get; set; }
        [ForeignKey(nameof(ObservationThresholdTypeID))]
        [InverseProperty("ObservationTypeSpecifications")]
        public virtual ObservationThresholdType ObservationThresholdType { get; set; }
        [ForeignKey(nameof(ObservationTypeCollectionMethodID))]
        [InverseProperty("ObservationTypeSpecifications")]
        public virtual ObservationTypeCollectionMethod ObservationTypeCollectionMethod { get; set; }
        [InverseProperty(nameof(TreatmentBMPAssessmentObservationType.ObservationTypeSpecification))]
        public virtual ICollection<TreatmentBMPAssessmentObservationType> TreatmentBMPAssessmentObservationTypes { get; set; }
    }
}
