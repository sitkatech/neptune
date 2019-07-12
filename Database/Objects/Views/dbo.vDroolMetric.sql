Drop View If Exists dbo.vDroolMetric
Go

Create view dbo.vDroolMetric
as
Select 
	[RawDroolMetricID] as PrimaryKey,
	[RawDroolMetricID],
	[MetricCatchIDN] as OCSurveyCatchmentID, 
	[MetricYear],
	[MetricMonth],
	[reshoa_MeterID_count] as NumberOfReshoaAccounts,
	[reshoa_irrg_area_sqft_sum] as TotalReshoaIrrigatedArea,
	[reshoa_irrg_area_sqft_mean] as AverageIrrigatedArea,
	[reshoa_headcount_sum] as TotalEstimatedReshoaUsers,
	[reshoa_daily_total_budget_sum] as TotalBudget,
	[reshoa_daily_outdoor_budget_sum] as TotalOutdoorBudget,
	[reshoa_daily_TotalUsage_mean] as AverageTotalUsage,
	[reshoa_daily_est_outdoor_usage_mean] as AverageEstimatedIrrigationUsage,
	[reshoa_meter_is_over_total_budget_sum] as NumberOfAccountsOverBudget,
	[reshoa_meter_is_over_total_budget_sum]/[reshoa_MeterID_count] as PercentOfAccountsOverBudget,
	[reshoa_daily_meter_budget_overage_per_meter] as AverageOverBudgetUsage,
	[reshoa_roll_mean_12mo_daily_meter_budget_overage_per_meter] as AverageOverBudgetUsageRolling,
	[reshoa_slope_12mo_daily_meter_budget_overage_per_meter] as AverageOverBudgetUsageSlope,
	[reshoa_daily_meter_budget_overage_sum] as TotalOverBudgetUsage,
	[reshoa_rebate_participation_fraction] RebateParticipationPercentage,
	[reshoa_roll_mean_12mo_rebate_participation_fraction] RebateParticipationPercentageRolling,
	[reshoa_slope_12mo_rebate_participation_fraction] RebateParticipationPercentageSlope,
	[reshoa_turf_rebate_area_sqft_sum] as TotalTurfReplacementArea
from dbo.RawDroolMetric
Go