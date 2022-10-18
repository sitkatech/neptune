SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectNereidResult](
	[ProjectNereidResultID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[IsBaselineCondition] [bit] NOT NULL,
	[TreatmentBMPID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
	[RegionalSubbasinID] [int] NULL,
	[DelineationID] [int] NULL,
	[NodeID] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FullResponse] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastUpdate] [datetime] NULL,
 CONSTRAINT [PK_ProjectNereidResult_ProjectNereidResultID] PRIMARY KEY CLUSTERED 
(
	[ProjectNereidResultID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectNereidResult]  WITH CHECK ADD  CONSTRAINT [FK_ProjectNereidResult_Delineation_DelineationID] FOREIGN KEY([DelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO
ALTER TABLE [dbo].[ProjectNereidResult] CHECK CONSTRAINT [FK_ProjectNereidResult_Delineation_DelineationID]
GO
ALTER TABLE [dbo].[ProjectNereidResult]  WITH CHECK ADD  CONSTRAINT [FK_ProjectNereidResult_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectNereidResult] CHECK CONSTRAINT [FK_ProjectNereidResult_Project_ProjectID]
GO
ALTER TABLE [dbo].[ProjectNereidResult]  WITH CHECK ADD  CONSTRAINT [FK_ProjectNereidResult_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY([RegionalSubbasinID])
REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID])
GO
ALTER TABLE [dbo].[ProjectNereidResult] CHECK CONSTRAINT [FK_ProjectNereidResult_RegionalSubbasin_RegionalSubbasinID]
GO
ALTER TABLE [dbo].[ProjectNereidResult]  WITH CHECK ADD  CONSTRAINT [FK_ProjectNereidResult_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[ProjectNereidResult] CHECK CONSTRAINT [FK_ProjectNereidResult_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[ProjectNereidResult]  WITH CHECK ADD  CONSTRAINT [FK_ProjectNereidResult_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[ProjectNereidResult] CHECK CONSTRAINT [FK_ProjectNereidResult_WaterQualityManagementPlan_WaterQualityManagementPlanID]