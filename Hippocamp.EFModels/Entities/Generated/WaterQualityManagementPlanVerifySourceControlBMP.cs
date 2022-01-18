using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanVerifySourceControlBMP")]
    public partial class WaterQualityManagementPlanVerifySourceControlBMP
    {
        [Key]
        public int WaterQualityManagementPlanVerifySourceControlBMPID { get; set; }
        public int WaterQualityManagementPlanVerifyID { get; set; }
        public int SourceControlBMPID { get; set; }
        [StringLength(1000)]
        public string WaterQualityManagementPlanSourceControlCondition { get; set; }

        [ForeignKey(nameof(SourceControlBMPID))]
        [InverseProperty("WaterQualityManagementPlanVerifySourceControlBMPs")]
        public virtual SourceControlBMP SourceControlBMP { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanVerifyID))]
        [InverseProperty("WaterQualityManagementPlanVerifySourceControlBMPs")]
        public virtual WaterQualityManagementPlanVerify WaterQualityManagementPlanVerify { get; set; }
    }
}
