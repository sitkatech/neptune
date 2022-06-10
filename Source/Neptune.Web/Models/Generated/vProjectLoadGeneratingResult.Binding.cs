//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vProjectLoadGeneratingResult]
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
    public partial class vProjectLoadGeneratingResult
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vProjectLoadGeneratingResult()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vProjectLoadGeneratingResult(int primaryKey, int projectNereidResultID, int projectID, string projectName, bool isBaselineCondition, int? waterQualityManagementPlanID, int? regionalSubbasinID, int? delineationID, string nodeID, DateTime? lastUpdate, double? wetWeatherVolumeGenerated, double? wetWeatherTSSGenerated, double? wetWeatherTNGenerated, double? wetWeatherTPGenerated, double? wetWeatherFCGenerated, double? wetWeatherTCuGenerated, double? wetWeatherTPbGenerated, double? wetWeatherTZnGenerated, double? summerDryWeatherVolumeGenerated, double? summerDryWeatherTSSGenerated, double? summerDryWeatherTNGenerated, double? summerDryWeatherTPGenerated, double? summerDryWeatherFCGenerated, double? summerDryWeatherTCuGenerated, double? summerDryWeatherTPbGenerated, double? summerDryWeatherTZnGenerated, double? winterDryWeatherVolumeGenerated, double? winterDryWeatherTSSGenerated, double? winterDryWeatherTNGenerated, double? winterDryWeatherTPGenerated, double? winterDryWeatherFCGenerated, double? winterDryWeatherTCuGenerated, double? winterDryWeatherTPbGenerated, double? winterDryWeatherTZnGenerated) : this()
        {
            this.PrimaryKey = primaryKey;
            this.ProjectNereidResultID = projectNereidResultID;
            this.ProjectID = projectID;
            this.ProjectName = projectName;
            this.IsBaselineCondition = isBaselineCondition;
            this.WaterQualityManagementPlanID = waterQualityManagementPlanID;
            this.RegionalSubbasinID = regionalSubbasinID;
            this.DelineationID = delineationID;
            this.NodeID = nodeID;
            this.LastUpdate = lastUpdate;
            this.WetWeatherVolumeGenerated = wetWeatherVolumeGenerated;
            this.WetWeatherTSSGenerated = wetWeatherTSSGenerated;
            this.WetWeatherTNGenerated = wetWeatherTNGenerated;
            this.WetWeatherTPGenerated = wetWeatherTPGenerated;
            this.WetWeatherFCGenerated = wetWeatherFCGenerated;
            this.WetWeatherTCuGenerated = wetWeatherTCuGenerated;
            this.WetWeatherTPbGenerated = wetWeatherTPbGenerated;
            this.WetWeatherTZnGenerated = wetWeatherTZnGenerated;
            this.SummerDryWeatherVolumeGenerated = summerDryWeatherVolumeGenerated;
            this.SummerDryWeatherTSSGenerated = summerDryWeatherTSSGenerated;
            this.SummerDryWeatherTNGenerated = summerDryWeatherTNGenerated;
            this.SummerDryWeatherTPGenerated = summerDryWeatherTPGenerated;
            this.SummerDryWeatherFCGenerated = summerDryWeatherFCGenerated;
            this.SummerDryWeatherTCuGenerated = summerDryWeatherTCuGenerated;
            this.SummerDryWeatherTPbGenerated = summerDryWeatherTPbGenerated;
            this.SummerDryWeatherTZnGenerated = summerDryWeatherTZnGenerated;
            this.WinterDryWeatherVolumeGenerated = winterDryWeatherVolumeGenerated;
            this.WinterDryWeatherTSSGenerated = winterDryWeatherTSSGenerated;
            this.WinterDryWeatherTNGenerated = winterDryWeatherTNGenerated;
            this.WinterDryWeatherTPGenerated = winterDryWeatherTPGenerated;
            this.WinterDryWeatherFCGenerated = winterDryWeatherFCGenerated;
            this.WinterDryWeatherTCuGenerated = winterDryWeatherTCuGenerated;
            this.WinterDryWeatherTPbGenerated = winterDryWeatherTPbGenerated;
            this.WinterDryWeatherTZnGenerated = winterDryWeatherTZnGenerated;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vProjectLoadGeneratingResult(vProjectLoadGeneratingResult vProjectLoadGeneratingResult) : this()
        {
            this.PrimaryKey = vProjectLoadGeneratingResult.PrimaryKey;
            this.ProjectNereidResultID = vProjectLoadGeneratingResult.ProjectNereidResultID;
            this.ProjectID = vProjectLoadGeneratingResult.ProjectID;
            this.ProjectName = vProjectLoadGeneratingResult.ProjectName;
            this.IsBaselineCondition = vProjectLoadGeneratingResult.IsBaselineCondition;
            this.WaterQualityManagementPlanID = vProjectLoadGeneratingResult.WaterQualityManagementPlanID;
            this.RegionalSubbasinID = vProjectLoadGeneratingResult.RegionalSubbasinID;
            this.DelineationID = vProjectLoadGeneratingResult.DelineationID;
            this.NodeID = vProjectLoadGeneratingResult.NodeID;
            this.LastUpdate = vProjectLoadGeneratingResult.LastUpdate;
            this.WetWeatherVolumeGenerated = vProjectLoadGeneratingResult.WetWeatherVolumeGenerated;
            this.WetWeatherTSSGenerated = vProjectLoadGeneratingResult.WetWeatherTSSGenerated;
            this.WetWeatherTNGenerated = vProjectLoadGeneratingResult.WetWeatherTNGenerated;
            this.WetWeatherTPGenerated = vProjectLoadGeneratingResult.WetWeatherTPGenerated;
            this.WetWeatherFCGenerated = vProjectLoadGeneratingResult.WetWeatherFCGenerated;
            this.WetWeatherTCuGenerated = vProjectLoadGeneratingResult.WetWeatherTCuGenerated;
            this.WetWeatherTPbGenerated = vProjectLoadGeneratingResult.WetWeatherTPbGenerated;
            this.WetWeatherTZnGenerated = vProjectLoadGeneratingResult.WetWeatherTZnGenerated;
            this.SummerDryWeatherVolumeGenerated = vProjectLoadGeneratingResult.SummerDryWeatherVolumeGenerated;
            this.SummerDryWeatherTSSGenerated = vProjectLoadGeneratingResult.SummerDryWeatherTSSGenerated;
            this.SummerDryWeatherTNGenerated = vProjectLoadGeneratingResult.SummerDryWeatherTNGenerated;
            this.SummerDryWeatherTPGenerated = vProjectLoadGeneratingResult.SummerDryWeatherTPGenerated;
            this.SummerDryWeatherFCGenerated = vProjectLoadGeneratingResult.SummerDryWeatherFCGenerated;
            this.SummerDryWeatherTCuGenerated = vProjectLoadGeneratingResult.SummerDryWeatherTCuGenerated;
            this.SummerDryWeatherTPbGenerated = vProjectLoadGeneratingResult.SummerDryWeatherTPbGenerated;
            this.SummerDryWeatherTZnGenerated = vProjectLoadGeneratingResult.SummerDryWeatherTZnGenerated;
            this.WinterDryWeatherVolumeGenerated = vProjectLoadGeneratingResult.WinterDryWeatherVolumeGenerated;
            this.WinterDryWeatherTSSGenerated = vProjectLoadGeneratingResult.WinterDryWeatherTSSGenerated;
            this.WinterDryWeatherTNGenerated = vProjectLoadGeneratingResult.WinterDryWeatherTNGenerated;
            this.WinterDryWeatherTPGenerated = vProjectLoadGeneratingResult.WinterDryWeatherTPGenerated;
            this.WinterDryWeatherFCGenerated = vProjectLoadGeneratingResult.WinterDryWeatherFCGenerated;
            this.WinterDryWeatherTCuGenerated = vProjectLoadGeneratingResult.WinterDryWeatherTCuGenerated;
            this.WinterDryWeatherTPbGenerated = vProjectLoadGeneratingResult.WinterDryWeatherTPbGenerated;
            this.WinterDryWeatherTZnGenerated = vProjectLoadGeneratingResult.WinterDryWeatherTZnGenerated;
            CallAfterConstructor(vProjectLoadGeneratingResult);
        }

        partial void CallAfterConstructor(vProjectLoadGeneratingResult vProjectLoadGeneratingResult);

        public int PrimaryKey { get; set; }
        public int ProjectNereidResultID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public string NodeID { get; set; }
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
    }
}