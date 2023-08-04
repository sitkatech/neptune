using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("SourceControlBMPAttribute")]
public partial class SourceControlBMPAttribute
{
    [Key]
    public int SourceControlBMPAttributeID { get; set; }

    public int SourceControlBMPAttributeCategoryID { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SourceControlBMPAttributeName { get; set; }

    [ForeignKey("SourceControlBMPAttributeCategoryID")]
    [InverseProperty("SourceControlBMPAttributes")]
    public virtual SourceControlBMPAttributeCategory SourceControlBMPAttributeCategory { get; set; } = null!;

    [InverseProperty("SourceControlBMPAttribute")]
    public virtual ICollection<SourceControlBMP> SourceControlBMPs { get; set; } = new List<SourceControlBMP>();
}
