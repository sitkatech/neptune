Drop View If Exists dbo.vDroolMetric
Go

Create view dbo.vDroolMetric
as
Select 
	[RawDroolMetricID] as PrimaryKey,
	[RawDroolMetricID],
	[CatchIDN] as OCSurveyCatchmentID, 
	[Year],
	[Month],
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
	[reshoa_daily_meter_budget_overage_sum] as TotalOverBudgetUsage,
	[reshoa_rebate_participation_fraction] RebateParticipationPercentage,
	[reshoa_turf_rebate_area_sqft_sum] as TotalTurfReplacementArea
from dbo.RawDroolMetric
Go

select * from dbo.vDroolMetric