CREATE TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType](
	[TreatmentBMPTypeAssessmentObservationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[TreatmentBMPAssessmentObservationTypeID] [int] NOT NULL,
	[AssessmentScoreWeight] [decimal](9, 6) NULL,
	[DefaultThresholdValue] [float] NULL,
	[DefaultBenchmarkValue] [float] NULL,
	[OverrideAssessmentScoreIfFailing] [bit] NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPTypeAssessmentObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTypeID_TreatmentBMPAssessme] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeAssessmentObservationTypeID] ASC,
	[TreatmentBMPTypeID] ASC,
	[TreatmentBMPAssessmentObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeID_TreatmentBMPAssessmentObservationTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeID] ASC,
	[TreatmentBMPAssessmentObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID] FOREIGN KEY([TreatmentBMPAssessmentObservationTypeID])
REFERENCES [dbo].[TreatmentBMPAssessmentObservationType] ([TreatmentBMPAssessmentObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [CK_OverrideNotNullIfAssessmentScoreWeightNull] CHECK  (([AssessmentScoreWeight] IS NULL AND [OverrideAssessmentScoreIfFailing] IS NOT NULL OR [AssessmentScoreWeight] IS NOT NULL AND [OverrideAssessmentScoreIfFailing] IS NOT NULL))
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType] CHECK CONSTRAINT [CK_OverrideNotNullIfAssessmentScoreWeightNull]