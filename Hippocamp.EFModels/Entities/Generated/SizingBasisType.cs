using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("SizingBasisType")]
    [Index(nameof(SizingBasisTypeDisplayName), Name = "AK_SizingBasisType_SizingBasisTypeDisplayName", IsUnique = true)]
    [Index(nameof(SizingBasisTypeName), Name = "AK_SizingBasisType_SizingBasisTypeName", IsUnique = true)]
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
        public string SizingBasisTypeName { get; set; }
        [Required]
        [StringLength(50)]
        public string SizingBasisTypeDisplayName { get; set; }

        [InverseProperty(nameof(TreatmentBMP.SizingBasisType))]
        public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; }
    }
}
