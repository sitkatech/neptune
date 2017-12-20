SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPTypeObservationType](
	[TreatmentBMPTypeObservationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[ObservationTypeID] [int] NOT NULL,
	[AssessmentScoreWeight] [float] NOT NULL,
	[DefaultThresholdValue] [float] NULL,
	[DefaultBenchmarkValue] [float] NULL,
 CONSTRAINT [PK_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPTypeObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeObservationType_TreatmentBMPTypeObservationTypeID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeObservationTypeID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPTypeObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeObservationType_ObservationType_ObservationTypeID] FOREIGN KEY([ObservationTypeID])
REFERENCES [dbo].[ObservationType] ([ObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeObservationType] CHECK CONSTRAINT [FK_TreatmentBMPTypeObservationType_ObservationType_ObservationTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeObservationType_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeObservationType] CHECK CONSTRAINT [FK_TreatmentBMPTypeObservationType_TreatmentBMPType_TreatmentBMPTypeID]