SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrashGeneratingUnit4326](
	[TrashGeneratingUnit4326ID] [int] IDENTITY(1,1) NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[OnlandVisualTrashAssessmentAreaID] [int] NULL,
	[LandUseBlockID] [int] NULL,
	[TrashGeneratingUnit4326Geometry] [geometry] NOT NULL,
	[LastUpdateDate] [datetime] NULL,
	[DelineationID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
 CONSTRAINT [PK_TrashGeneratingUnit4326_TrashGeneratingUnit4326ID] PRIMARY KEY CLUSTERED 
(
	[TrashGeneratingUnit4326ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326] ADD  CONSTRAINT [DF_TrashGeneratingUnit_4326_LastUpdateDate]  DEFAULT (getdate()) FOR [LastUpdateDate]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_Delineation_DelineationID] FOREIGN KEY([DelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326] CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_Delineation_DelineationID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_LandUseBlock_LandUseBlockID] FOREIGN KEY([LandUseBlockID])
REFERENCES [dbo].[LandUseBlock] ([LandUseBlockID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326] CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_LandUseBlock_LandUseBlockID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] FOREIGN KEY([OnlandVisualTrashAssessmentAreaID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326] CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326] CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnit4326_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnit4326] CHECK CONSTRAINT [FK_TrashGeneratingUnit4326_WaterQualityManagementPlan_WaterQualityManagementPlanID]