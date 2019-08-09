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
            NumberOfReshoaAccounts = vDroolMetric?.NumberOfReshoaAccounts == null ? "Not Available" : vDroolMetric.NumberOfReshoaAccounts.Value.ToString("N");
            TotalReshoaIrrigatedArea = vDroolMetric?.TotalReshoaIrrigatedArea == null ? "Not Available" : vDroolMetric.TotalReshoaIrrigatedArea.Value.ToString("N");
            AverageIrrigatedArea = vDroolMetric?.AverageIrrigatedArea == null ? "Not Available" : vDroolMetric.AverageIrrigatedArea.Value.ToString("N");
            TotalEstimatedReshoaUsers = vDroolMetric?.TotalEstimatedReshoaUsers == null ? "Not Available" : vDroolMetric.TotalEstimatedReshoaUsers.Value.ToString("N");
            TotalBudget = vDroolMetric?.TotalBudget == null ? "Not Available" : vDroolMetric.TotalBudget.Value.ToString("N");
            TotalOutdoorBudget = vDroolMetric?.TotalOutdoorBudget == null ? "Not Available" : vDroolMetric.TotalOutdoorBudget.Value.ToString("N");
            AverageTotalUsage = vDroolMetric?.AverageTotalUsage == null ? "Not Available" : vDroolMetric.AverageTotalUsage.Value.ToString("N");
            AverageEstimatedIrrigationUsage = vDroolMetric?.AverageEstimatedIrrigationUsage == null ? "Not Available" : vDroolMetric.AverageEstimatedIrrigationUsage.Value.ToString("N");
            NumberOfAccountsOverBudget = vDroolMetric?.NumberOfAccountsOverBudget == null ? "Not Available" : vDroolMetric.NumberOfAccountsOverBudget.Value.ToString("N");
            PercentOfAccountsOverBudget = vDroolMetric?.PercentOfAccountsOverBudget == null ? "Not Available" : vDroolMetric.PercentOfAccountsOverBudget.Value.ToString("N");
            AverageOverBudgetUsage = vDroolMetric?.AverageOverBudgetUsage == null ? "Not Available" : vDroolMetric.AverageOverBudgetUsage.Value.ToString("N");
            AverageOverBudgetUsageRolling = vDroolMetric?.AverageOverBudgetUsageRolling == null ? "Not Available" : vDroolMetric.AverageOverBudgetUsageRolling.Value.ToString("N");
            AverageOverBudgetUsageSlope = vDroolMetric?.AverageOverBudgetUsageSlope == null ? "Not Available" : vDroolMetric.AverageOverBudgetUsageSlope.Value.ToString("N");
            TotalOverBudgetUsage = vDroolMetric?.TotalOverBudgetUsage == null ? "Not Available" : vDroolMetric.TotalOverBudgetUsage.Value.ToString("N");
            RebateParticipationPercentage = vDroolMetric?.RebateParticipationPercentage == null ? "Not Available" : vDroolMetric.RebateParticipationPercentage.Value.ToString("N");
            RebateParticipationPercentageRolling = vDroolMetric?.RebateParticipationPercentageRolling == null ? "Not Available" : vDroolMetric.RebateParticipationPercentageRolling.Value.ToString("N");
            RebateParticipationPercentageSlope = vDroolMetric?.RebateParticipationPercentageSlope == null ? "Not Available" : vDroolMetric.RebateParticipationPercentageSlope.Value.ToString("N");
            TotalTurfReplacementArea = vDroolMetric?.TotalTurfReplacementArea == null ? "Not Available" : vDroolMetric.TotalTurfReplacementArea.Value.ToString("N");
        }
    }
}