using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("NereidLogTreatmentBMP")]
[Index("NereidLogID", "TreatmentBMPID", Name = "AK_NereidLogTreatmentBMP_NereidLogID_TreatmentBMPID", IsUnique = true)]
public partial class NereidLogTreatmentBMP
{
    [Key]
    public int NereidLogTreatmentBMPID { get; set; }

    public int NereidLogID { get; set; }

    public int TreatmentBMPID { get; set; }

    [ForeignKey("NereidLogID")]
    [InverseProperty("NereidLogTreatmentBMPs")]
    public virtual NereidLog NereidLog { get; set; } = null!;

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("NereidLogTreatmentBMPs")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;
}
