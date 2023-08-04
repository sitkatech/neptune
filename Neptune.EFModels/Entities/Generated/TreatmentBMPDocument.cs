using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities;

[Table("TreatmentBMPDocument")]
[Index("FileResourceID", "TreatmentBMPID", Name = "AK_TreatmentBMPDocument_FileResourceID_TreatmentBMPID", IsUnique = true)]
public partial class TreatmentBMPDocument
{
    [Key]
    public int TreatmentBMPDocumentID { get; set; }

    public int FileResourceID { get; set; }

    public int TreatmentBMPID { get; set; }

    [StringLength(200)]
    [Unicode(false)]
    public string? DisplayName { get; set; }

    [Column(TypeName = "date")]
    public DateTime UploadDate { get; set; }

    [StringLength(500)]
    [Unicode(false)]
    public string? DocumentDescription { get; set; }

    [ForeignKey("FileResourceID")]
    [InverseProperty("TreatmentBMPDocuments")]
    public virtual FileResource FileResource { get; set; } = null!;

    [ForeignKey("TreatmentBMPID")]
    [InverseProperty("TreatmentBMPDocuments")]
    public virtual TreatmentBMP TreatmentBMP { get; set; } = null!;
}
