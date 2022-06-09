using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Table("WaterQualityManagementPlanParcel")]
    [Index("WaterQualityManagementPlanID", "ParcelID", Name = "AK_WaterQualityManagementPlanParcel_WaterQualityManagementPlanID_ParcelID", IsUnique = true)]
    public partial class WaterQualityManagementPlanParcel
    {
        [Key]
        public int WaterQualityManagementPlanParcelID { get; set; }
        public int WaterQualityManagementPlanID { get; set; }
        public int ParcelID { get; set; }

        [ForeignKey("ParcelID")]
        [InverseProperty("WaterQualityManagementPlanParcels")]
        public virtual Parcel Parcel { get; set; }
        [ForeignKey("WaterQualityManagementPlanID")]
        [InverseProperty("WaterQualityManagementPlanParcels")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
    }
}
