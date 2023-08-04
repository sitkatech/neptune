using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("NeptuneHomePageImage")]
public partial class NeptuneHomePageImage
{
    [Key]
    public int NeptuneHomePageImageID { get; set; }

    public int FileResourceID { get; set; }

    [StringLength(300)]
    [Unicode(false)]
    public string? Caption { get; set; }

    public int SortOrder { get; set; }

    [ForeignKey("FileResourceID")]
    [InverseProperty("NeptuneHomePageImages")]
    public virtual FileResource FileResource { get; set; } = null!;
}
