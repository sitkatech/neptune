CREATE TABLE [dbo].[CustomAttributeType](
	[CustomAttributeTypeID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_CustomAttributeType_CustomAttributeTypeID] PRIMARY KEY,
	[CustomAttributeTypeName] [varchar](100) CONSTRAINT [AK_CustomAttributeType_CustomAttributeTypeName] UNIQUE,
	[CustomAttributeDataTypeID] [int] NOT NULL CONSTRAINT [FK_CustomAttributeType_CustomAttributeDataType_CustomAttributeDataTypeID] FOREIGN KEY REFERENCES [dbo].[CustomAttributeDataType] ([CustomAttributeDataTypeID]),
	[MeasurementUnitTypeID] [int] NULL CONSTRAINT [FK_CustomAttributeType_MeasurementUnitType_MeasurementUnitTypeID] FOREIGN KEY REFERENCES [dbo].[MeasurementUnitType] ([MeasurementUnitTypeID]),
	[IsRequired] [bit] NOT NULL,
	[CustomAttributeTypeDescription] [varchar](200) NULL,
	[CustomAttributeTypePurposeID] [int] NOT NULL CONSTRAINT [FK_CustomAttributeType_CustomAttributeTypePurpose_CustomAttributeTypePurposeID] FOREIGN KEY REFERENCES [dbo].[CustomAttributeTypePurpose] ([CustomAttributeTypePurposeID]),
	[CustomAttributeTypeOptionsSchema] [varchar](max) NULL,
	CONSTRAINT [CK_CustomAttributeType_PickListTypeOptionSchemaNotNull] CHECK  ((NOT ([CustomAttributeDataTypeID]=(6) OR [CustomAttributeDataTypeID]=(5)) AND [CustomAttributeTypeOptionsSchema] IS NULL OR ([CustomAttributeDataTypeID]=(6) OR [CustomAttributeDataTypeID]=(5)) AND [CustomAttributeTypeOptionsSchema] IS NOT NULL))
)