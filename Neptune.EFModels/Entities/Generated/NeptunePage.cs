using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("NeptunePage")]
public partial class NeptunePage
{
    [Key]
    public int NeptunePageID { get; set; }

    public int NeptunePageTypeID { get; set; }

    [Unicode(false)]
    public string? NeptunePageContent { get; set; }

    [InverseProperty("NeptunePage")]
    public virtual ICollection<NeptunePageImage> NeptunePageImages { get; set; } = new List<NeptunePageImage>();
}
