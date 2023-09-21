using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("ObservationThresholdType")]
    [Index("ObservationThresholdTypeDisplayName", Name = "AK_ObservationThresholdType_ObservationThresholdTypeDisplayName", IsUnique = true)]
    [Index("ObservationThresholdTypeName", Name = "AK_ObservationThresholdType_ObservationThresholdTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string ObservationThresholdTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ObservationThresholdTypeDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty("ObservationThresholdType")]
        public virtual ICollection<ObservationTypeSpecification> ObservationTypeSpecifications { get; set; }
    }
}
