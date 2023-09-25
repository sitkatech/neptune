using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vProjectLoadGeneratingResult
    {
        public int PrimaryKey { get; set; }
        public int ProjectNereidResultID { get; set; }
        public int ProjectID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string ProjectName { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        [Unicode(false)]
        public string NodeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public double? WetWeatherVolumeGenerated { get; set; }
        public double? WetWeatherTSSGenerated { get; set; }
        public double? WetWeatherTNGenerated { get; set; }
        public double? WetWeatherTPGenerated { get; set; }
        public double? WetWeatherFCGenerated { get; set; }
        public double? WetWeatherTCuGenerated { get; set; }
        public double? WetWeatherTPbGenerated { get; set; }
        public double? WetWeatherTZnGenerated { get; set; }
        public double? SummerDryWeatherVolumeGenerated { get; set; }
        public double? SummerDryWeatherTSSGenerated { get; set; }
        public double? SummerDryWeatherTNGenerated { get; set; }
        public double? SummerDryWeatherTPGenerated { get; set; }
        public double? SummerDryWeatherFCGenerated { get; set; }
        public double? SummerDryWeatherTCuGenerated { get; set; }
        public double? SummerDryWeatherTPbGenerated { get; set; }
        public double? SummerDryWeatherTZnGenerated { get; set; }
        public double? WinterDryWeatherVolumeGenerated { get; set; }
        public double? WinterDryWeatherTSSGenerated { get; set; }
        public double? WinterDryWeatherTNGenerated { get; set; }
        public double? WinterDryWeatherTPGenerated { get; set; }
        public double? WinterDryWeatherFCGenerated { get; set; }
        public double? WinterDryWeatherTCuGenerated { get; set; }
        public double? WinterDryWeatherTPbGenerated { get; set; }
        public double? WinterDryWeatherTZnGenerated { get; set; }
        public double? ImperviousAreaTreatedAcres { get; set; }
    }
}
