//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vProjectModelingResults]
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
    public partial class vProjectModelingResults
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vProjectModelingResults()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vProjectModelingResults(int primaryKey, int projectNereidResultID, int projectID, string projectName, bool isBaselineCondition, int? treatmentBMPID, string treatmentBMPName, int? waterQualityManagementPlanID, int? regionalSubbasinID, int? delineationID, string nodeID, DateTime? lastUpdate, double? wetWeatherInflow, double? wetWeatherTreated, double? wetWeatherRetained, double? wetWeatherUntreated, double? wetWeatherTSSRemoved, double? wetWeatherTNRemoved, double? wetWeatherTPRemoved, double? wetWeatherFCRemoved, double? wetWeatherTCuRemoved, double? wetWeatherTPbRemoved, double? wetWeatherTZnRemoved, double? wetWeatherTSSInflow, double? wetWeatherTNInflow, double? wetWeatherTPInflow, double? wetWeatherFCInflow, double? wetWeatherTCuInflow, double? wetWeatherTPbInflow, double? wetWeatherTZnInflow, double? summerDryWeatherInflow, double? summerDryWeatherTreated, double? summerDryWeatherRetained, double? summerDryWeatherUntreated, double? summerDryWeatherTSSRemoved, double? summerDryWeatherTNRemoved, double? summerDryWeatherTPRemoved, double? summerDryWeatherFCRemoved, double? summerDryWeatherTCuRemoved, double? summerDryWeatherTPbRemoved, double? summerDryWeatherTZnRemoved, double? summerDryWeatherTSSInflow, double? summerDryWeatherTNInflow, double? summerDryWeatherTPInflow, double? summerDryWeatherFCInflow, double? summerDryWeatherTCuInflow, double? summerDryWeatherTPbInflow, double? summerDryWeatherTZnInflow, double? winterDryWeatherInflow, double? winterDryWeatherTreated, double? winterDryWeatherRetained, double? winterDryWeatherUntreated, double? winterDryWeatherTSSRemoved, double? winterDryWeatherTNRemoved, double? winterDryWeatherTPRemoved, double? winterDryWeatherFCRemoved, double? winterDryWeatherTCuRemoved, double? winterDryWeatherTPbRemoved, double? winterDryWeatherTZnRemoved, double? winterDryWeatherTSSInflow, double? winterDryWeatherTNInflow, double? winterDryWeatherTPInflow, double? winterDryWeatherFCInflow, double? winterDryWeatherTCuInflow, double? winterDryWeatherTPbInflow, double? winterDryWeatherTZnInflow) : this()
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
            this.WetWeatherInflow = wetWeatherInflow;
            this.WetWeatherTreated = wetWeatherTreated;
            this.WetWeatherRetained = wetWeatherRetained;
            this.WetWeatherUntreated = wetWeatherUntreated;
            this.WetWeatherTSSRemoved = wetWeatherTSSRemoved;
            this.WetWeatherTNRemoved = wetWeatherTNRemoved;
            this.WetWeatherTPRemoved = wetWeatherTPRemoved;
            this.WetWeatherFCRemoved = wetWeatherFCRemoved;
            this.WetWeatherTCuRemoved = wetWeatherTCuRemoved;
            this.WetWeatherTPbRemoved = wetWeatherTPbRemoved;
            this.WetWeatherTZnRemoved = wetWeatherTZnRemoved;
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
            this.SummerDryWeatherTSSRemoved = summerDryWeatherTSSRemoved;
            this.SummerDryWeatherTNRemoved = summerDryWeatherTNRemoved;
            this.SummerDryWeatherTPRemoved = summerDryWeatherTPRemoved;
            this.SummerDryWeatherFCRemoved = summerDryWeatherFCRemoved;
            this.SummerDryWeatherTCuRemoved = summerDryWeatherTCuRemoved;
            this.SummerDryWeatherTPbRemoved = summerDryWeatherTPbRemoved;
            this.SummerDryWeatherTZnRemoved = summerDryWeatherTZnRemoved;
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
            this.WinterDryWeatherTSSRemoved = winterDryWeatherTSSRemoved;
            this.WinterDryWeatherTNRemoved = winterDryWeatherTNRemoved;
            this.WinterDryWeatherTPRemoved = winterDryWeatherTPRemoved;
            this.WinterDryWeatherFCRemoved = winterDryWeatherFCRemoved;
            this.WinterDryWeatherTCuRemoved = winterDryWeatherTCuRemoved;
            this.WinterDryWeatherTPbRemoved = winterDryWeatherTPbRemoved;
            this.WinterDryWeatherTZnRemoved = winterDryWeatherTZnRemoved;
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
        public vProjectModelingResults(vProjectModelingResults vProjectModelingResults) : this()
        {
            this.PrimaryKey = vProjectModelingResults.PrimaryKey;
            this.ProjectNereidResultID = vProjectModelingResults.ProjectNereidResultID;
            this.ProjectID = vProjectModelingResults.ProjectID;
            this.ProjectName = vProjectModelingResults.ProjectName;
            this.IsBaselineCondition = vProjectModelingResults.IsBaselineCondition;
            this.TreatmentBMPID = vProjectModelingResults.TreatmentBMPID;
            this.TreatmentBMPName = vProjectModelingResults.TreatmentBMPName;
            this.WaterQualityManagementPlanID = vProjectModelingResults.WaterQualityManagementPlanID;
            this.RegionalSubbasinID = vProjectModelingResults.RegionalSubbasinID;
            this.DelineationID = vProjectModelingResults.DelineationID;
            this.NodeID = vProjectModelingResults.NodeID;
            this.LastUpdate = vProjectModelingResults.LastUpdate;
            this.WetWeatherInflow = vProjectModelingResults.WetWeatherInflow;
            this.WetWeatherTreated = vProjectModelingResults.WetWeatherTreated;
            this.WetWeatherRetained = vProjectModelingResults.WetWeatherRetained;
            this.WetWeatherUntreated = vProjectModelingResults.WetWeatherUntreated;
            this.WetWeatherTSSRemoved = vProjectModelingResults.WetWeatherTSSRemoved;
            this.WetWeatherTNRemoved = vProjectModelingResults.WetWeatherTNRemoved;
            this.WetWeatherTPRemoved = vProjectModelingResults.WetWeatherTPRemoved;
            this.WetWeatherFCRemoved = vProjectModelingResults.WetWeatherFCRemoved;
            this.WetWeatherTCuRemoved = vProjectModelingResults.WetWeatherTCuRemoved;
            this.WetWeatherTPbRemoved = vProjectModelingResults.WetWeatherTPbRemoved;
            this.WetWeatherTZnRemoved = vProjectModelingResults.WetWeatherTZnRemoved;
            this.WetWeatherTSSInflow = vProjectModelingResults.WetWeatherTSSInflow;
            this.WetWeatherTNInflow = vProjectModelingResults.WetWeatherTNInflow;
            this.WetWeatherTPInflow = vProjectModelingResults.WetWeatherTPInflow;
            this.WetWeatherFCInflow = vProjectModelingResults.WetWeatherFCInflow;
            this.WetWeatherTCuInflow = vProjectModelingResults.WetWeatherTCuInflow;
            this.WetWeatherTPbInflow = vProjectModelingResults.WetWeatherTPbInflow;
            this.WetWeatherTZnInflow = vProjectModelingResults.WetWeatherTZnInflow;
            this.SummerDryWeatherInflow = vProjectModelingResults.SummerDryWeatherInflow;
            this.SummerDryWeatherTreated = vProjectModelingResults.SummerDryWeatherTreated;
            this.SummerDryWeatherRetained = vProjectModelingResults.SummerDryWeatherRetained;
            this.SummerDryWeatherUntreated = vProjectModelingResults.SummerDryWeatherUntreated;
            this.SummerDryWeatherTSSRemoved = vProjectModelingResults.SummerDryWeatherTSSRemoved;
            this.SummerDryWeatherTNRemoved = vProjectModelingResults.SummerDryWeatherTNRemoved;
            this.SummerDryWeatherTPRemoved = vProjectModelingResults.SummerDryWeatherTPRemoved;
            this.SummerDryWeatherFCRemoved = vProjectModelingResults.SummerDryWeatherFCRemoved;
            this.SummerDryWeatherTCuRemoved = vProjectModelingResults.SummerDryWeatherTCuRemoved;
            this.SummerDryWeatherTPbRemoved = vProjectModelingResults.SummerDryWeatherTPbRemoved;
            this.SummerDryWeatherTZnRemoved = vProjectModelingResults.SummerDryWeatherTZnRemoved;
            this.SummerDryWeatherTSSInflow = vProjectModelingResults.SummerDryWeatherTSSInflow;
            this.SummerDryWeatherTNInflow = vProjectModelingResults.SummerDryWeatherTNInflow;
            this.SummerDryWeatherTPInflow = vProjectModelingResults.SummerDryWeatherTPInflow;
            this.SummerDryWeatherFCInflow = vProjectModelingResults.SummerDryWeatherFCInflow;
            this.SummerDryWeatherTCuInflow = vProjectModelingResults.SummerDryWeatherTCuInflow;
            this.SummerDryWeatherTPbInflow = vProjectModelingResults.SummerDryWeatherTPbInflow;
            this.SummerDryWeatherTZnInflow = vProjectModelingResults.SummerDryWeatherTZnInflow;
            this.WinterDryWeatherInflow = vProjectModelingResults.WinterDryWeatherInflow;
            this.WinterDryWeatherTreated = vProjectModelingResults.WinterDryWeatherTreated;
            this.WinterDryWeatherRetained = vProjectModelingResults.WinterDryWeatherRetained;
            this.WinterDryWeatherUntreated = vProjectModelingResults.WinterDryWeatherUntreated;
            this.WinterDryWeatherTSSRemoved = vProjectModelingResults.WinterDryWeatherTSSRemoved;
            this.WinterDryWeatherTNRemoved = vProjectModelingResults.WinterDryWeatherTNRemoved;
            this.WinterDryWeatherTPRemoved = vProjectModelingResults.WinterDryWeatherTPRemoved;
            this.WinterDryWeatherFCRemoved = vProjectModelingResults.WinterDryWeatherFCRemoved;
            this.WinterDryWeatherTCuRemoved = vProjectModelingResults.WinterDryWeatherTCuRemoved;
            this.WinterDryWeatherTPbRemoved = vProjectModelingResults.WinterDryWeatherTPbRemoved;
            this.WinterDryWeatherTZnRemoved = vProjectModelingResults.WinterDryWeatherTZnRemoved;
            this.WinterDryWeatherTSSInflow = vProjectModelingResults.WinterDryWeatherTSSInflow;
            this.WinterDryWeatherTNInflow = vProjectModelingResults.WinterDryWeatherTNInflow;
            this.WinterDryWeatherTPInflow = vProjectModelingResults.WinterDryWeatherTPInflow;
            this.WinterDryWeatherFCInflow = vProjectModelingResults.WinterDryWeatherFCInflow;
            this.WinterDryWeatherTCuInflow = vProjectModelingResults.WinterDryWeatherTCuInflow;
            this.WinterDryWeatherTPbInflow = vProjectModelingResults.WinterDryWeatherTPbInflow;
            this.WinterDryWeatherTZnInflow = vProjectModelingResults.WinterDryWeatherTZnInflow;
            CallAfterConstructor(vProjectModelingResults);
        }

        partial void CallAfterConstructor(vProjectModelingResults vProjectModelingResults);

        public int PrimaryKey { get; set; }
        public int ProjectNereidResultID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public bool IsBaselineCondition { get; set; }
        public int? TreatmentBMPID { get; set; }
        public string TreatmentBMPName { get; set; }
        public int? WaterQualityManagementPlanID { get; set; }
        public int? RegionalSubbasinID { get; set; }
        public int? DelineationID { get; set; }
        public string NodeID { get; set; }
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