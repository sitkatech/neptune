CREATE TABLE [dbo].[TreatmentBMPAssessment](
	[TreatmentBMPAssessmentID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPAssessment_TreatmentBMPAssessmentID] PRIMARY KEY,
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[TreatmentBMPTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID]),
	[FieldVisitID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPAssessment_FieldVisit_FieldVisitID] FOREIGN KEY REFERENCES [dbo].[FieldVisit] ([FieldVisitID]),
	[TreatmentBMPAssessmentTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPAssessmentType] ([TreatmentBMPAssessmentTypeID]),
	[Notes] [varchar](1000) NULL,
	[AssessmentScore] [float] NULL,
	[IsAssessmentComplete] [bit] NOT NULL,
	CONSTRAINT [AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPID] UNIQUE ([TreatmentBMPAssessmentID], [TreatmentBMPID]),
	CONSTRAINT [AK_TreatmentBMPAssessment_TreatmentBMPAssessmentID_TreatmentBMPTypeID] UNIQUE ([TreatmentBMPAssessmentID], [TreatmentBMPTypeID]),
	CONSTRAINT [FK_TreatmentBMPAssessment_FieldVisit_FieldVisitID_TreatmentBMPID] FOREIGN KEY([FieldVisitID], [TreatmentBMPID]) REFERENCES [dbo].[FieldVisit] ([FieldVisitID], [TreatmentBMPID]),
	CONSTRAINT [FK_TreatmentBMPAssessment_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID]) REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID])
)