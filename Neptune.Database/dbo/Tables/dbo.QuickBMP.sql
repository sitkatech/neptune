CREATE TABLE [dbo].[QuickBMP](
	[QuickBMPID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_QuickBMP_QuickBMPID] PRIMARY KEY,
	[WaterQualityManagementPlanID] [int] NOT NULL CONSTRAINT [FK_QuickBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[TreatmentBMPTypeID] [int] NOT NULL CONSTRAINT [FK_QuickBMP_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID]),
	[QuickBMPName] [varchar](100),
	[QuickBMPNote] [varchar](200) NULL,
	[PercentOfSiteTreated] [decimal](5, 2) NULL,
	[PercentCaptured] [decimal](5, 2) NULL,
	[PercentRetained] [decimal](5, 2) NULL,
	[DryWeatherFlowOverrideID] [int] NULL CONSTRAINT [FK_QuickBMP_DryWeatherFlowOverride_DryWeatherFlowOverrideID] FOREIGN KEY REFERENCES [dbo].[DryWeatherFlowOverride] ([DryWeatherFlowOverrideID]),
	[NumberOfIndividualBMPs] [int] NOT NULL default 1,
	CONSTRAINT [AK_QuickBMP_WaterQualityManagementPlanID_QuickBMPName] UNIQUE([WaterQualityManagementPlanID], [QuickBMPName])
)