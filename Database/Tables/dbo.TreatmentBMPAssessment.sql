SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPAssessment](
	[TreatmentBMPAssessmentID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[AssessmentDate] [datetime] NOT NULL,
	[PersonID] [int] NOT NULL,
	[AlternateAssessmentScore] [float] NULL,
	[AlternateAssessmentRationale] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsPrivate] [bit] NOT NULL,
	[Notes] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_TreatmentBMPAssessment_TreatmentBMPAssessmentID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPAssessmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAssessmentID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAssessmentID] ASC,
	[TreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAssessmentID] ASC,
	[TreatmentBMPTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_Person_PersonID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_Person_PersonID_TenantID] FOREIGN KEY([PersonID], [TenantID])
REFERENCES [dbo].[Person] ([PersonID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_Person_PersonID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TenantID] FOREIGN KEY([TreatmentBMPID], [TenantID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [CK_TreatmentBMPAssessment_AlternateAssessmentScoreAndAlternateAssessmentRationaleBothOrNone] CHECK  (([AlternateAssessmentScore] IS NULL AND [AlternateAssessmentRationale] IS NULL OR [AlternateAssessmentScore] IS NOT NULL AND [AlternateAssessmentRationale] IS NOT NULL))
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [CK_TreatmentBMPAssessment_AlternateAssessmentScoreAndAlternateAssessmentRationaleBothOrNone]