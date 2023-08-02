CREATE TABLE [dbo].[FieldDefinition](
	[FieldDefinitionID] [int] IDENTITY(1,1) NOT NULL,
	[FieldDefinitionTypeID] [int] NOT NULL,
	[FieldDefinitionValue] [dbo].[html] NULL,
 CONSTRAINT [PK_FieldDefinition_FieldDefinitionID] PRIMARY KEY CLUSTERED 
(
	[FieldDefinitionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[FieldDefinition]  WITH CHECK ADD  CONSTRAINT [FK_FieldDefinition_FieldDefinitionType_FieldDefinitionTypeID] FOREIGN KEY([FieldDefinitionTypeID])
REFERENCES [dbo].[FieldDefinitionType] ([FieldDefinitionTypeID])
GO
ALTER TABLE [dbo].[FieldDefinition] CHECK CONSTRAINT [FK_FieldDefinition_FieldDefinitionType_FieldDefinitionTypeID]