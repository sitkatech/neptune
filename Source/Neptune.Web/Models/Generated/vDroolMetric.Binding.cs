//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Use the corresponding partial class for customizations.
//  Source Table: [dbo].[vDroolMetric]
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
    public partial class vDroolMetric
    {
        /// <summary>
        /// Needed by ModelBinder
        /// </summary>
        public vDroolMetric()
        {
        }

        /// <summary>
        /// Constructor for building a new object with MaximalConstructor required fields in preparation for insert into database
        /// </summary>
        public vDroolMetric(int primaryKey, int rawDroolMetricID, int oCSurveyCatchmentID, int year, int month, double? numberOfReshoaAccounts, double? totalReshoaIrrigatedArea, double? averageIrrigatedArea, double? totalEstimatedReshoaUsers, double? totalBudget, double? totalOutdoorBudget, double? averageTotalUsage, double? averageEstimatedIrrigationUsage, double? numberOfAccountsOverBudget, double? percentOfAccountsOverBudget, double? averageOverBudgetUsage, double? totalOverBudgetUsage, double? rebateParticipationPercentage, double? totalTurfReplacementArea) : this()
        {
            this.PrimaryKey = primaryKey;
            this.RawDroolMetricID = rawDroolMetricID;
            this.OCSurveyCatchmentID = oCSurveyCatchmentID;
            this.Year = year;
            this.Month = month;
            this.NumberOfReshoaAccounts = numberOfReshoaAccounts;
            this.TotalReshoaIrrigatedArea = totalReshoaIrrigatedArea;
            this.AverageIrrigatedArea = averageIrrigatedArea;
            this.TotalEstimatedReshoaUsers = totalEstimatedReshoaUsers;
            this.TotalBudget = totalBudget;
            this.TotalOutdoorBudget = totalOutdoorBudget;
            this.AverageTotalUsage = averageTotalUsage;
            this.AverageEstimatedIrrigationUsage = averageEstimatedIrrigationUsage;
            this.NumberOfAccountsOverBudget = numberOfAccountsOverBudget;
            this.PercentOfAccountsOverBudget = percentOfAccountsOverBudget;
            this.AverageOverBudgetUsage = averageOverBudgetUsage;
            this.TotalOverBudgetUsage = totalOverBudgetUsage;
            this.RebateParticipationPercentage = rebateParticipationPercentage;
            this.TotalTurfReplacementArea = totalTurfReplacementArea;
        }

        /// <summary>
        /// Constructor for building a new simple object with the POCO class
        /// </summary>
        public vDroolMetric(vDroolMetric vDroolMetric) : this()
        {
            this.PrimaryKey = vDroolMetric.PrimaryKey;
            this.RawDroolMetricID = vDroolMetric.RawDroolMetricID;
            this.OCSurveyCatchmentID = vDroolMetric.OCSurveyCatchmentID;
            this.Year = vDroolMetric.Year;
            this.Month = vDroolMetric.Month;
            this.NumberOfReshoaAccounts = vDroolMetric.NumberOfReshoaAccounts;
            this.TotalReshoaIrrigatedArea = vDroolMetric.TotalReshoaIrrigatedArea;
            this.AverageIrrigatedArea = vDroolMetric.AverageIrrigatedArea;
            this.TotalEstimatedReshoaUsers = vDroolMetric.TotalEstimatedReshoaUsers;
            this.TotalBudget = vDroolMetric.TotalBudget;
            this.TotalOutdoorBudget = vDroolMetric.TotalOutdoorBudget;
            this.AverageTotalUsage = vDroolMetric.AverageTotalUsage;
            this.AverageEstimatedIrrigationUsage = vDroolMetric.AverageEstimatedIrrigationUsage;
            this.NumberOfAccountsOverBudget = vDroolMetric.NumberOfAccountsOverBudget;
            this.PercentOfAccountsOverBudget = vDroolMetric.PercentOfAccountsOverBudget;
            this.AverageOverBudgetUsage = vDroolMetric.AverageOverBudgetUsage;
            this.TotalOverBudgetUsage = vDroolMetric.TotalOverBudgetUsage;
            this.RebateParticipationPercentage = vDroolMetric.RebateParticipationPercentage;
            this.TotalTurfReplacementArea = vDroolMetric.TotalTurfReplacementArea;
            CallAfterConstructor(vDroolMetric);
        }

        partial void CallAfterConstructor(vDroolMetric vDroolMetric);

        public int PrimaryKey { get; set; }
        public int RawDroolMetricID { get; set; }
        public int OCSurveyCatchmentID { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public double? NumberOfReshoaAccounts { get; set; }
        public double? TotalReshoaIrrigatedArea { get; set; }
        public double? AverageIrrigatedArea { get; set; }
        public double? TotalEstimatedReshoaUsers { get; set; }
        public double? TotalBudget { get; set; }
        public double? TotalOutdoorBudget { get; set; }
        public double? AverageTotalUsage { get; set; }
        public double? AverageEstimatedIrrigationUsage { get; set; }
        public double? NumberOfAccountsOverBudget { get; set; }
        public double? PercentOfAccountsOverBudget { get; set; }
        public double? AverageOverBudgetUsage { get; set; }
        public double? TotalOverBudgetUsage { get; set; }
        public double? RebateParticipationPercentage { get; set; }
        public double? TotalTurfReplacementArea { get; set; }
    }
}