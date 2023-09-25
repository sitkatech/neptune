using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("UnderlyingHydrologicSoilGroup")]
    [Index("UnderlyingHydrologicSoilGroupDisplayName", Name = "AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupDisplayName", IsUnique = true)]
    [Index("UnderlyingHydrologicSoilGroupName", Name = "AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupName", IsUnique = true)]
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
        [Unicode(false)]
        public string UnderlyingHydrologicSoilGroupName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string UnderlyingHydrologicSoilGroupDisplayName { get; set; }

        [InverseProperty("UnderlyingHydrologicSoilGroup")]
        public virtual ICollection<TreatmentBMPModelingAttribute> TreatmentBMPModelingAttributes { get; set; }
    }
}
