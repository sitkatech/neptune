SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlannedProjectLoadGeneratingUnit](
	[PlannedProjectLoadGeneratingUnitID] [int] IDENTITY(1,1) NOT NULL,
	[PlannedProjectLoadGeneratingUnitGeometry] [geometry] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[ModelBasinID] [int] NULL,
	[RegionalSubbasinID] [int] NULL,
	[DelineationID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
	[IsEmptyResponseFromHRUService] [bit] NULL,
 CONSTRAINT [PK_PlannedProjectLoadGeneratingUnit_PlannedProjectLoadGeneratingUnitID] PRIMARY KEY CLUSTERED 
(
	[PlannedProjectLoadGeneratingUnitID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_Delineation_DelineationID] FOREIGN KEY([DelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit] CHECK CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_Delineation_DelineationID]
GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_ModelBasin_ModelBasinID] FOREIGN KEY([ModelBasinID])
REFERENCES [dbo].[ModelBasin] ([ModelBasinID])
GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit] CHECK CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_ModelBasin_ModelBasinID]
GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit] CHECK CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_Project_ProjectID]
GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID] FOREIGN KEY([RegionalSubbasinID])
REFERENCES [dbo].[RegionalSubbasin] ([RegionalSubbasinID])
GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit] CHECK CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_RegionalSubbasin_RegionalSubbasinID]
GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit]  WITH CHECK ADD  CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[PlannedProjectLoadGeneratingUnit] CHECK CONSTRAINT [FK_PlannedProjectLoadGeneratingUnit_WaterQualityManagementPlan_WaterQualityManagementPlanID]