CREATE TABLE [dbo].[TreatmentBMPLifespanType](
	[TreatmentBMPLifespanTypeID] [int] NOT NULL CONSTRAINT [PK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeID] PRIMARY KEY,
	[TreatmentBMPLifespanTypeName] [varchar](100) CONSTRAINT [AK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeName] UNIQUE,
	[TreatmentBMPLifespanTypeDisplayName] [varchar](100) CONSTRAINT [AK_TreatmentBMPLifespanType_TreatmentBMPLifespanTypeDisplayName] UNIQUE
)
