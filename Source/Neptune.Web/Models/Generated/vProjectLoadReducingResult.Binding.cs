//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vProjectLoadReducingResult]
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using LtInfo.Common.DesignByContract;
using LtInfo.Common.Models;
using Neptune.Web.Common;

namespace Neptune.Web.Models
{
    public partial class vProjectLoadReducingResult
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vProjectLoadReducingResult()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vProjectLoadReducingResult(int primaryKey, int projectNereidResultID, int projectID, string projectName, bool isBaselineCondition, int treatmentBMPID, string treatmentBMPName, int? waterQualityManagementPlanID, int? regionalSubbasinID, int? delineationID, string nodeID, DateTime? lastUpdate, double? effectiveAreaAcres, double? designStormDepth85thPercentile, double? designVolume85thPercentile, double? wetWeatherInflow, double? wetWeatherTreated, double? wetWeatherRetained, double? wetWeatherUntreated, double? wetWeatherTSSReduced, double? wetWeatherTNReduced, double? wetWeatherTPReduced, double? wetWeatherFCReduced, double? wetWeatherTCuReduced, double? wetWeatherTPbReduced, double? wetWeatherTZnReduced, double? wetWeatherTSSInflow, double? wetWeatherTNInflow, double? wetWeatherTPInflow, double? wetWeatherFCInflow, double? wetWeatherTCuInflow, double? wetWeatherTPbInflow, double? wetWeatherTZnInflow, double? summerDryWeatherInflow, double? summerDryWeatherTreated, double? summerDryWeatherRetained, double? summerDryWeatherUntreated, double? summerDryWeatherTSSReduced, double? summerDryWeatherTNReduced, double? summerDryWeatherTPReduced, double? summerDryWeatherFCReduced, double? summerDryWeatherTCuReduced, double? summerDryWeatherTPbReduced, double? summerDryWeatherTZnReduced, double? summerDryWeatherTSSInflow, double? summerDryWeatherTNInflow, double? summerDryWeatherTPInflow, double? summerDryWeatherFCInflow, double? summerDryWeatherTCuInflow, double? summerDryWeatherTPbInflow, double? summerDryWeatherTZnInflow, double? winterDryWeatherInflow, double? winterDryWeatherTreated, double? winterDryWeatherRetained, double? winterDryWeatherUntreated, double? winterDryWeatherTSSReduced, double? winterDryWeatherTNReduced, double? winterDryWeatherTPReduced, double? winterDryWeatherFCReduced, double? winterDryWeatherTCuReduced, double? winterDryWeatherTPbReduced, double? winterDryWeatherTZnReduced, double? winterDryWeatherTSSInflow, double? winterDryWeatherTNInflow, double? winterDryWeatherTPInflow, double? winterDryWeatherFCInflow, double? winterDryWeatherTCuInflow, double? winterDryWeatherTPbInflow, double? winterDryWeatherTZnInflow) : this()
        {
            this.PrimaryKey = primaryKey;
            this.ProjectNereidResultID = projectNereidResultID;
            this.ProjectID = projectID;
            this.ProjectName = projectName;
            this.IsBaselineCondition = isBaselineCondition;
            this.TreatmentBMPID = treatmentBMPID;
            this.TreatmentBMPName = treatmentBMPName;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.DelineationID = delineationID;
            this.NodeID = nodeID;
            this.LastUpdate = lastUpdate;
            this.EffectiveAreaAcres = effectiveAreaAcres;
            this.DesignStormDepth85thPercentile = designStormDepth85thPercentile;
            this.DesignVolume85thPercentile = designVolume85thPercentile;
            this.WetWeatherInflow = wetWeatherInflow;
            this.WetWeatherTreated = wetWeatherTreated;
            this.WetWeatherRetained = wetWeatherRetained;
            this.WetWeatherUntreated = wetWeatherUntreated;
            this.WetWeatherTSSReduced = wetWeatherTSSReduced;
            this.WetWeatherTNReduced = wetWeatherTNReduced;
            this.WetWeatherTPReduced = wetWeatherTPReduced;
            this.WetWeatherFCReduced = wetWeatherFCReduced;
            this.WetWeatherTCuReduced = wetWeatherTCuReduced;
            this.WetWeatherTPbReduced = wetWeatherTPbReduced;
            this.WetWeatherTZnReduced = wetWeatherTZnReduced;
            this.WetWeatherTSSInflow = wetWeatherTSSInflow;
            this.WetWeatherTNInflow = wetWeatherTNInflow;
            this.WetWeatherTPInflow = wetWeatherTPInflow;
            this.WetWeatherFCInflow = wetWeatherFCInflow;
            this.WetWeatherTCuInflow = wetWeatherTCuInflow;
            this.WetWeatherTPbInflow = wetWeatherTPbInflow;
            this.WetWeatherTZnInflow = wetWeatherTZnInflow;
            this.SummerDryWeatherInflow = summerDryWeatherInflow;
            this.SummerDryWeatherTreated = summerDryWeatherTreated;
            this.SummerDryWeatherRetained = summerDryWeatherRetained;
            this.SummerDryWeatherUntreated = summerDryWeatherUntreated;
            this.SummerDryWeatherTSSReduced = summerDryWeatherTSSReduced;
            this.SummerDryWeatherTNReduced = summerDryWeatherTNReduced;
            this.SummerDryWeatherTPReduced = summerDryWeatherTPReduced;
            this.SummerDryWeatherFCReduced = summerDryWeatherFCReduced;
            this.SummerDryWeatherTCuReduced = summerDryWeatherTCuReduced;
            this.SummerDryWeatherTPbReduced = summerDryWeatherTPbReduced;
            this.SummerDryWeatherTZnReduced = summerDryWeatherTZnReduced;
            this.SummerDryWeatherTSSInflow = summerDryWeatherTSSInflow;
            this.SummerDryWeatherTNInflow = summerDryWeatherTNInflow;
            this.SummerDryWeatherTPInflow = summerDryWeatherTPInflow;
            this.SummerDryWeatherFCInflow = summerDryWeatherFCInflow;
            this.SummerDryWeatherTCuInflow = summerDryWeatherTCuInflow;
            this.SummerDryWeatherTPbInflow = summerDryWeatherTPbInflow;
            this.SummerDryWeatherTZnInflow = summerDryWeatherTZnInflow;
            this.WinterDryWeatherInflow = winterDryWeatherInflow;
            this.WinterDryWeatherTreated = winterDryWeatherTreated;
            this.WinterDryWeatherRetained = winterDryWeatherRetained;
            this.WinterDryWeatherUntreated = winterDryWeatherUntreated;
            this.WinterDryWeatherTSSReduced = winterDryWeatherTSSReduced;
            this.WinterDryWeatherTNReduced = winterDryWeatherTNReduced;
            this.WinterDryWeatherTPReduced = winterDryWeatherTPReduced;
            this.WinterDryWeatherFCReduced = winterDryWeatherFCReduced;
            this.WinterDryWeatherTCuReduced = winterDryWeatherTCuReduced;
            this.WinterDryWeatherTPbReduced = winterDryWeatherTPbReduced;
            this.WinterDryWeatherTZnReduced = winterDryWeatherTZnReduced;
            this.WinterDryWeatherTSSInflow = winterDryWeatherTSSInflow;
            this.WinterDryWeatherTNInflow = winterDryWeatherTNInflow;
            this.WinterDryWeatherTPInflow = winterDryWeatherTPInflow;
            this.WinterDryWeatherFCInflow = winterDryWeatherFCInflow;
            this.WinterDryWeatherTCuInflow = winterDryWeatherTCuInflow;
            this.WinterDryWeatherTPbInflow = winterDryWeatherTPbInflow;
            this.WinterDryWeatherTZnInflow = winterDryWeatherTZnInflow;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vProjectLoadReducingResult(vProjectLoadReducingResult vProjectLoadReducingResult) : this()
        {
            this.PrimaryKey = vProjectLoadReducingResult.PrimaryKey;
            this.ProjectNereidResultID = vProjectLoadReducingResult.ProjectNereidResultID;
            this.ProjectID = vProjectLoadReducingResult.ProjectID;
            this.ProjectName = vProjectLoadReducingResult.ProjectName;
            this.IsBaselineCondition = vProjectLoadReducingResult.IsBaselineCondition;
            this.TreatmentBMPID = vProjectLoadReducingResult.TreatmentBMPID;
            this.TreatmentBMPName = vProjectLoadReducingResult.TreatmentBMPName;
            this.WaterQualityManagementPlanID = vProjectLoadReducingResult.WaterQualityManagementPlanID;
            this.RegionalSubbasinID = vProjectLoadReducingResult.RegionalSubbasinID;
            this.DelineationID = vProjectLoadReducingResult.DelineationID;
            this.NodeID = vProjectLoadReducingResult.NodeID;
            this.LastUpdate = vProjectLoadReducingResult.LastUpdate;
            this.EffectiveAreaAcres = vProjectLoadReducingResult.EffectiveAreaAcres;
            this.DesignStormDepth85thPercentile = vProjectLoadReducingResult.DesignStormDepth85thPercentile;
            this.DesignVolume85thPercentile = vProjectLoadReducingResult.DesignVolume85thPercentile;
            this.WetWeatherInflow = vProjectLoadReducingResult.WetWeatherInflow;
            this.WetWeatherTreated = vProjectLoadReducingResult.WetWeatherTreated;
            this.WetWeatherRetained = vProjectLoadReducingResult.WetWeatherRetained;
            this.WetWeatherUntreated = vProjectLoadReducingResult.WetWeatherUntreated;
            this.WetWeatherTSSReduced = vProjectLoadReducingResult.WetWeatherTSSReduced;
            this.WetWeatherTNReduced = vProjectLoadReducingResult.WetWeatherTNReduced;
            this.WetWeatherTPReduced = vProjectLoadReducingResult.WetWeatherTPReduced;
            this.WetWeatherFCReduced = vProjectLoadReducingResult.WetWeatherFCReduced;
            this.WetWeatherTCuReduced = vProjectLoadReducingResult.WetWeatherTCuReduced;
            this.WetWeatherTPbReduced = vProjectLoadReducingResult.WetWeatherTPbReduced;
            this.WetWeatherTZnReduced = vProjectLoadReducingResult.WetWeatherTZnReduced;
            this.WetWeatherTSSInflow = vProjectLoadReducingResult.WetWeatherTSSInflow;
            this.WetWeatherTNInflow = vProjectLoadReducingResult.WetWeatherTNInflow;
            this.WetWeatherTPInflow = vProjectLoadReducingResult.WetWeatherTPInflow;
            this.WetWeatherFCInflow = vProjectLoadReducingResult.WetWeatherFCInflow;
            this.WetWeatherTCuInflow = vProjectLoadReducingResult.WetWeatherTCuInflow;
            this.WetWeatherTPbInflow = vProjectLoadReducingResult.WetWeatherTPbInflow;
            this.WetWeatherTZnInflow = vProjectLoadReducingResult.WetWeatherTZnInflow;
            this.SummerDryWeatherInflow = vProjectLoadReducingResult.SummerDryWeatherInflow;
            this.SummerDryWeatherTreated = vProjectLoadReducingResult.SummerDryWeatherTreated;
            this.SummerDryWeatherRetained = vProjectLoadReducingResult.SummerDryWeatherRetained;
            this.SummerDryWeatherUntreated = vProjectLoadReducingResult.SummerDryWeatherUntreated;
            this.SummerDryWeatherTSSReduced = vProjectLoadReducingResult.SummerDryWeatherTSSReduced;
            this.SummerDryWeatherTNReduced = vProjectLoadReducingResult.SummerDryWeatherTNReduced;
            this.SummerDryWeatherTPReduced = vProjectLoadReducingResult.SummerDryWeatherTPReduced;
            this.SummerDryWeatherFCReduced = vProjectLoadReducingResult.SummerDryWeatherFCReduced;
            this.SummerDryWeatherTCuReduced = vProjectLoadReducingResult.SummerDryWeatherTCuReduced;
            this.SummerDryWeatherTPbReduced = vProjectLoadReducingResult.SummerDryWeatherTPbReduced;
            this.SummerDryWeatherTZnReduced = vProjectLoadReducingResult.SummerDryWeatherTZnReduced;
            this.SummerDryWeatherTSSInflow = vProjectLoadReducingResult.SummerDryWeatherTSSInflow;
            this.SummerDryWeatherTNInflow = vProjectLoadReducingResult.SummerDryWeatherTNInflow;
            this.SummerDryWeatherTPInflow = vProjectLoadReducingResult.SummerDryWeatherTPInflow;
            this.SummerDryWeatherFCInflow = vProjectLoadReducingResult.SummerDryWeatherFCInflow;
            this.SummerDryWeatherTCuInflow = vProjectLoadReducingResult.SummerDryWeatherTCuInflow;
            this.SummerDryWeatherTPbInflow = vProjectLoadReducingResult.SummerDryWeatherTPbInflow;
            this.SummerDryWeatherTZnInflow = vProjectLoadReducingResult.SummerDryWeatherTZnInflow;
            this.WinterDryWeatherInflow = vProjectLoadReducingResult.WinterDryWeatherInflow;
            this.WinterDryWeatherTreated = vProjectLoadReducingResult.WinterDryWeatherTreated;
            this.WinterDryWeatherRetained = vProjectLoadReducingResult.WinterDryWeatherRetained;
            this.WinterDryWeatherUntreated = vProjectLoadReducingResult.WinterDryWeatherUntreated;
            this.WinterDryWeatherTSSReduced = vProjectLoadReducingResult.WinterDryWeatherTSSReduced;
            this.WinterDryWeatherTNReduced = vProjectLoadReducingResult.WinterDryWeatherTNReduced;
            this.WinterDryWeatherTPReduced = vProjectLoadReducingResult.WinterDryWeatherTPReduced;
            this.WinterDryWeatherFCReduced = vProjectLoadReducingResult.WinterDryWeatherFCReduced;
            this.WinterDryWeatherTCuReduced = vProjectLoadReducingResult.WinterDryWeatherTCuReduced;
            this.WinterDryWeatherTPbReduced = vProjectLoadReducingResult.WinterDryWeatherTPbReduced;
            this.WinterDryWeatherTZnReduced = vProjectLoadReducingResult.WinterDryWeatherTZnReduced;
            this.WinterDryWeatherTSSInflow = vProjectLoadReducingResult.WinterDryWeatherTSSInflow;
            this.WinterDryWeatherTNInflow = vProjectLoadReducingResult.WinterDryWeatherTNInflow;
            this.WinterDryWeatherTPInflow = vProjectLoadReducingResult.WinterDryWeatherTPInflow;
            this.WinterDryWeatherFCInflow = vProjectLoadReducingResult.WinterDryWeatherFCInflow;
            this.WinterDryWeatherTCuInflow = vProjectLoadReducingResult.WinterDryWeatherTCuInflow;
            this.WinterDryWeatherTPbInflow = vProjectLoadReducingResult.WinterDryWeatherTPbInflow;
            this.WinterDryWeatherTZnInflow = vProjectLoadReducingResult.WinterDryWeatherTZnInflow;
            CallAfterConstructor(vProjectLoadReducingResult);
        }

        partial void CallAfterConstructor(vProjectLoadReducingResult vProjectLoadReducingResult);

        public int PrimaryKey { get; set; }
        public int ProjectNereidResultID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public string NodeID { get; set; }
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