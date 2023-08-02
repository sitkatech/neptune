CREATE TABLE [dbo].[TreatmentBMPAssessment](
	[TreatmentBMPAssessmentID] [int] IDENTITY(1,1) NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[FieldVisitID] [int] NOT NULL,
	[TreatmentBMPAssessmentTypeID] [int] NOT NULL,
	[Notes] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AssessmentScore] [float] NULL,
	[IsAssessmentComplete] [bit] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPAssessment_TreatmentBMPAssessmentID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPAssessmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAssessmentID] ASC,
	[TreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAssessmentID] ASC,
	[TreatmentBMPTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_FieldVisit_FieldVisitID] FOREIGN KEY([FieldVisitID])
REFERENCES [dbo].[FieldVisit] ([FieldVisitID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_FieldVisit_FieldVisitID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_FieldVisit_FieldVisitID_TreatmentBMPID] FOREIGN KEY([FieldVisitID], [TreatmentBMPID])
REFERENCES [dbo].[FieldVisit] ([FieldVisitID], [TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_FieldVisit_FieldVisitID_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeID] FOREIGN KEY([TreatmentBMPAssessmentTypeID])
REFERENCES [dbo].[TreatmentBMPAssessmentType] ([TreatmentBMPAssessmentTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAssessment] CHECK CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMPType_TreatmentBMPTypeID]