using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("TreatmentBMPImage")]
    [Index(nameof(FileResourceID), nameof(TreatmentBMPID), Name = "AK_TreatmentBMPImage_FileResourceID_TreatmentBMPID", IsUnique = true)]
    public partial class TreatmentBMPImage
    {
        [Key]
        public int TreatmentBMPImageID { get; set; }
        public int FileResourceID { get; set; }
        public int TreatmentBMPID { get; set; }
        [StringLength(500)]
        public string Caption { get; set; }
        [Column(TypeName = "date")]
        public DateTime UploadDate { get; set; }

        [ForeignKey(nameof(FileResourceID))]
        [InverseProperty("TreatmentBMPImages")]
        public virtual FileResource FileResource { get; set; }
        [ForeignKey(nameof(TreatmentBMPID))]
        [InverseProperty("TreatmentBMPImages")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
    }
}
