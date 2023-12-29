using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("FundingEventFundingSource")]
[Index("FundingSourceID", "FundingEventID", Name = "AK_FundingEventFundingSource_FundingSourceID_FundingEventID", IsUnique = true)]
public partial class FundingEventFundingSource
{
    [Key]
    public int FundingEventFundingSourceID { get; set; }

    public int FundingSourceID { get; set; }

    public int FundingEventID { get; set; }

    [Column(TypeName = "money")]
    public decimal? Amount { get; set; }

    [ForeignKey("FundingEventID")]
    [InverseProperty("FundingEventFundingSources")]
    public virtual FundingEvent FundingEvent { get; set; } = null!;

    [ForeignKey("FundingSourceID")]
    [InverseProperty("FundingEventFundingSources")]
    public virtual FundingSource FundingSource { get; set; } = null!;
}
