using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("SourceControlBMPAttributeCategory")]
[Index("SourceControlBMPAttributeCategoryName", Name = "PK_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryName", IsUnique = true)]
[Index("SourceControlBMPAttributeCategoryShortName", Name = "PK_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryShortName", IsUnique = true)]
public partial class SourceControlBMPAttributeCategory
{
    [Key]
    public int SourceControlBMPAttributeCategoryID { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? SourceControlBMPAttributeCategoryShortName { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? SourceControlBMPAttributeCategoryName { get; set; }

    [InverseProperty("SourceControlBMPAttributeCategory")]
    public virtual ICollection<SourceControlBMPAttribute> SourceControlBMPAttributes { get; set; } = new List<SourceControlBMPAttribute>();
}
