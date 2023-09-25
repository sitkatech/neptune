using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("HydromodificationAppliesType")]
    [Index("HydromodificationAppliesTypeDisplayName", Name = "AK_HydromodificationAppliesType_HydromodificationAppliesTypeDisplayName", IsUnique = true)]
    [Index("HydromodificationAppliesTypeName", Name = "AK_HydromodificationAppliesType_HydromodificationAppliesTypeName", IsUnique = true)]
    public partial class HydromodificationAppliesType
    {
        public HydromodificationAppliesType()
        {
            WaterQualityManagementPlans = new HashSet<WaterQualityManagementPlan>();
        }

        [Key]
        public int HydromodificationAppliesTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string HydromodificationAppliesTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string HydromodificationAppliesTypeDisplayName { get; set; }
        public int SortOrder { get; set; }

        [InverseProperty("HydromodificationAppliesType")]
        public virtual ICollection<WaterQualityManagementPlan> WaterQualityManagementPlans { get; set; }
    }
}
