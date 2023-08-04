CREATE TABLE [dbo].[CustomAttributeDataType](
	[CustomAttributeDataTypeID] [int] NOT NULL CONSTRAINT [PK_CustomAttributeDataType_CustomAttributeDataTypeID] PRIMARY KEY,
	[CustomAttributeDataTypeName] [varchar](100) CONSTRAINT [AK_CustomAttributeDataType_CustomAttributeDataTypeName] UNIQUE,
	[CustomAttributeDataTypeDisplayName] [varchar](100) CONSTRAINT [AK_CustomAttributeDataType_CustomAttributeDataTypeDisplayName] UNIQUE
)
