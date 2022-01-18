using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("StormwaterJurisdictionPublicBMPVisibilityType")]
    [Index(nameof(StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName), Name = "AK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName", IsUnique = true)]
    [Index(nameof(StormwaterJurisdictionPublicBMPVisibilityTypeName), Name = "AK_StormwaterJurisdictionPublicBMPVisibilityType_StormwaterJurisdictionPublicBMPVisibilityTypeName", IsUnique = true)]
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
        public string StormwaterJurisdictionPublicBMPVisibilityTypeName { get; set; }
        [Required]
        [StringLength(80)]
        public string StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName { get; set; }

        [InverseProperty(nameof(StormwaterJurisdiction.StormwaterJurisdictionPublicBMPVisibilityType))]
        public virtual ICollection<StormwaterJurisdiction> StormwaterJurisdictions { get; set; }
    }
}
