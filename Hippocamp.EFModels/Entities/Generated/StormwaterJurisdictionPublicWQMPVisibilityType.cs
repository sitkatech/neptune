using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("StormwaterJurisdictionPublicWQMPVisibilityType")]
    [Index(nameof(StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName), Name = "AK_StormwaterJurisdictionPublicWQMPVisibilityType_StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName", IsUnique = true)]
    [Index(nameof(StormwaterJurisdictionPublicWQMPVisibilityTypeName), Name = "AK_StormwaterJurisdictionPublicWQMPVisibilityType_StormwaterJurisdictionPublicWQMPVisibilityTypeName", IsUnique = true)]
    public partial class StormwaterJurisdictionPublicWQMPVisibilityType
    {
        public StormwaterJurisdictionPublicWQMPVisibilityType()
        {
            StormwaterJurisdictions = new HashSet<StormwaterJurisdiction>();
        }

        [Key]
        public int StormwaterJurisdictionPublicWQMPVisibilityTypeID { get; set; }
        [Required]
        [StringLength(80)]
        public string StormwaterJurisdictionPublicWQMPVisibilityTypeName { get; set; }
        [Required]
        [StringLength(80)]
        public string StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName { get; set; }

        [InverseProperty(nameof(StormwaterJurisdiction.StormwaterJurisdictionPublicWQMPVisibilityType))]
        public virtual ICollection<StormwaterJurisdiction> StormwaterJurisdictions { get; set; }
    }
}
