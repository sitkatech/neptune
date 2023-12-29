CREATE TABLE [dbo].[OnlandVisualTrashAssessmentPreliminarySourceIdentificationType](
	[OnlandVisualTrashAssessmentPreliminarySourceIdentificationTypeID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType] PRIMARY KEY,
	[OnlandVisualTrashAssessmentID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessment_OnlandVisualTrashAssessmentID] FOREIGN KEY REFERENCES [dbo].[OnlandVisualTrashAssessment] ([OnlandVisualTrashAssessmentID]),
	[PreliminarySourceIdentificationTypeID] [int] NOT NULL CONSTRAINT [FK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_PreliminarySourceIdentificationType_PreliminarySourceIdentific] FOREIGN KEY REFERENCES [dbo].[PreliminarySourceIdentificationType] ([PreliminarySourceIdentificationTypeID]),
	[ExplanationIfTypeIsOther] [varchar](500) NULL,
	CONSTRAINT [AK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_OnlandVisualTrashAssessmentID_PreliminarySourceIdentificationT] UNIQUE([OnlandVisualTrashAssessmentID], [PreliminarySourceIdentificationTypeID]),
	CONSTRAINT [CK_OnlandVisualTrashAssessmentPreliminarySourceIdentificationType_ExplanationNotNullIfAndOnlyIfTypeIsOther] CHECK  ((([ExplanationIfTypeIsOther] IS NULL OR ([PreliminarySourceIdentificationTypeID]=(4) OR [PreliminarySourceIdentificationTypeID]=(7) OR [PreliminarySourceIdentificationTypeID]=(13) OR [PreliminarySourceIdentificationTypeID]=(16))) AND NOT ([ExplanationIfTypeIsOther] IS NULL AND ([PreliminarySourceIdentificationTypeID]=(4) OR [PreliminarySourceIdentificationTypeID]=(7) OR [PreliminarySourceIdentificationTypeID]=(13) OR [PreliminarySourceIdentificationTypeID]=(16)))))
)