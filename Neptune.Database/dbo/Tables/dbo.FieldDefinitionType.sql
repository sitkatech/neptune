CREATE TABLE [dbo].[FieldDefinitionType](
	[FieldDefinitionTypeID] [int] NOT NULL CONSTRAINT [PK_FieldDefinitionType_FieldDefinitionTypeID] PRIMARY KEY,
	[FieldDefinitionTypeName] [varchar](100) CONSTRAINT [AK_FieldDefinitionType_FieldDefinitionTypeName] UNIQUE,
	[FieldDefinitionTypeDisplayName] [varchar](100) CONSTRAINT [AK_FieldDefinitionType_FieldDefinitionTypeDisplayName] UNIQUE
)
