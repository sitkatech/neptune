CREATE TABLE [dbo].[FieldDefinitionType](
	[FieldDefinitionTypeID] [int] NOT NULL CONSTRAINT [PK_FieldDefinitionType_FieldDefinitionTypeID] PRIMARY KEY,
	[FieldDefinitionTypeName] [varchar](300) CONSTRAINT [AK_FieldDefinitionType_FieldDefinitionTypeName] UNIQUE,
	[FieldDefinitionTypeDisplayName] [varchar](300) CONSTRAINT [AK_FieldDefinitionType_FieldDefinitionTypeDisplayName] UNIQUE
)
