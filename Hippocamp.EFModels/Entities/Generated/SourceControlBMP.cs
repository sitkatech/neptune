using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
        [Unicode(false)]
        public string SourceControlBMPNote { get; set; }

        [ForeignKey("SourceControlBMPAttributeID")]
        [InverseProperty("SourceControlBMPs")]
        public virtual SourceControlBMPAttribute SourceControlBMPAttribute { get; set; }
        [ForeignKey("WaterQualityManagementPlanID")]
        [InverseProperty("SourceControlBMPs")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [InverseProperty("SourceControlBMP")]
        public virtual ICollection<WaterQualityManagementPlanVerifySourceControlBMP> WaterQualityManagementPlanVerifySourceControlBMPs { get; set; }
    }
}
