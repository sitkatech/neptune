using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    public partial class vProjectLoadReducingResult
    {
        public int PrimaryKey { get; set; }
        public int ProjectNereidResultID { get; set; }
        public int ProjectID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string ProjectName { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string OrganizationName { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string JurisdictionName { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int TreatmentBMPID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string TreatmentBMPName { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        [Unicode(false)]
        public string NodeID { get; set; }
        public bool? IsPartOfProject { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public double? EffectiveAreaAcres { get; set; }
        public double? DesignStormDepth85thPercentile { get; set; }
        public double? DesignVolume85thPercentile { get; set; }
        public double? WetWeatherInflow { get; set; }
        public double? WetWeatherTreated { get; set; }
        public double? WetWeatherRetained { get; set; }
        public double? WetWeatherUntreated { get; set; }
        public double? WetWeatherTSSReduced { get; set; }
        public double? WetWeatherTNReduced { get; set; }
        public double? WetWeatherTPReduced { get; set; }
        public double? WetWeatherFCReduced { get; set; }
        public double? WetWeatherTCuReduced { get; set; }
        public double? WetWeatherTPbReduced { get; set; }
        public double? WetWeatherTZnReduced { get; set; }
        public double? WetWeatherTSSInflow { get; set; }
        public double? WetWeatherTNInflow { get; set; }
        public double? WetWeatherTPInflow { get; set; }
        public double? WetWeatherFCInflow { get; set; }
        public double? WetWeatherTCuInflow { get; set; }
        public double? WetWeatherTPbInflow { get; set; }
        public double? WetWeatherTZnInflow { get; set; }
        public double? SummerDryWeatherInflow { get; set; }
        public double? SummerDryWeatherTreated { get; set; }
        public double? SummerDryWeatherRetained { get; set; }
        public double? SummerDryWeatherUntreated { get; set; }
        public double? SummerDryWeatherTSSReduced { get; set; }
        public double? SummerDryWeatherTNReduced { get; set; }
        public double? SummerDryWeatherTPReduced { get; set; }
        public double? SummerDryWeatherFCReduced { get; set; }
        public double? SummerDryWeatherTCuReduced { get; set; }
        public double? SummerDryWeatherTPbReduced { get; set; }
        public double? SummerDryWeatherTZnReduced { get; set; }
        public double? SummerDryWeatherTSSInflow { get; set; }
        public double? SummerDryWeatherTNInflow { get; set; }
        public double? SummerDryWeatherTPInflow { get; set; }
        public double? SummerDryWeatherFCInflow { get; set; }
        public double? SummerDryWeatherTCuInflow { get; set; }
        public double? SummerDryWeatherTPbInflow { get; set; }
        public double? SummerDryWeatherTZnInflow { get; set; }
        public double? WinterDryWeatherInflow { get; set; }
        public double? WinterDryWeatherTreated { get; set; }
        public double? WinterDryWeatherRetained { get; set; }
        public double? WinterDryWeatherUntreated { get; set; }
        public double? WinterDryWeatherTSSReduced { get; set; }
        public double? WinterDryWeatherTNReduced { get; set; }
        public double? WinterDryWeatherTPReduced { get; set; }
        public double? WinterDryWeatherFCReduced { get; set; }
        public double? WinterDryWeatherTCuReduced { get; set; }
        public double? WinterDryWeatherTPbReduced { get; set; }
        public double? WinterDryWeatherTZnReduced { get; set; }
        public double? WinterDryWeatherTSSInflow { get; set; }
        public double? WinterDryWeatherTNInflow { get; set; }
        public double? WinterDryWeatherTPInflow { get; set; }
        public double? WinterDryWeatherFCInflow { get; set; }
        public double? WinterDryWeatherTCuInflow { get; set; }
        public double? WinterDryWeatherTPbInflow { get; set; }
        public double? WinterDryWeatherTZnInflow { get; set; }
    }
}
