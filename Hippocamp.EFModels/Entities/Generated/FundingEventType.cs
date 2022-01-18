using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("FundingEventType")]
    [Index(nameof(FundingEventTypeName), Name = "AK_FundingEventType_FundingEventTypeName", IsUnique = true)]
    public partial class FundingEventType
    {
        public FundingEventType()
        {
            FundingEvents = new HashSet<FundingEvent>();
        }

        [Key]
        public int FundingEventTypeID { get; set; }
        [Required]
        [StringLength(100)]
        public string FundingEventTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string FundingEventTypeDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty(nameof(FundingEvent.FundingEventType))]
        public virtual ICollection<FundingEvent> FundingEvents { get; set; }
    }
}
