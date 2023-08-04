CREATE TABLE [dbo].[TreatmentBMPTypeCustomAttributeType](
	[TreatmentBMPTypeCustomAttributeTypeID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID] PRIMARY KEY,
	[TreatmentBMPTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID]),
	[CustomAttributeTypeID] [int] NOT NULL CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_CustomAttributeType_CustomAttributeTypeID] FOREIGN KEY REFERENCES [dbo].[CustomAttributeType] ([CustomAttributeTypeID]),
	[SortOrder] [int] NULL,
	CONSTRAINT [AK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID] UNIQUE ([TreatmentBMPTypeID], [CustomAttributeTypeID])
)