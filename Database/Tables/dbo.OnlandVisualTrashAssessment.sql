SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OnlandVisualTrashAssessment](
	[OnlandVisualTrashAssessmentID] [int] IDENTITY(1,1) NOT NULL,
	[CreatedByPersonID] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[OnlandVisualTrashAssessmentAreaID] [int] NULL,
	[Notes] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[AssessingNewArea] [bit] NULL,
	[OnlandVisualTrashAssessmentStatusID] [int] NOT NULL,
	[DraftGeometry] [geometry] NULL,
	[IsDraftGeometryManuallyRefined] [bit] NULL,
	[OnlandVisualTrashAssessmentScoreID] [int] NULL,
	[CompletedDate] [datetime] NULL,
	[DraftAreaName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DraftAreaDescription] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsTransectBackingAssessment] [bit] NOT NULL,
	[IsProgressAssessment] [bit] NOT NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
CREATE UNIQUE NONCLUSTERED INDEX [CK_OnlandVisualTrashAssessment_AtMostOneTransectBackingAssessmentPerArea] ON [dbo].[OnlandVisualTrashAssessment]
(
	[OnlandVisualTrashAssessmentAreaID] ASC
)
WHERE ([IsTransectBackingAssessment]=(1))
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID] FOREIGN KEY([OnlandVisualTrashAssessmentAreaID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID_StormwaterJurisdictionID] FOREIGN KEY([OnlandVisualTrashAssessmentAreaID], [StormwaterJurisdictionID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentArea] ([OnlandVisualTrashAssessmentAreaID], [StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentArea_OnlandVisualTrashAssessmentAreaID_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreID] FOREIGN KEY([OnlandVisualTrashAssessmentScoreID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentScore] ([OnlandVisualTrashAssessmentScoreID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentScore_OnlandVisualTrashAssessmentScoreID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusID] FOREIGN KEY([OnlandVisualTrashAssessmentStatusID])
REFERENCES [dbo].[OnlandVisualTrashAssessmentStatus] ([OnlandVisualTrashAssessmentStatusID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentStatus_OnlandVisualTrashAssessmentStatusID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID] FOREIGN KEY([CreatedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_Person_CreatedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessment_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessment_StormwaterJurisdiction_StormwaterJurisdictionID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [CK_OnlandVisualTrashAssessment_AssessmentCannotHaveDraftGeometryAndOfficialArea] CHECK  ((NOT ([DraftGeometry] IS NOT NULL AND [OnlandVisualTrashAssessmentAreaID] IS NOT NULL)))
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [CK_OnlandVisualTrashAssessment_AssessmentCannotHaveDraftGeometryAndOfficialArea]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [CK_OnlandVisualTrashAssessment_AssessmentCannotHaveDraftGeometryWhenComplete] CHECK  ((NOT ([DraftGeometry] IS NOT NULL AND [OnlandVisualTrashAssessmentStatusID]=(2))))
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [CK_OnlandVisualTrashAssessment_AssessmentCannotHaveDraftGeometryWhenComplete]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [CK_OnlandVisualTrashAssessment_CompletedAssessmentMustHaveCompletedDate] CHECK  ((NOT ([CompletedDate] IS NULL AND [OnlandVisualTrashAssessmentStatusID]=(2))))
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [CK_OnlandVisualTrashAssessment_CompletedAssessmentMustHaveCompletedDate]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment]  WITH CHECK ADD  CONSTRAINT [CK_OnlandVisualTrashAssessment_CompletedAssessmentMustHaveScore] CHECK  ((NOT ([OnlandVisualTrashAssessmentScoreID] IS NULL AND [OnlandVisualTrashAssessmentStatusID]=(2))))
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessment] CHECK CONSTRAINT [CK_OnlandVisualTrashAssessment_CompletedAssessmentMustHaveScore]
GO
SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO
CREATE SPATIAL INDEX [SPATIAL_OnlandVisualTrashAssessment_DraftGeometry] ON [dbo].[OnlandVisualTrashAssessment]
(
	[DraftGeometry]
)USING  GEOMETRY_AUTO_GRID 
WITH (BOUNDING_BOX =(-118, 33, -117, 34), 
CELLS_PER_OBJECT = 8, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]