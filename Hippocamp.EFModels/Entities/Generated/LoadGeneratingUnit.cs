using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("LoadGeneratingUnit")]
    public partial class LoadGeneratingUnit
    {
        public LoadGeneratingUnit()
        {
            HRUCharacteristics = new HashSet<HRUCharacteristic>();
        }

        [Key]
        public int LoadGeneratingUnitID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry LoadGeneratingUnitGeometry { get; set; }
        public int? LSPCBasinID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }

        [ForeignKey(nameof(DelineationID))]
        [InverseProperty("LoadGeneratingUnits")]
        public virtual Delineation Delineation { get; set; }
        [ForeignKey(nameof(LSPCBasinID))]
        [InverseProperty("LoadGeneratingUnits")]
        public virtual LSPCBasin LSPCBasin { get; set; }
        [ForeignKey(nameof(RegionalSubbasinID))]
        [InverseProperty("LoadGeneratingUnits")]
        public virtual RegionalSubbasin RegionalSubbasin { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanID))]
        [InverseProperty("LoadGeneratingUnits")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [InverseProperty(nameof(HRUCharacteristic.LoadGeneratingUnit))]
        public virtual ICollection<HRUCharacteristic> HRUCharacteristics { get; set; }
    }
}
