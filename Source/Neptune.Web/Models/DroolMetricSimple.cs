namespace Neptune.Web.Models
{
    public class DroolMetricSimple
    {
        public int? MetricYear { get; set; }
        public int? MetricMonth { get; set; }
        public string NumberOfReshoaAccounts { get; set; }
        public string TotalReshoaIrrigatedArea { get; set; }
        public string AverageIrrigatedArea { get; set; }
        public string TotalEstimatedReshoaUsers { get; set; }
        public string TotalBudget { get; set; }
        public string TotalOutdoorBudget { get; set; }
        public string AverageTotalUsage { get; set; }
        public string AverageEstimatedIrrigationUsage { get; set; }
        public string NumberOfAccountsOverBudget { get; set; }
        public string PercentOfAccountsOverBudget { get; set; }
        public string AverageOverBudgetUsage { get; set; }
        public string AverageOverBudgetUsageRolling { get; set; }
        public string AverageOverBudgetUsageSlope { get; set; }
        public string TotalOverBudgetUsage { get; set; }
        public string RebateParticipationPercentage { get; set; }
        public string RebateParticipationPercentageRolling { get; set; }
        public string RebateParticipationPercentageSlope { get; set; }
        public string TotalTurfReplacementArea { get; set; }

        public DroolMetricSimple(vDroolMetric vDroolMetric)
        {
            MetricYear = vDroolMetric?.MetricYear;
            MetricMonth = vDroolMetric?.MetricMonth;
            NumberOfReshoaAccounts = vDroolMetric?.NumberOfReshoaAccounts == null ? "Not Available" : (vDroolMetric.NumberOfReshoaAccounts.Value).ToString("N0");
            TotalReshoaIrrigatedArea = vDroolMetric?.TotalReshoaIrrigatedArea == null ? "Not Available" : (vDroolMetric.TotalReshoaIrrigatedArea.Value).ToString("N0") + " sq ft";
            AverageIrrigatedArea = vDroolMetric?.AverageIrrigatedArea == null ? "Not Available" : (vDroolMetric.AverageIrrigatedArea.Value).ToString("N0")+ " sq ft per household";
            TotalEstimatedReshoaUsers = vDroolMetric?.TotalEstimatedReshoaUsers == null ? "Not Available" : (vDroolMetric.TotalEstimatedReshoaUsers.Value).ToString("N0");
            TotalBudget = vDroolMetric?.TotalBudget == null ? "Not Available" : (vDroolMetric.TotalBudget.Value * 748 * 30 ).ToString("N0") + " gal per month";
            TotalOutdoorBudget = vDroolMetric?.TotalOutdoorBudget == null ? "Not Available" : (vDroolMetric.TotalOutdoorBudget.Value * 748 * 30).ToString("N0") + " gal per month"; 
            AverageTotalUsage = vDroolMetric?.AverageTotalUsage == null ? "Not Available" : (vDroolMetric.AverageTotalUsage.Value * 748 * 30).ToString("N0") + " gal per household";
            AverageEstimatedIrrigationUsage = vDroolMetric?.AverageEstimatedIrrigationUsage == null ? "Not Available" : (vDroolMetric.AverageEstimatedIrrigationUsage.Value * 748 * 30).ToString("N0") + " gal per household";
            NumberOfAccountsOverBudget = vDroolMetric?.NumberOfAccountsOverBudget == null ? "Not Available" : (vDroolMetric.NumberOfAccountsOverBudget.Value).ToString("N0");
            PercentOfAccountsOverBudget = vDroolMetric?.PercentOfAccountsOverBudget == null ? "Not Available" : (vDroolMetric.PercentOfAccountsOverBudget.Value).ToString("P");
            AverageOverBudgetUsage = vDroolMetric?.AverageOverBudgetUsage == null ? "Not Available" : (vDroolMetric.AverageOverBudgetUsage.Value * 748 * 30).ToString("N0")+ " gal per household";
            AverageOverBudgetUsageRolling = vDroolMetric?.AverageOverBudgetUsageRolling == null ? "Not Available" : (vDroolMetric.AverageOverBudgetUsageRolling.Value).ToString("N");
            AverageOverBudgetUsageSlope = vDroolMetric?.AverageOverBudgetUsageSlope == null ? "Not Available" : (vDroolMetric.AverageOverBudgetUsageSlope.Value).ToString("N");
            TotalOverBudgetUsage = vDroolMetric?.TotalOverBudgetUsage == null ? "Not Available" : (vDroolMetric.TotalOverBudgetUsage.Value * 748 * 30).ToString("N0") + " gal";
            RebateParticipationPercentage = vDroolMetric?.RebateParticipationPercentage == null ? "Not Available" : (vDroolMetric.RebateParticipationPercentage.Value).ToString("P");
            RebateParticipationPercentageRolling = vDroolMetric?.RebateParticipationPercentageRolling == null ? "Not Available" : (vDroolMetric.RebateParticipationPercentageRolling.Value).ToString("N");
            RebateParticipationPercentageSlope = vDroolMetric?.RebateParticipationPercentageSlope == null ? "Not Available" : (vDroolMetric.RebateParticipationPercentageSlope.Value).ToString("N");
            TotalTurfReplacementArea = vDroolMetric?.TotalTurfReplacementArea == null ? "Not Available" : (vDroolMetric.TotalTurfReplacementArea.Value).ToString("N0") + " sq ft";
        }
    }
}