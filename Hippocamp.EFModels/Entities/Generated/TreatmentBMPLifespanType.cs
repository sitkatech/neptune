using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("TreatmentBMPLifespanType")]
    [Index("TreatmentBMPLifespanTypeDisplayName", Name = "AK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeDisplayName", IsUnique = true)]
    [Index("TreatmentBMPLifespanTypeName", Name = "AK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string TreatmentBMPLifespanTypeName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string TreatmentBMPLifespanTypeDisplayName { get; set; }

        [InverseProperty("TreatmentBMPLifespanType")]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
