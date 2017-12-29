SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPObservation](
	[TreatmentBMPObservationID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPAssessmentID] [int] NOT NULL,
	[ObservationTypeID] [int] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPObservation_TreatmentBMPObservationID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPObservationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPObservation_TreatmentBMPObservationID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPObservationID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPObservation]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPObservation_ObservationType_ObservationTypeID] FOREIGN KEY([ObservationTypeID])
REFERENCES [dbo].[ObservationType] ([ObservationTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPObservation] CHECK CONSTRAINT [FK_TreatmentBMPObservation_ObservationType_ObservationTypeID]
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