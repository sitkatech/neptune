CREATE TABLE [dbo].[TrashGeneratingUnit](
	[TrashGeneratingUnitID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TrashGeneratingUnit_TrashGeneratingUnitID] PRIMARY KEY,
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_TrashGeneratingUnit_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[OnlandVisualTrashAssessmentAreaID] [int] NULL,
	[LandUseBlockID] [int] NULL CONSTRAINT [FK_TrashGeneratingUnit_LandUseBlock_LandUseBlockID] FOREIGN KEY REFERENCES [dbo].[LandUseBlock] ([LandUseBlockID]),
	[TrashGeneratingUnitGeometry] [geometry] NOT NULL,
	[LastUpdateDate] [datetime] NULL CONSTRAINT [DF_TrashGeneratingUnit_LastUpdateDate]  DEFAULT (getdate()),
	[DelineationID] [int] NULL,
	[WaterQualityManagementPlanID] [int] NULL,
)
GO

CREATE SPATIAL INDEX [SPATIAL_TrashGeneratingUnit_TrashGeneratingUnitGeometry] ON [dbo].[TrashGeneratingUnit]
(
	[TrashGeneratingUnitGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(1.82653e+006, 636160, 1.89215e+006, 698756), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)