using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanVerifyPhoto")]
    public partial class WaterQualityManagementPlanVerifyPhoto
    {
        [Key]
        public int WaterQualityManagementPlanVerifyPhotoID { get; set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int WaterQualityManagementPlanPhotoID { get; set; }

        [ForeignKey(nameof(WaterQualityManagementPlanPhotoID))]
        [InverseProperty("WaterQualityManagementPlanVerifyPhotos")]
        public virtual WaterQualityManagementPlanPhoto WaterQualityManagementPlanPhoto { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanVerifyID))]
        [InverseProperty("WaterQualityManagementPlanVerifyPhotos")]
        public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
    }
}
