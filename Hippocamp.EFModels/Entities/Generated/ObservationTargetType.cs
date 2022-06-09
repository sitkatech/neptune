using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("ObservationTargetType")]
    [Index("ObservationTargetTypeDisplayName", Name = "AK_ObservationTargetType_ObservationTargetTypeDisplayName", IsUnique = true)]
    [Index("ObservationTargetTypeName", Name = "AK_ObservationTargetType_ObservationTargetTypeName", IsUnique = true)]
    public partial class ObservationTargetType
    {
        public ObservationTargetType()
        {
            ObservationTypeSpecifications = new HashSet<ObservationTypeSpecification>();
        }

        [Key]
        public int ObservationTargetTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ObservationTargetTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ObservationTargetTypeDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty("ObservationTargetType")]
        public virtual ICollection<ObservationTypeSpecification> ObservationTypeSpecifications { get; set; }
    }
}
