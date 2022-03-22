using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("ProjectLoadGeneratingUnit")]
    public partial class ProjectLoadGeneratingUnit
    {
        public ProjectLoadGeneratingUnit()
        {
            ProjectHRUCharacteristics = new HashSet<ProjectHRUCharacteristic>();
        }

        [Key]
        public int ProjectLoadGeneratingUnitID { get; set; }
        [Required]
        [Column(TypeName = "geometry")]
        public Geometry ProjectLoadGeneratingUnitGeometry { get; set; }
        public int ProjectID { get; set; }
        public int? ModelBasinID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public bool? IsEmptyResponseFromHRUService { get; set; }

        [ForeignKey(nameof(DelineationID))]
        [InverseProperty("ProjectLoadGeneratingUnits")]
        public virtual Delineation Delineation { get; set; }
        [ForeignKey(nameof(ModelBasinID))]
        [InverseProperty("ProjectLoadGeneratingUnits")]
        public virtual ModelBasin ModelBasin { get; set; }
        [ForeignKey(nameof(ProjectID))]
        [InverseProperty("ProjectLoadGeneratingUnits")]
        public virtual Project Project { get; set; }
        [ForeignKey(nameof(RegionalSubbasinID))]
        [InverseProperty("ProjectLoadGeneratingUnits")]
        public virtual RegionalSubbasin RegionalSubbasin { get; set; }
        [ForeignKey(nameof(WaterQualityManagementPlanID))]
        [InverseProperty("ProjectLoadGeneratingUnits")]
        public virtual WaterQualityManagementPlan WaterQualityManagementPlan { get; set; }
        [InverseProperty(nameof(ProjectHRUCharacteristic.ProjectLoadGeneratingUnit))]
        public virtual ICollection<ProjectHRUCharacteristic> ProjectHRUCharacteristics { get; set; }
    }
}
