using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Keyless]
    public partial class vProjectModelingResult
    {
        public int PrimaryKey { get; set; }
        public int ProjectNereidResultID { get; set; }
        public int ProjectID { get; set; }
        [Required]
        [StringLength(200)]
        [Unicode(false)]
        public string ProjectName { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int? TreatmentBMPID { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string TreatmentBMPName { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        [Unicode(false)]
        public string NodeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public double? WetWeatherInflow { get; set; }
        public double? WetWeatherTreated { get; set; }
        public double? WetWeatherRetained { get; set; }
        public double? WetWeatherUntreated { get; set; }
        public double? WetWeatherTSSRemoved { get; set; }
        public double? WetWeatherTNRemoved { get; set; }
        public double? WetWeatherTPRemoved { get; set; }
        public double? WetWeatherFCRemoved { get; set; }
        public double? WetWeatherTCuRemoved { get; set; }
        public double? WetWeatherTPbRemoved { get; set; }
        public double? WetWeatherTZnRemoved { get; set; }
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
        public double? SummerDryWeatherTSSRemoved { get; set; }
        public double? SummerDryWeatherTNRemoved { get; set; }
        public double? SummerDryWeatherTPRemoved { get; set; }
        public double? SummerDryWeatherFCRemoved { get; set; }
        public double? SummerDryWeatherTCuRemoved { get; set; }
        public double? SummerDryWeatherTPbRemoved { get; set; }
        public double? SummerDryWeatherTZnRemoved { get; set; }
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
        public double? WinterDryWeatherTSSRemoved { get; set; }
        public double? WinterDryWeatherTNRemoved { get; set; }
        public double? WinterDryWeatherTPRemoved { get; set; }
        public double? WinterDryWeatherFCRemoved { get; set; }
        public double? WinterDryWeatherTCuRemoved { get; set; }
        public double? WinterDryWeatherTPbRemoved { get; set; }
        public double? WinterDryWeatherTZnRemoved { get; set; }
        public double? WinterDryWeatherTSSInflow { get; set; }
        public double? WinterDryWeatherTNInflow { get; set; }
        public double? WinterDryWeatherTPInflow { get; set; }
        public double? WinterDryWeatherFCInflow { get; set; }
        public double? WinterDryWeatherTCuInflow { get; set; }
        public double? WinterDryWeatherTPbInflow { get; set; }
        public double? WinterDryWeatherTZnInflow { get; set; }
    }
}
