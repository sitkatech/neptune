CREATE TABLE [dbo].[OnlandVisualTrashAssessmentObservation](
	[OnlandVisualTrashAssessmentObservationID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessmentObservationID] PRIMARY KEY,
	[OnlandVisualTrashAssessmentID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessment] ([OnlandVisualTrashAssessmentID]),
	[LocationPoint] [geometry] NOT NULL CONSTRAINT [CK_LocationIsAPoint] CHECK  (([LocationPoint].[STGeometryType]()='Point')),
	[Note] [varchar](500) NULL,
	[ObservationDatetime] [datetime] NOT NULL,
	[LocationPoint4326] [geometry] NULL,
)	
GO

CREATE SPATIAL INDEX [SPATIAL_OnlandVisualTrashAssessmentObservation_LocationPoint] ON [dbo].[OnlandVisualTrashAssessmentObservation]
(
	[LocationPoint]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-118, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)