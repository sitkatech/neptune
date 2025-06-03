using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("HRULog")]
public partial class HRULog
{
    [Key]
    public int HRULogID { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime RequestDate { get; set; }

    public bool Success { get; set; }

    [Unicode(false)]
    public string HRURequest { get; set; } = null!;

    [Unicode(false)]
    public string? HRUResponse { get; set; }

    [InverseProperty("HRULog")]
    public virtual ICollection<LoadGeneratingUnit4326> LoadGeneratingUnit4326s { get; set; } = new List<LoadGeneratingUnit4326>();

    [InverseProperty("HRULog")]
    public virtual ICollection<LoadGeneratingUnit> LoadGeneratingUnits { get; set; } = new List<LoadGeneratingUnit>();
}
