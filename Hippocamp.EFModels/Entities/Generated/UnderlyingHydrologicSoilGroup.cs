using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("UnderlyingHydrologicSoilGroup")]
    [Index(nameof(UnderlyingHydrologicSoilGroupDisplayName), Name = "AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupDisplayName", IsUnique = true)]
    [Index(nameof(UnderlyingHydrologicSoilGroupName), Name = "AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupName", IsUnique = true)]
    public partial class UnderlyingHydrologicSoilGroup
    {
        public UnderlyingHydrologicSoilGroup()
        {
            TreatmentBMPModelingAttributes = new HashSet<TreatmentBMPModelingAttribute>();
        }

        [Key]
        public int UnderlyingHydrologicSoilGroupID { get; set; }
        [Required]
        [StringLength(100)]
        public string UnderlyingHydrologicSoilGroupName { get; set; }
        [Required]
        [StringLength(100)]
        public string UnderlyingHydrologicSoilGroupDisplayName { get; set; }

        [InverseProperty(nameof(TreatmentBMPModelingAttribute.UnderlyingHydrologicSoilGroup))]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
