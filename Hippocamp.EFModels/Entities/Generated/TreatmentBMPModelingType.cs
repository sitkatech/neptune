using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("TreatmentBMPModelingType")]
    [Index("TreatmentBMPModelingTypeDisplayName", Name = "PK_TreatmentBMPModelingType_TreatmentBMPModelingTypeDisplayName", IsUnique = true)]
    [Index("TreatmentBMPModelingTypeName", Name = "PK_TreatmentBMPModelingType_TreatmentBMPModelingTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string TreatmentBMPModelingTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string TreatmentBMPModelingTypeDisplayName { get; set; }

        [InverseProperty("TreatmentBMPModelingType")]
        public virtual ICollection<TreatmentBMPType> TreatmentBMPTypes { get; set; }
    }
}
