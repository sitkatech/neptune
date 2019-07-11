SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RawDroolMetric](
	[RawDroolMetricID] [int] NOT NULL,
	[CatchIDN] [int] NOT NULL,
	[Year] [int] NOT NULL,
	[Month] [int] NOT NULL,
	[reshoa_MeterID_count] [float] NULL,
	[reshoa_irrg_area_sqft_sum] [float] NULL,
	[reshoa_irrg_area_sqft_mean] [float] NULL,
	[reshoa_headcount_sum] [float] NULL,
	[reshoa_daily_total_budget_sum] [float] NULL,
	[reshoa_daily_outdoor_budget_sum] [float] NULL,
	[reshoa_daily_TotalUsage_mean] [float] NULL,
	[reshoa_daily_est_outdoor_usage_mean] [float] NULL,
	[reshoa_meter_is_over_total_budget_sum] [float] NULL,
	[reshoa_daily_meter_budget_overage_per_meter] [float] NULL,
	[reshoa_daily_meter_budget_overage_sum] [float] NULL,
	[reshoa_rebate_participation_fraction] [float] NULL,
	[reshoa_turf_rebate_area_sqft_sum] [float] NULL,
 CONSTRAINT [PK_RawDroolMetric_RawDroolMetricID] PRIMARY KEY CLUSTERED 
(
	[RawDroolMetricID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[RawDroolMetric]  WITH CHECK ADD  CONSTRAINT [FK_RawDroolMatric_NetworkCatchment_CatchIDN_OCSurveyCatchmentID] FOREIGN KEY([CatchIDN])
REFERENCES [dbo].[NetworkCatchment] ([OCSurveyCatchmentID])
GO
ALTER TABLE [dbo].[RawDroolMetric] CHECK CONSTRAINT [FK_RawDroolMatric_NetworkCatchment_CatchIDN_OCSurveyCatchmentID]