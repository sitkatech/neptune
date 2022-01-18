using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("RegionalSubbasinRevisionRequestStatus")]
    [Index(nameof(RegionalSubbasinRevisionRequestStatusDisplayName), Name = "AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusDisplayName", IsUnique = true)]
    [Index(nameof(RegionalSubbasinRevisionRequestStatusName), Name = "AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusName", IsUnique = true)]
    public partial class RegionalSubbasinRevisionRequestStatus
    {
        public RegionalSubbasinRevisionRequestStatus()
        {
            RegionalSubbasinRevisionRequests = new HashSet<RegionalSubbasinRevisionRequest>();
        }

        [Key]
        public int RegionalSubbasinRevisionRequestStatusID { get; set; }
        [Required]
        [StringLength(20)]
        public string RegionalSubbasinRevisionRequestStatusName { get; set; }
        [Required]
        [StringLength(20)]
        public string RegionalSubbasinRevisionRequestStatusDisplayName { get; set; }

        [InverseProperty(nameof(RegionalSubbasinRevisionRequest.RegionalSubbasinRevisionRequestStatus))]
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequests { get; set; }
    }
}
