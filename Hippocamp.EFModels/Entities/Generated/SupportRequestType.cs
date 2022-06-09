using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("SupportRequestType")]
    [Index("SupportRequestTypeDisplayName", Name = "AK_SupportRequestType_SupportRequestTypeDisplayName", IsUnique = true)]
    [Index("SupportRequestTypeName", Name = "AK_SupportRequestType_SupportRequestTypeName", IsUnique = true)]
    public partial class SupportRequestType
    {
        public SupportRequestType()
        {
            SupportRequestLogs = new HashSet<SupportRequestLog>();
        }

        [Key]
        public int SupportRequestTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string SupportRequestTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string SupportRequestTypeDisplayName { get; set; }
        public int SupportRequestTypeSortOrder { get; set; }

        [InverseProperty("SupportRequestType")]
        public virtual ICollection<SupportRequestLog> SupportRequestLogs { get; set; }
    }
}
