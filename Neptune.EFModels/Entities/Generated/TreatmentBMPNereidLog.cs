using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("TreatmentBMPNereidLog")]
[Index("TreatmentBMPID", Name = "AK_TreatmentBMPNereidLog_TreatmentBMPID", IsUnique = true)]
public partial class TreatmentBMPNereidLog
{
    [Key]
    public int TreatmentBMPNereidLogID { get; set; }

    public int TreatmentBMPID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastRequestDate { get; set; }

    [Unicode(false)]
    public string? NereidRequest { get; set; }

    [Unicode(false)]
    public string? NereidResponse { get; set; }

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("TreatmentBMPNereidLog")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;
}
