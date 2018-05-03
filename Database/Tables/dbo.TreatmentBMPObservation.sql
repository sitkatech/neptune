SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPObservation](
	[TreatmentBMPObservationID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPAssessmentID] [int] NOT NULL,
	[TreatmentBMPTypeAssessmentObservationTypeID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[TreatmentBMPAssessmentObservationTypeID] [int] NOT NULL,
	[ObservationData] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TreatmentBMPObservation_TreatmentBMPObservationID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPObservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPObservation]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservation_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservation] CHECK CONSTRAINT [FK_TreatmentBMPObservation_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservation]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID] FOREIGN KEY([TreatmentBMPAssessmentID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservation] CHECK CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservation]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID] FOREIGN KEY([TreatmentBMPAssessmentID], [TenantID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservation] CHECK CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservation]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPAssessmentID], [TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPAssessment] ([TreatmentBMPAssessmentID], [TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservation] CHECK CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservation]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID] FOREIGN KEY([TreatmentBMPAssessmentObservationTypeID])
REFERENCES [dbo].[TreatmentBMPAssessmentObservationType] ([TreatmentBMPAssessmentObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservation] CHECK CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservation]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservation] CHECK CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservation]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID] FOREIGN KEY([TreatmentBMPTypeAssessmentObservationTypeID])
REFERENCES [dbo].[TreatmentBMPTypeAssessmentObservationType] ([TreatmentBMPTypeAssessmentObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservation] CHECK CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPObservation]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTyp] FOREIGN KEY([TreatmentBMPTypeAssessmentObservationTypeID], [TreatmentBMPTypeID], [TreatmentBMPAssessmentObservationTypeID])
REFERENCES [dbo].[TreatmentBMPTypeAssessmentObservationType] ([TreatmentBMPTypeAssessmentObservationTypeID], [TreatmentBMPTypeID], [TreatmentBMPAssessmentObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservation] CHECK CONSTRAINT [FK_TreatmentBMPObservation_TreatmentBMPTypeAssessmentObservationType_TreatmentBMPTypeAssessmentObservationTypeID_TreatmentBMPTyp]