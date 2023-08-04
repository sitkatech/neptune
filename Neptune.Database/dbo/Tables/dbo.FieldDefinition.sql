CREATE TABLE [dbo].[FieldDefinition](
	[FieldDefinitionID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_FieldDefinition_FieldDefinitionID] PRIMARY KEY,
	[FieldDefinitionTypeID] [int] NOT NULL CONSTRAINT [FK_FieldDefinition_FieldDefinitionType_FieldDefinitionTypeID] FOREIGN KEY REFERENCES [dbo].[FieldDefinitionType] ([FieldDefinitionTypeID]),
	[FieldDefinitionValue] [dbo].[html] NULL,
)