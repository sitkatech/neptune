CREATE TABLE [dbo].[TrashGeneratingUnitAdjustment](
	[TrashGeneratingUnitAdjustmentID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TrashGeneratingUnitAdjustment_TrashGeneratingUnitAdjustmentID] PRIMARY KEY,
	[AdjustedDelineationID] [int] NULL,
	[AdjustedOnlandVisualTrashAssessmentAreaID] [int] NULL,
	[DeletedGeometry] [geometry] NULL,
	[AdjustmentDate] [datetime] NOT NULL,
	[AdjustedByPersonID] [int] NOT NULL CONSTRAINT [FK_TrashGeneratingUnitAdjustment_Person_AdjustedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[IsProcessed] [bit] NOT NULL,
	[ProcessedDate] [datetime] NULL,
	CONSTRAINT [CK_TrashGeneratingUnitAdjustment_ExclusiveOrDelineationAssessmentDeletedGeometry] CHECK  (([AdjustedDelineationID] IS NOT NULL AND [AdjustedOnlandVisualTrashAssessmentAreaID] IS NULL AND [DeletedGeometry] IS NULL OR [AdjustedDelineationID] IS NULL AND [AdjustedOnlandVisualTrashAssessmentAreaID] IS NOT NULL AND [DeletedGeometry] IS NULL OR [AdjustedDelineationID] IS NULL AND [AdjustedOnlandVisualTrashAssessmentAreaID] IS NULL AND [DeletedGeometry] IS NOT NULL)),
)
GO

CREATE SPATIAL INDEX [SPATIAL_TrashGeneratingUnitAdjustment_DeletedGeometry] ON [dbo].[TrashGeneratingUnitAdjustment]
(
	[DeletedGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-118, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)