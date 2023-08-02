CREATE TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold](
	[TreatmentBMPBenchmarkAndThresholdID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[TreatmentBMPTypeAssessmentObservationTypeID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[TreatmentBMPAssessmentObservationTypeID] [int] NOT NULL,
	[BenchmarkValue] [float] NOT NULL,
	[ThresholdValue] [float] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPBenchmarkAndThresholdID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPBenchmarkAndThresholdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPID_TreatmentBMPAssessmentObservationTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC,
	[TreatmentBMPAssessmentObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID] FOREIGN KEY([TreatmentBMPAssessmentObservationTypeID])
REFERENCES [dbo].[TreatmentBMPAssessmentObservationType] ([TreatmentBMPAssessmentObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID] FOREIGN KEY([TreatmentBMPTypeAssessmentObservationTypeID])
REFERENCES [dbo].[TreatmentBMPTypeAssessmentObservationType] ([TreatmentBMPTypeAssessmentObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_Treat] FOREIGN KEY([TreatmentBMPTypeAssessmentObservationTypeID], [TreatmentBMPTypeID], [TreatmentBMPAssessmentObservationTypeID])
REFERENCES [dbo].[TreatmentBMPTypeAssessmentObservationType] ([TreatmentBMPTypeAssessmentObservationTypeID], [TreatmentBMPTypeID], [TreatmentBMPAssessmentObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_Treat]