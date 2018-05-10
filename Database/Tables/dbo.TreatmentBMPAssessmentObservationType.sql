SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPAssessmentObservationType](
	[TreatmentBMPAssessmentObservationTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPAssessmentObservationTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ObservationTypeSpecificationID] [int] NOT NULL,
	[TreatmentBMPAssessmentObservationTypeSchema] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPAssessmentObservationTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAssessmentObservationTypeID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeName] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAssessmentObservationTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessmentObservationType_ObservationTypeSpecification_ObservationTypeSpecificationID] FOREIGN KEY([ObservationTypeSpecificationID])
REFERENCES [dbo].[ObservationTypeSpecification] ([ObservationTypeSpecificationID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentObservationType] CHECK CONSTRAINT [FK_TreatmentBMPAssessmentObservationType_ObservationTypeSpecification_ObservationTypeSpecificationID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessmentObservationType_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessmentObservationType] CHECK CONSTRAINT [FK_TreatmentBMPAssessmentObservationType_Tenant_TenantID]