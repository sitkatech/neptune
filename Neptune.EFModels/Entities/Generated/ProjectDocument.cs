using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("ProjectDocument")]
public partial class ProjectDocument
{
    [Key]
    public int ProjectDocumentID { get; set; }

    public int FileResourceID { get; set; }

    public int ProjectID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? DisplayName { get; set; }

    public DateOnly UploadDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? DocumentDescription { get; set; }

    [ForeignKey("FileResourceID")]
    [InverseProperty("ProjectDocuments")]
    public virtual FileResource FileResource { get; set; } = null!;

    [ForeignKey("ProjectID")]
    [InverseProperty("ProjectDocuments")]
    public virtual Project Project { get; set; } = null!;
}
