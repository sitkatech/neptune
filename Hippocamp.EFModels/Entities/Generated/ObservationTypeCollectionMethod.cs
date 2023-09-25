using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("ObservationTypeCollectionMethod")]
    [Index("ObservationTypeCollectionMethodDisplayName", Name = "AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodDisplayName", IsUnique = true)]
    [Index("ObservationTypeCollectionMethodName", Name = "AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodName", IsUnique = true)]
    public partial class ObservationTypeCollectionMethod
    {
        public ObservationTypeCollectionMethod()
        {
            ObservationTypeSpecifications = new HashSet<ObservationTypeSpecification>();
        }

        [Key]
        public int ObservationTypeCollectionMethodID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ObservationTypeCollectionMethodName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string ObservationTypeCollectionMethodDisplayName { get; set; }
        public int SortOrder { get; set; }
        [Required]
        [Unicode(false)]
        public string ObservationTypeCollectionMethodDescription { get; set; }

        [InverseProperty("ObservationTypeCollectionMethod")]
        public virtual ICollection<ObservationTypeSpecification> ObservationTypeSpecifications { get; set; }
    }
}
