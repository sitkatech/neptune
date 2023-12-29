CREATE TABLE [dbo].[TreatmentBMPModelingType](
	[TreatmentBMPModelingTypeID] [int] NOT NULL CONSTRAINT [PK_TreatmentBMPModelingType_TreatmentBMPModelingTypeID] PRIMARY KEY,
	[TreatmentBMPModelingTypeName] [varchar](100) CONSTRAINT [AK_TreatmentBMPModelingType_TreatmentBMPModelingTypeName] UNIQUE,
	[TreatmentBMPModelingTypeDisplayName] [varchar](100) CONSTRAINT [AK_TreatmentBMPModelingType_TreatmentBMPModelingTypeDisplayName] UNIQUE
)
