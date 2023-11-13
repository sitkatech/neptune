CREATE TABLE [dbo].[OnlandVisualTrashAssessmentObservation](
	[OnlandVisualTrashAssessmentObservationID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessmentObservationID] PRIMARY KEY,
	[OnlandVisualTrashAssessmentID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentObservation_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessment] ([OnlandVisualTrashAssessmentID]),
	[LocationPoint] [geometry] NOT NULL CONSTRAINT [CK_LocationIsAPoint] CHECK  (([LocationPoint].[STGeometryType]()='Point')),
	[Note] [varchar](500) NULL,
	[ObservationDatetime] [datetime] NOT NULL,
	[LocationPoint4326] [geometry] NULL,
)	
GO

create spatial index [SPATIAL_OnlandVisualTrashAssessmentObservation_LocationPoint] on [dbo].[OnlandVisualTrashAssessmentObservation]
(
	[LocationPoint]
)
with (BOUNDING_BOX=(1.83482e+006, 645096, 1.87056e+006, 690276))
GO

create spatial index [SPATIAL_OnlandVisualTrashAssessmentObservation_LocationPoint4326] on [dbo].[OnlandVisualTrashAssessmentObservation]
(
	[LocationPoint4326]
)
with (BOUNDING_BOX=(-119, 33, -117, 34))
