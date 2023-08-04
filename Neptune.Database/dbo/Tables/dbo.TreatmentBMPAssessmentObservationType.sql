CREATE TABLE [dbo].[TreatmentBMPAssessmentObservationType](
	[TreatmentBMPAssessmentObservationTypeID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeID] PRIMARY KEY,
	[TreatmentBMPAssessmentObservationTypeName] [varchar](100) CONSTRAINT [AK_TreatmentBMPAssessmentObservationType_TreatmentBMPAssessmentObservationTypeName] UNIQUE,
	[ObservationTypeSpecificationID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPAssessmentObservationType_ObservationTypeSpecification_ObservationTypeSpecificationID] FOREIGN KEY REFERENCES [dbo].[ObservationTypeSpecification] ([ObservationTypeSpecificationID]),
	[TreatmentBMPAssessmentObservationTypeSchema] [varchar](max)
)