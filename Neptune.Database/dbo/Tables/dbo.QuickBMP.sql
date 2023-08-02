CREATE TABLE [dbo].[QuickBMP](
	[QuickBMPID] [int] IDENTITY(1,1) NOT NULL,
	[WaterQualityManagementPlanID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[QuickBMPName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[QuickBMPNote] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PercentOfSiteTreated] [decimal](5, 2) NULL,
	[PercentCaptured] [decimal](5, 2) NULL,
	[PercentRetained] [decimal](5, 2) NULL,
	[DryWeatherFlowOverrideID] [int] NULL,
 CONSTRAINT [PK_QuickBMP_QuickBMPID] PRIMARY KEY CLUSTERED 
(
	[QuickBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_QuickBMP_WaterQualityManagementPlanID_QuickBMPName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanID] ASC,
	[QuickBMPName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[QuickBMP]  WITH CHECK ADD  CONSTRAINT [FK_QuickBMP_DryWeatherFlowOverride_DryWeatherFlowOverrideID] FOREIGN KEY([DryWeatherFlowOverrideID])
REFERENCES [dbo].[DryWeatherFlowOverride] ([DryWeatherFlowOverrideID])
GO
ALTER TABLE [dbo].[QuickBMP] CHECK CONSTRAINT [FK_QuickBMP_DryWeatherFlowOverride_DryWeatherFlowOverrideID]
GO
ALTER TABLE [dbo].[QuickBMP]  WITH CHECK ADD  CONSTRAINT [FK_QuickBMP_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[QuickBMP] CHECK CONSTRAINT [FK_QuickBMP_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[QuickBMP]  WITH CHECK ADD  CONSTRAINT [FK_QuickBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[QuickBMP] CHECK CONSTRAINT [FK_QuickBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID]