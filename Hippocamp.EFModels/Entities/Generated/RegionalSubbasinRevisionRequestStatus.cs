using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("RegionalSubbasinRevisionRequestStatus")]
    [Index("RegionalSubbasinRevisionRequestStatusDisplayName", Name = "AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusDisplayName", IsUnique = true)]
    [Index("RegionalSubbasinRevisionRequestStatusName", Name = "AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusName", IsUnique = true)]
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
        [Unicode(false)]
        public string RegionalSubbasinRevisionRequestStatusName { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string RegionalSubbasinRevisionRequestStatusDisplayName { get; set; }

        [InverseProperty("RegionalSubbasinRevisionRequestStatus")]
        public virtual ICollection<RegionalSubbasinRevisionRequest> RegionalSubbasinRevisionRequests { get; set; }
    }
}
