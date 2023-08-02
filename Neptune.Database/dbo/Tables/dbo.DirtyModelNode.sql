CREATE TABLE [dbo].[DirtyModelNode](
	[DirtyModelNodeID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
	[RegionalSubbasinID] [int] NULL,
	[DelineationID] [int] NULL,
	[CreateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DirtyModelNode_DirtyModelNodeID] PRIMARY KEY CLUSTERED 
(
	[DirtyModelNodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[DirtyModelNode]  WITH CHECK ADD  CONSTRAINT [FK_DirtyModelNode_Delineation_DelineationID] FOREIGN KEY([DelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO
ALTER TABLE [dbo].[DirtyModelNode] CHECK CONSTRAINT [FK_DirtyModelNode_Delineation_DelineationID]
GO
ALTER TABLE [dbo].[DirtyModelNode]  WITH CHECK ADD  CONSTRAINT [FK_DirtyModelNode_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY([RegionalSubbasinID])
REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID])
GO
ALTER TABLE [dbo].[DirtyModelNode] CHECK CONSTRAINT [FK_DirtyModelNode_RegionalSubbasin_RegionalSubbasinID]
GO
ALTER TABLE [dbo].[DirtyModelNode]  WITH CHECK ADD  CONSTRAINT [FK_DirtyModelNode_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[DirtyModelNode] CHECK CONSTRAINT [FK_DirtyModelNode_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[DirtyModelNode]  WITH CHECK ADD  CONSTRAINT [FK_DirtyModelNode_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[DirtyModelNode] CHECK CONSTRAINT [FK_DirtyModelNode_WaterQualityManagementPlan_WaterQualityManagementPlanID]