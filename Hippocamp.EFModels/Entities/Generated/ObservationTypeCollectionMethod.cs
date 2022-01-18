using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ObservationTypeCollectionMethod")]
    [Index(nameof(ObservationTypeCollectionMethodDisplayName), Name = "AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodDisplayName", IsUnique = true)]
    [Index(nameof(ObservationTypeCollectionMethodName), Name = "AK_ObservationTypeCollectionMethod_ObservationTypeCollectionMethodName", IsUnique = true)]
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
        public string ObservationTypeCollectionMethodName { get; set; }
        [Required]
        [StringLength(100)]
        public string ObservationTypeCollectionMethodDisplayName { get; set; }
        public int SortOrder { get; set; }
        [Required]
        public string ObservationTypeCollectionMethodDescription { get; set; }

        [InverseProperty(nameof(ObservationTypeSpecification.ObservationTypeCollectionMethod))]
        public virtual ICollection<ObservationTypeSpecification> ObservationTypeSpecifications { get; set; }
    }
}
