CREATE TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType](
	[TreatmentBMPTypeAssessmentObservationTypeID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID] PRIMARY KEY,
	[TreatmentBMPTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID]),
	[TreatmentBMPAssessmentObservationTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPAssessmentObservationType] ([TreatmentBMPAssessmentObservationTypeID]),
	[AssessmentScoreWeight] [decimal](9, 6) NULL,
	[DefaultThresholdValue] [float] NULL,
	[DefaultBenchmarkValue] [float] NULL,
	[OverrideAssessmentScoreIfFailing] [bit] NOT NULL,
	[SortOrder] [int] NULL,
	CONSTRAINT [AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTypeID_TreatmentBMPAssessme] UNIQUE (
		[TreatmentBMPTypeAssessmentObservationTypeID] ASC,
		[TreatmentBMPTypeID] ASC,
		[TreatmentBMPAssessmentObservationTypeID] ASC
	),
	CONSTRAINT [AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeID_TreatmentBMPAssessmentObservationTypeID] UNIQUE ([TreatmentBMPTypeID], [TreatmentBMPAssessmentObservationTypeID]),
	CONSTRAINT [CK_TreatmentBMPTypeAssessmentObservationType_OverrideNotNullIfAssessmentScoreWeightNull] CHECK  (([AssessmentScoreWeight] IS NULL AND [OverrideAssessmentScoreIfFailing] IS NOT NULL OR [AssessmentScoreWeight] IS NOT NULL AND [OverrideAssessmentScoreIfFailing] IS NOT NULL))
)