CREATE TABLE [dbo].[ProjectNereidResult](
	[ProjectNereidResultID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ProjectNereidResult_ProjectNereidResultID] PRIMARY KEY,
	[ProjectID] [int] NOT NULL CONSTRAINT [FK_ProjectNereidResult_Project_ProjectID] FOREIGN KEY REFERENCES [dbo].[Project] ([ProjectID]),
	[IsBaselineCondition] [bit] NOT NULL,
	[TreatmentBMPID] [int] NULL CONSTRAINT [FK_ProjectNereidResult_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[WaterQualityManagementPlanID] [int] NULL CONSTRAINT [FK_ProjectNereidResult_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[RegionalSubbasinID] [int] NULL CONSTRAINT [FK_ProjectNereidResult_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID]),
	[DelineationID] [int] NULL CONSTRAINT [FK_ProjectNereidResult_Delineation_DelineationID] FOREIGN KEY REFERENCES [dbo].[Delineation] ([DelineationID]),
	[NodeID] [varchar](max) NULL,
	[FullResponse] [varchar](max) NULL,
	[LastUpdate] [datetime] NULL
)