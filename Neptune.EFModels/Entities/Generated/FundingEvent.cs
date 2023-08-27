using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("FundingEvent")]
public partial class FundingEvent
{
    [Key]
    public int FundingEventID { get; set; }

    public int TreatmentBMPID { get; set; }

    public int FundingEventTypeID { get; set; }

    public int Year { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? Description { get; set; }

    [InverseProperty("FundingEvent")]
    public virtual ICollection<FundingEventFundingSource> FundingEventFundingSources { get; set; } = new List<FundingEventFundingSource>();

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("FundingEvents")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;

    public void DeleteFull(NeptuneDbContext dbContext)
    {
        throw new NotImplementedException();
    }
}
