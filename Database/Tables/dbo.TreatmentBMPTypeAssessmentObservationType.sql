SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType](
	[TreatmentBMPTypeAssessmentObservationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[ObservationTypeID] [int] NOT NULL,
	[AssessmentScoreWeight] [decimal](9, 6) NULL,
	[DefaultThresholdValue] [float] NULL,
	[DefaultBenchmarkValue] [float] NULL,
	[OverrideAssessmentScoreIfFailing] [bit] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPTypeAssessmentObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTypeID_ObservationTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeAssessmentObservationTypeID] ASC,
	[TreatmentBMPTypeID] ASC,
	[ObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeID_ObservationTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeID] ASC,
	[ObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_ObservationType_ObservationTypeID] FOREIGN KEY([ObservationTypeID])
REFERENCES [dbo].[ObservationType] ([ObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_ObservationType_ObservationTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_ObservationType_ObservationTypeID_TenantID] FOREIGN KEY([ObservationTypeID], [TenantID])
REFERENCES [dbo].[ObservationType] ([ObservationTypeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_ObservationType_ObservationTypeID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID_TenantID] FOREIGN KEY([TreatmentBMPTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPType_TreatmentBMPTypeID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [CK_OverrideNotNullIfAssessmentScoreWeightNull] CHECK  (([AssessmentScoreWeight] IS NULL AND [OverrideAssessmentScoreIfFailing] IS NOT NULL OR [AssessmentScoreWeight] IS NOT NULL AND [OverrideAssessmentScoreIfFailing] IS NOT NULL))
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAssessmentObservationType] CHECK CONSTRAINT [CK_OverrideNotNullIfAssessmentScoreWeightNull]