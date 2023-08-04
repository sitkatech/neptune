CREATE TABLE [dbo].[DirtyModelNode](
	[DirtyModelNodeID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_DirtyModelNode_DirtyModelNodeID] PRIMARY KEY,
	[TreatmentBMPID] [int] NULL CONSTRAINT [FK_DirtyModelNode_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[WaterQualityManagementPlanID] [int] NULL CONSTRAINT [FK_DirtyModelNode_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID]),
	[RegionalSubbasinID] [int] NULL CONSTRAINT [FK_DirtyModelNode_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID]),
	[DelineationID] [int] NULL CONSTRAINT [FK_DirtyModelNode_Delineation_DelineationID] FOREIGN KEY REFERENCES [dbo].[Delineation] ([DelineationID]),
	[CreateDate] [datetime] NOT NULL,
)