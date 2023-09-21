using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanPhoto")]
    public partial class WaterQualityManagementPlanPhoto
    {
        public WaterQualityManagementPlanPhoto()
        {
            WaterQualityManagementPlanVerifyPhotos = new HashSet<WaterQualityManagementPlanVerifyPhoto>();
        }

        [Key]
        public int WaterQualityManagementPlanPhotoID { get; set; }
        public int FileResourceID { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string Caption { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UploadDate { get; set; }

        [ForeignKey("FileResourceID")]
        [InverseProperty("WaterQualityManagementPlanPhotos")]
        public virtual FileResource FileResource { get; set; }
        [InverseProperty("WaterQualityManagementPlanPhoto")]
        public virtual ICollection<WaterQualityManagementPlanVerifyPhoto> WaterQualityManagementPlanVerifyPhotos { get; set; }
    }
}
