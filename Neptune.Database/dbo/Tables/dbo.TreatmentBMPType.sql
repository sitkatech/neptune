CREATE TABLE [dbo].[TreatmentBMPType](
	[TreatmentBMPTypeID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPType_TreatmentBMPTypeID] PRIMARY KEY,
	[TreatmentBMPTypeName] [varchar](100) CONSTRAINT [AK_TreatmentBMPType_TreatmentBMPTypeName] UNIQUE,
	[TreatmentBMPTypeDescription] [varchar](1000),
	[IsAnalyzedInModelingModule] [bit] NOT NULL,
	[TreatmentBMPModelingTypeID] [int] NULL CONSTRAINT [FK_TreatmentBMPType_TreatmentBMPModelingType_TreatmentBMPModelingTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPModelingType] ([TreatmentBMPModelingTypeID])
)