using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("SupportRequestType")]
    [Index(nameof(SupportRequestTypeDisplayName), Name = "AK_SupportRequestType_SupportRequestTypeDisplayName", IsUnique = true)]
    [Index(nameof(SupportRequestTypeName), Name = "AK_SupportRequestType_SupportRequestTypeName", IsUnique = true)]
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
        public string SupportRequestTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string SupportRequestTypeDisplayName { get; set; }
        public int SupportRequestTypeSortOrder { get; set; }

        [InverseProperty(nameof(SupportRequestLog.SupportRequestType))]
        public virtual ICollection<SupportRequestLog> SupportRequestLogs { get; set; }
    }
}
