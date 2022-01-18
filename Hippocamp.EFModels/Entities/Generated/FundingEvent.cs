using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("FundingEvent")]
    public partial class FundingEvent
    {
        public FundingEvent()
        {
            FundingEventFundingSources = new HashSet<FundingEventFundingSource>();
        }

        [Key]
        public int FundingEventID { get; set; }
        public int TreatmentBMPID { get; set; }
        public int FundingEventTypeID { get; set; }
        public int Year { get; set; }
        [StringLength(500)]
        public string Description { get; set; }

        [ForeignKey(nameof(FundingEventTypeID))]
        [InverseProperty("FundingEvents")]
        public virtual FundingEventType FundingEventType { get; set; }
        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("FundingEvents")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        [InverseProperty(nameof(FundingEventFundingSource.FundingEvent))]
        public virtual ICollection<FundingEventFundingSource> FundingEventFundingSources { get; set; }
    }
}
