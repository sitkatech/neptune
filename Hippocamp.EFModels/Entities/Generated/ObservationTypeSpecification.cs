using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("ObservationTypeSpecification")]
    [Index("ObservationTypeSpecificationDisplayName", Name = "AK_ObservationTypeSpecification_ObservationTypeSpecificationDisplayName", IsUnique = true)]
    [Index("ObservationTypeSpecificationName", Name = "AK_ObservationTypeSpecification_ObservationTypeSpecificationName", IsUnique = true)]
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
        [Unicode(false)]
        public string ObservationTypeSpecificationName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ObservationTypeSpecificationDisplayName { get; set; }
        public int SortOrder { get; set; }
        public int ObservationTypeCollectionMethodID { get; set; }
        public int ObservationTargetTypeID { get; set; }
        public int ObservationThresholdTypeID { get; set; }

        [ForeignKey("ObservationTargetTypeID")]
        [InverseProperty("ObservationTypeSpecifications")]
        public virtual ObservationTargetType ObservationTargetType { get; set; }
        [ForeignKey("ObservationThresholdTypeID")]
        [InverseProperty("ObservationTypeSpecifications")]
        public virtual ObservationThresholdType ObservationThresholdType { get; set; }
        [ForeignKey("ObservationTypeCollectionMethodID")]
        [InverseProperty("ObservationTypeSpecifications")]
        public virtual ObservationTypeCollectionMethod ObservationTypeCollectionMethod { get; set; }
        [InverseProperty("ObservationTypeSpecification")]
        public virtual ICollection<TreatmentBMPAssessmentObservationType> TreatmentBMPAssessmentObservationTypes { get; set; }
    }
}
