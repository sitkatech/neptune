using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("SizingBasisType")]
    [Index("SizingBasisTypeDisplayName", Name = "AK_SizingBasisType_SizingBasisTypeDisplayName", IsUnique = true)]
    [Index("SizingBasisTypeName", Name = "AK_SizingBasisType_SizingBasisTypeName", IsUnique = true)]
    public partial class SizingBasisType
    {
        public SizingBasisType()
        {
            TreatmentBMPs = new HashSet<TreatmentBMP>();
        }

        [Key]
        public int SizingBasisTypeID { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string SizingBasisTypeName { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string SizingBasisTypeDisplayName { get; set; }

        [InverseProperty("SizingBasisType")]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
