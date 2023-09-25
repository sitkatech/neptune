using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Neptune.EFModels.Entities
{
    [Table("PlannedProjectLoadGeneratingUnit")]
    public partial class PlannedProjectLoadGeneratingUnit
    {
        public PlannedProjectLoadGeneratingUnit()
        {
            PlannedProjectHRUCharacteristics = new HashSet<PlannedProjectHRUCharacteristic>();
        }

        [Key]
        public int PlannedProjectLoadGeneratingUnitID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry PlannedProjectLoadGeneratingUnitGeometry { get; set; }
        public int ProjectID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }

        [ForeignKey(nameof(DelineationID))]
        [InverseProperty("PlannedProjectLoadGeneratingUnits")]
        public virtual Delineation Delineation { get; set; }
        [ForeignKey(nameof(ModelBasinID))]
        [InverseProperty("PlannedProjectLoadGeneratingUnits")]
        public virtual ModelBasin ModelBasin { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty("PlannedProjectLoadGeneratingUnits")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(RegionalSubbasinID))]
        [InverseProperty("PlannedProjectLoadGeneratingUnits")]
        public virtual RegionalSubbasin RegionalSubbasin { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanID))]
        [InverseProperty("PlannedProjectLoadGeneratingUnits")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [InverseProperty(nameof(PlannedProjectHRUCharacteristic.PlannedProjectLoadGeneratingUnit))]
        public virtual ICollection<PlannedProjectHRUCharacteristic> PlannedProjectHRUCharacteristics { get; set; }
    }
}
