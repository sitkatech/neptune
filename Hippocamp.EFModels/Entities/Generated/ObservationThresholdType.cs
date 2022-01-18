using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ObservationThresholdType")]
    [Index(nameof(ObservationThresholdTypeDisplayName), Name = "AK_ObservationThresholdType_ObservationThresholdTypeDisplayName", IsUnique = true)]
    [Index(nameof(ObservationThresholdTypeName), Name = "AK_ObservationThresholdType_ObservationThresholdTypeName", IsUnique = true)]
    public partial class ObservationThresholdType
    {
        public ObservationThresholdType()
        {
            ObservationTypeSpecifications = new HashSet<ObservationTypeSpecification>();
        }

        [Key]
        public int ObservationThresholdTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string ObservationThresholdTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string ObservationThresholdTypeDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty(nameof(ObservationTypeSpecification.ObservationThresholdType))]
        public virtual ICollection<ObservationTypeSpecification> ObservationTypeSpecifications { get; set; }
    }
}
