CREATE TABLE [dbo].[TrashGeneratingUnit4326](
	[TrashGeneratingUnit4326ID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TrashGeneratingUnit4326_TrashGeneratingUnit4326ID] PRIMARY KEY,
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_TrashGeneratingUnit4326_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[OnlandVisualTrashAssessmentAreaID] [int] NULL CONSTRAINT [FK_TrashGeneratingUnit4326_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID]),
	[LandUseBlockID] [int] NULL CONSTRAINT [FK_TrashGeneratingUnit4326_LandUseBlock_LandUseBlockID] FOREIGN KEY REFERENCES [dbo].[LandUseBlock] ([LandUseBlockID]),
	[TrashGeneratingUnit4326Geometry] [geometry] NOT NULL,
	[LastUpdateDate] [datetime] NULL CONSTRAINT [DF_TrashGeneratingUnit4326_LastUpdateDate]  DEFAULT (getdate()),
	[DelineationID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL CONSTRAINT [FK_TrashGeneratingUnit4326_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
)
GO

CREATE SPATIAL INDEX [SPATIAL_TrashGeneratingUnit4326_TrashGeneratingUnit4326Geometry] ON [dbo].[TrashGeneratingUnit4326]
(
	[TrashGeneratingUnit4326Geometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-119, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)