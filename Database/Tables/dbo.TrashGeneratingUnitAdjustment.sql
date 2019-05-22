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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnitAdjustment_Delineation_AdjustedDelineationID_DelineationID] FOREIGN KEY([AdjustedDelineationID])
REFERENCES [dbo].[Delineation] ([DelineationID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment] CHECK CONSTRAINT [FK_TrashGeneratingUnitAdjustment_Delineation_AdjustedDelineationID_DelineationID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnitAdjustment_OnlandVisualTrashAssessmentArea_AdjustedOnlandVisualTrashAssessmentAreaID_OnlandVisualTrashAsse] FOREIGN KEY([AdjustedOnlandVisualTrashAssessmentAreaID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment] CHECK CONSTRAINT [FK_TrashGeneratingUnitAdjustment_OnlandVisualTrashAssessmentArea_AdjustedOnlandVisualTrashAssessmentAreaID_OnlandVisualTrashAsse]
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment]  WITH CHECK ADD  CONSTRAINT [FK_TrashGeneratingUnitAdjustment_Person_AdjustedByPersonID_PersonID] FOREIGN KEY([AdjustedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment] CHECK CONSTRAINT [FK_TrashGeneratingUnitAdjustment_Person_AdjustedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment]  WITH CHECK ADD  CONSTRAINT [CK_TrashGeneratingUnitAdjustment_ExclusiveOrDelineationAssessmentDeletedGeometry] CHECK  (([AdjustedDelineationID] IS NOT NULL AND [AdjustedOnlandVisualTrashAssessmentAreaID] IS NULL AND [DeletedGeometry] IS NULL OR [AdjustedDelineationID] IS NULL AND [AdjustedOnlandVisualTrashAssessmentAreaID] IS NOT NULL AND [DeletedGeometry] IS NULL OR [AdjustedDelineationID] IS NULL AND [AdjustedOnlandVisualTrashAssessmentAreaID] IS NULL AND [DeletedGeometry] IS NOT NULL))
GO
ALTER TABLE [dbo].[TrashGeneratingUnitAdjustment] CHECK CONSTRAINT [CK_TrashGeneratingUnitAdjustment_ExclusiveOrDelineationAssessmentDeletedGeometry]