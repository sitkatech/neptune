using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("NereidLog")]
public partial class NereidLog
{
    [Key]
    public int NereidLogID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RequestDate { get; set; }

    [Unicode(false)]
    public string NereidRequest { get; set; } = null!;

    [Unicode(false)]
    public string? NereidResponse { get; set; }

    [InverseProperty("LastNereidLog")]
    public virtual ICollection<TreatmentBMP> TreatmentBMPs { get; set; } = new List<TreatmentBMP>();

    [InverseProperty("LastNereidLog")]
    public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; } = new List<WaterQualityManagementPlan>();
}
