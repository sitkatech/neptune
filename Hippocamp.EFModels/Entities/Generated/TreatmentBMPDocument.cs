using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPDocument")]
    [Index(nameof(FileResourceID), nameof(TreatmentBMPID), Name = "AK_TreatmentBMPDocument_FileResourceID_TreatmentBMPID", IsUnique = true)]
    public partial class TreatmentBMPDocument
    {
        [Key]
        public int TreatmentBMPDocumentID { get; set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        [Required]
        [StringLength(200)]
        public string DisplayName { get; set; }
        [Column(TypeName = "date")]
        public DateTime UploadDate { get; set; }
        [StringLength(500)]
        public string DocumentDescription { get; set; }

        [ForeignKey(nameof(FileResourceID))]
        [InverseProperty("TreatmentBMPDocuments")]
        public virtual FileResource FileResource { get; set; }
        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("TreatmentBMPDocuments")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
    }
}
