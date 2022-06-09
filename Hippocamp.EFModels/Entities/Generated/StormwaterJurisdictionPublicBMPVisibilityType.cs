using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("StormwaterJurisdictionPublicBMPVisibilityType")]
    [Index("StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName", Name = "AK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName", IsUnique = true)]
    [Index("StormwaterJurisdictionPublicBMPVisibilityTypeName", Name = "AK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeName", IsUnique = true)]
    public partial class StormwaterJurisdictionPublicBMPVisibilityType
    {
        public StormwaterJurisdictionPublicBMPVisibilityType()
        {
            StormwaterJurisdictions = new HashSet<StormwaterJurisdiction>();
        }

        [Key]
        public int StormwaterJurisdictionPublicBMPVisibilityTypeID { get; set; }
        [Required]
        [StringLength(80)]
        [Unicode(false)]
        public string StormwaterJurisdictionPublicBMPVisibilityTypeName { get; set; }
        [Required]
        [StringLength(80)]
        [Unicode(false)]
        public string StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName { get; set; }

        [InverseProperty("StormwaterJurisdictionPublicBMPVisibilityType")]
        public virtual ICollection<StormwaterJurisdiction> StormwaterJurisdictions { get; set; }
    }
}
