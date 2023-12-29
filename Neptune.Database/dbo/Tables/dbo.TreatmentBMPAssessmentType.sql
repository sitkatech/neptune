CREATE TABLE [dbo].[TreatmentBMPAssessmentType](
	[TreatmentBMPAssessmentTypeID] [int] NOT NULL CONSTRAINT [PK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeID] PRIMARY KEY,
	[TreatmentBMPAssessmentTypeName] [varchar](100) CONSTRAINT [AK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeName] UNIQUE,
	[TreatmentBMPAssessmentTypeDisplayName] [varchar](100) CONSTRAINT [AK_TreatmentBMPAssessmentType_TreatmentBMPAssessmentTypeDisplayName] UNIQUE
)
