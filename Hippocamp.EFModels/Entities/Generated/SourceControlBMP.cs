using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("SourceControlBMP")]
    public partial class SourceControlBMP
    {
        public SourceControlBMP()
        {
            WaterQualityManagementPlanVerifySourceControlBMPs = new HashSet<WaterQualityManagementPlanVerifySourceControlBMP>();
        }

        [Key]
        public int SourceControlBMPID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int SourceControlBMPAttributeID { get; set; }
        public bool? IsPresent { get; set; }
        [StringLength(200)]
        public string SourceControlBMPNote { get; set; }

        [ForeignKey(nameof(SourceControlBMPAttributeID))]
        [InverseProperty("SourceControlBMPs")]
        public virtual SourceControlBMPAttribute SourceControlBMPAttribute { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanID))]
        [InverseProperty("SourceControlBMPs")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [InverseProperty(nameof(WaterQualityManagementPlanVerifySourceControlBMP.SourceControlBMP))]
        public virtual ICollection<WaterQualityManagementPlanVerifySourceControlBMP> WaterQualityManagementPlanVerifySourceControlBMPs { get; set; }
    }
}
