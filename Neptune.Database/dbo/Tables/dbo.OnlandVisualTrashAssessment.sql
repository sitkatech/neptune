CREATE TABLE [dbo].[OnlandVisualTrashAssessment](
	[OnlandVisualTrashAssessmentID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] PRIMARY KEY,
	[CreatedByPersonID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[CreatedDate] [datetime] NOT NULL,
	[OnlandVisualTrashAssessmentAreaID] [int] NULL CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID]),
	[Notes] [varchar](500) NULL,
	[StormwaterJurisdictionID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessment_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID]),
	[AssessingNewArea] [bit] NULL,
	[OnlandVisualTrashAssessmentStatusID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusID] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessmentStatus] ([OnlandVisualTrashAssessmentStatusID]),
	[DraftGeometry] [geometry] NULL,
	[IsDraftGeometryManuallyRefined] [bit] NULL,
	[OnlandVisualTrashAssessmentScoreID] [int] NULL CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreID] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessmentScore] ([OnlandVisualTrashAssessmentScoreID]),
	[CompletedDate] [datetime] NULL,
	[DraftAreaName] [varchar](100) NULL,
	[DraftAreaDescription] [varchar](500) NULL,
	[IsTransectBackingAssessment] [bit] NOT NULL,
	[IsProgressAssessment] [bit] NOT NULL,
	--CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID_StormwaterJurisdictionID] FOREIGN KEY([OnlandVisualTrashAssessmentAreaID], [StormwaterJurisdictionID]) REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID], [StormwaterJurisdictionID]),
	CONSTRAINT [CK_OnlandVisualTrashAssessment_AssessmentCannotHaveDraftGeometryAndOfficialArea] CHECK  ((NOT ([DraftGeometry] IS NOT NULL AND [OnlandVisualTrashAssessmentAreaID] IS NOT NULL))),
	CONSTRAINT [CK_OnlandVisualTrashAssessment_AssessmentCannotHaveDraftGeometryWhenComplete] CHECK  ((NOT ([DraftGeometry] IS NOT NULL AND [OnlandVisualTrashAssessmentStatusID]=(2)))),
	CONSTRAINT [CK_OnlandVisualTrashAssessment_CompletedAssessmentMustHaveCompletedDate] CHECK  ((NOT ([CompletedDate] IS NULL AND [OnlandVisualTrashAssessmentStatusID]=(2)))),
	CONSTRAINT [CK_OnlandVisualTrashAssessment_CompletedAssessmentMustHaveScore] CHECK  ((NOT ([OnlandVisualTrashAssessmentScoreID] IS NULL AND [OnlandVisualTrashAssessmentStatusID]=(2))))
)
GO

CREATE UNIQUE NONCLUSTERED INDEX [CK_OnlandVisualTrashAssessment_AtMostOneTransectBackingAssessmentPerArea] ON [dbo].[OnlandVisualTrashAssessment]
(
	[OnlandVisualTrashAssessmentAreaID] ASC
)
WHERE ([IsTransectBackingAssessment]=(1))
GO

CREATE SPATIAL INDEX [SPATIAL_OnlandVisualTrashAssessment_DraftGeometry] ON [dbo].[OnlandVisualTrashAssessment]
(
	[DraftGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-118, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)