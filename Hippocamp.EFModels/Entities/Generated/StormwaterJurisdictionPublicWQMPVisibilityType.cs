using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("StormwaterJurisdictionPublicWQMPVisibilityType")]
    [Index("StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName", Name = "AK_StormwaterJurisdictionPublicWQMPVisibilityType_StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName", IsUnique = true)]
    [Index("StormwaterJurisdictionPublicWQMPVisibilityTypeName", Name = "AK_StormwaterJurisdictionPublicWQMPVisibilityType_StormwaterJurisdictionPublicWQMPVisibilityTypeName", IsUnique = true)]
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
        [Unicode(false)]
        public string StormwaterJurisdictionPublicWQMPVisibilityTypeName { get; set; }
        [Required]
        [StringLength(80)]
        [Unicode(false)]
        public string StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName { get; set; }

        [InverseProperty("StormwaterJurisdictionPublicWQMPVisibilityType")]
        public virtual ICollection<StormwaterJurisdiction> StormwaterJurisdictions { get; set; }
    }
}
