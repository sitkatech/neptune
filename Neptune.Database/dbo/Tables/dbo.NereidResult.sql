CREATE TABLE [dbo].[NereidResult](
	[NereidResultID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_NereidResult_NereidResultID] PRIMARY KEY,
	[TreatmentBMPID] [int] NULL CONSTRAINT [FK_NereidResult_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[WaterQualityManagementPlanID] [int] NULL CONSTRAINT [FK_NereidResult_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[RegionalSubbasinID] [int] NULL CONSTRAINT [FK_NereidResult_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID]),
	[DelineationID] [int] NULL CONSTRAINT [FK_NereidResult_Delineation_DelineationID] FOREIGN KEY REFERENCES [dbo].[Delineation] ([DelineationID]),
	[NodeID] [varchar](max) NULL,
	[FullResponse] [varchar](max),
	[LastUpdate] [datetime] NULL,
	[IsBaselineCondition] [bit] NOT NULL
)