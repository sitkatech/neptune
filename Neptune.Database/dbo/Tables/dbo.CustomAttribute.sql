CREATE TABLE [dbo].[CustomAttribute](
	[CustomAttributeID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_CustomAttribute_CustomAttributeID] PRIMARY KEY,
	[TreatmentBMPID] [int] NOT NULL CONSTRAINT [FK_CustomAttribute_TreatmentBMP_TreatmentBMPID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID]),
	[TreatmentBMPTypeCustomAttributeTypeID] [int] NOT NULL CONSTRAINT [FK_CustomAttribute_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPTypeCustomAttributeType] ([TreatmentBMPTypeCustomAttributeTypeID]),
	[TreatmentBMPTypeID] [int] NOT NULL CONSTRAINT [FK_CustomAttribute_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID]),
	[CustomAttributeTypeID] [int] NOT NULL CONSTRAINT [FK_CustomAttribute_CustomAttributeType_CustomAttributeTypeID] FOREIGN KEY REFERENCES [dbo].[CustomAttributeType] ([CustomAttributeTypeID]),
	CONSTRAINT [AK_CustomAttribute_TreatmentBMPID_TreatmentBMPTypeID_CustomAttributeTypeID] UNIQUE (
		[TreatmentBMPID] ASC,
		[TreatmentBMPTypeID] ASC,
		[CustomAttributeTypeID] ASC
	),
	CONSTRAINT [FK_CustomAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID]) REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID]),
	CONSTRAINT [FK_CustomAttribute_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID] FOREIGN KEY([TreatmentBMPTypeID], [CustomAttributeTypeID]) REFERENCES [dbo].[TreatmentBMPTypeCustomAttributeType] ([TreatmentBMPTypeID], [CustomAttributeTypeID])
)