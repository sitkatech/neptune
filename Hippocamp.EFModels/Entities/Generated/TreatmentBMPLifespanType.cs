using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPLifespanType")]
    [Index(nameof(TreatmentBMPLifespanTypeDisplayName), Name = "AK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeDisplayName", IsUnique = true)]
    [Index(nameof(TreatmentBMPLifespanTypeName), Name = "AK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeName", IsUnique = true)]
    public partial class TreatmentBMPLifespanType
    {
        public TreatmentBMPLifespanType()
        {
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int TreatmentBMPLifespanTypeID { get; set; }
        [Required]
        [StringLength(50)]
        public string TreatmentBMPLifespanTypeName { get; set; }
        [Required]
        [StringLength(50)]
        public string TreatmentBMPLifespanTypeDisplayName { get; set; }

        [InverseProperty(nameof(TreatmentBMP.TreatmentBMPLifespanType))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
