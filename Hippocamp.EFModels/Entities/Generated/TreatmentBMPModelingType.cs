using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPModelingType")]
    [Index(nameof(TreatmentBMPModelingTypeDisplayName), Name = "PK_TreatmentBMPModelingType_TreatmentBMPModelingTypeDisplayName", IsUnique = true)]
    [Index(nameof(TreatmentBMPModelingTypeName), Name = "PK_TreatmentBMPModelingType_TreatmentBMPModelingTypeName", IsUnique = true)]
    public partial class TreatmentBMPModelingType
    {
        public TreatmentBMPModelingType()
        {
            TreatmentBMPTypes = new HashSet<TreatmentBMPType>();
        }

        [Key]
        public int TreatmentBMPModelingTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string TreatmentBMPModelingTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string TreatmentBMPModelingTypeDisplayName { get; set; }

        [InverseProperty(nameof(TreatmentBMPType.TreatmentBMPModelingType))]
        public virtual ICollection<TreatmentBMPType> TreatmentBMPTypes { get; set; }
    }
}
