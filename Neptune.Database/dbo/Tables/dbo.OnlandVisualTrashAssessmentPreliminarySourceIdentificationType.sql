CREATE TABLE [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType](
	[OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[OnlandVisualTrashAssessmentID] [int] NOT NULL,
	[PreliminarySourceIdentificationTypeID] [int] NOT NULL,
	[ExplanationIfTypeIsOther] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType] PRIMARY KEY CLUSTERED 
(
	[OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessmentID_PreliminarySourceIdentificationT] UNIQUE NONCLUSTERED 
(
	[OnlandVisualTrashAssessmentID] ASC,
	[PreliminarySourceIdentificationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] FOREIGN KEY([OnlandVisualTrashAssessmentID])
REFERENCES [dbo].[OnlandVisualTrashAssessment] ([OnlandVisualTrashAssessmentID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]  WITH CHECK ADD  CONSTRAINT [FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_PreliminarySourceIdentificationType_PreliminarySourceIdentific] FOREIGN KEY([PreliminarySourceIdentificationTypeID])
REFERENCES [dbo].[PreliminarySourceIdentificationType] ([PreliminarySourceIdentificationTypeID])
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType] CHECK CONSTRAINT [FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_PreliminarySourceIdentificationType_PreliminarySourceIdentific]
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType]  WITH CHECK ADD  CONSTRAINT [CK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_ExplanationNotNullIfAndOnlyIfTypeIsOther] CHECK  ((([ExplanationIfTypeIsOther] IS NULL OR ([PreliminarySourceIdentificationTypeID]=(4) OR [PreliminarySourceIdentificationTypeID]=(7) OR [PreliminarySourceIdentificationTypeID]=(13) OR [PreliminarySourceIdentificationTypeID]=(16))) AND NOT ([ExplanationIfTypeIsOther] IS NULL AND ([PreliminarySourceIdentificationTypeID]=(4) OR [PreliminarySourceIdentificationTypeID]=(7) OR [PreliminarySourceIdentificationTypeID]=(13) OR [PreliminarySourceIdentificationTypeID]=(16)))))
GO
ALTER TABLE [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType] CHECK CONSTRAINT [CK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_ExplanationNotNullIfAndOnlyIfTypeIsOther]