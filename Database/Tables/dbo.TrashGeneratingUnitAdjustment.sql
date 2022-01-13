SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrashGeneratingUnitAdjustment](
	[TrashGeneratingUnitAdjustmentID] [int] IDENTITY(1,1) NOT NULL,
	[AdjustedDelineationID] [int] NULL,
	[AdjustedOnlandVisualTrashAssessmentAreaID] [int] NULL,
	[DeletedGeometry] [geometry] NULL,
	[AdjustmentDate] [datetime] NOT NULL,
	[AdjustedByPersonID] [int] NOT NULL,
	[IsProcessed] [bit] NOT NULL,
	[ProcessedDate] [datetime] NULL,
 CONSTRAINT [PK_TrashGeneratingUnitAdjustment_TrashGeneratingUnitAdjustmentID] PRIMARY KEY CLUSTERED 
(
	[TrashGeneratingUnitAdjustmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnitAdjustment_Person_AdjustedByPersonID_PersonID] FOREIGN KEY([AdjustedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment] CHECK CONSTRAINT [FK_TrashGeneratingUnitAdjustment_Person_AdjustedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment]  WITH CHECK ADD  CONSTRAINT [CK_TrashGeneratingUnitAdjustment_ExclusiveOrDelineationAssessmentDeletedGeometry] CHECK  (([AdjustedDelineationID] IS NOT NULL AND [AdjustedOnlandVisualTrashAssessmentAreaID] IS NULL AND [DeletedGeometry] IS NULL OR [AdjustedDelineationID] IS NULL AND [AdjustedOnlandVisualTrashAssessmentAreaID] IS NOT NULL AND [DeletedGeometry] IS NULL OR [AdjustedDelineationID] IS NULL AND [AdjustedOnlandVisualTrashAssessmentAreaID] IS NULL AND [DeletedGeometry] IS NOT NULL))
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment] CHECK CONSTRAINT [CK_TrashGeneratingUnitAdjustment_ExclusiveOrDelineationAssessmentDeletedGeometry]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_TrashGeneratingUnitAdjustment_DeletedGeometry] ON [dbo].[TrashGeneratingUnitAdjustment]
(
	[DeletedGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-118, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]