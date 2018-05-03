SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold](
	[TreatmentBMPBenchmarkAndThresholdID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[TreatmentBMPTypeAssessmentObservationTypeID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[ObservationTypeID] [int] NOT NULL,
	[BenchmarkValue] [float] NOT NULL,
	[ThresholdValue] [float] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPBenchmarkAndThresholdID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPBenchmarkAndThresholdID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPID_ObservationTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC,
	[ObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_ObservationType_ObservationTypeID] FOREIGN KEY([ObservationTypeID])
REFERENCES [dbo].[ObservationType] ([ObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_ObservationType_ObservationTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TenantID] FOREIGN KEY([TreatmentBMPID], [TenantID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID]
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
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_Treat] FOREIGN KEY([TreatmentBMPTypeAssessmentObservationTypeID], [TreatmentBMPTypeID], [ObservationTypeID])
REFERENCES [dbo].[TreatmentBMPTypeAssessmentObservationType] ([TreatmentBMPTypeAssessmentObservationTypeID], [TreatmentBMPTypeID], [ObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPBenchmarkAndThreshold] CHECK CONSTRAINT [FK_TreatmentBMPBenchmarkAndThreshold_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_Treat]