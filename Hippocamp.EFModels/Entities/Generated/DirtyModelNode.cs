using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("DirtyModelNode")]
    public partial class DirtyModelNode
    {
        [Key]
        public int DirtyModelNodeID { get; set; }
        public int? TreatmentBMPID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDate { get; set; }

        [ForeignKey("DelineationID")]
        [InverseProperty("DirtyModelNodes")]
        public virtual Delineation Delineation { get; set; }
        [ForeignKey("RegionalSubbasinID")]
        [InverseProperty("DirtyModelNodes")]
        public virtual RegionalSubbasin RegionalSubbasin { get; set; }
        [ForeignKey("TreatmentBMPID")]
        [InverseProperty("DirtyModelNodes")]
        public virtual TreatmentBMP TreatmentBMP { get; set; }
        [ForeignKey("WaterQualityManagementPlanID")]
        [InverseProperty("DirtyModelNodes")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
    }
}
