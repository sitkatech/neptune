using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("NeptunePageImage")]
[Index("NeptunePageID", "FileResourceID", Name = "AK_NeptunePageImage_NeptunePageID_FileResourceID", IsUnique = true)]
public partial class NeptunePageImage
{
    [Key]
    public int NeptunePageImageID { get; set; }

    public int NeptunePageID { get; set; }

    public int FileResourceID { get; set; }

    [ForeignKey("FileResourceID")]
    [InverseProperty("NeptunePageImages")]
    public virtual FileResource FileResource { get; set; } = null!;

    [ForeignKey("NeptunePageID")]
    [InverseProperty("NeptunePageImages")]
    public virtual NeptunePage NeptunePage { get; set; } = null!;
}
