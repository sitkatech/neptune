CREATE TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold](
	[TreatmentBMPBenchmarkAndThresholdID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPBenchmarkAndThresholdID] PRIMARY KEY,
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[TreatmentBMPTypeAssessmentObservationTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPTypeAssessmentObservationType] ([TreatmentBMPTypeAssessmentObservationTypeID]),
	[TreatmentBMPTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID]),
	[TreatmentBMPAssessmentObservationTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPAssessmentObservationType] ([TreatmentBMPAssessmentObservationTypeID]),
	[BenchmarkValue] [float] NOT NULL,
	[ThresholdValue] [float] NOT NULL,
	CONSTRAINT [AK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPID_TreatmentBMPAssessmentObservationTypeID] UNIQUE([TreatmentBMPID], [TreatmentBMPAssessmentObservationTypeID]),
	--CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID]) REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID]),
	--CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_Treat] FOREIGN KEY([TreatmentBMPTypeAssessmentObservationTypeID], [TreatmentBMPTypeID], [TreatmentBMPAssessmentObservationTypeID]) REFERENCES [dbo].[TreatmentBMPTypeAssessmentObservationType] ([TreatmentBMPTypeAssessmentObservationTypeID], [TreatmentBMPTypeID], [TreatmentBMPAssessmentObservationTypeID])
)