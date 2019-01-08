SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FieldDefinitionData](
	[FieldDefinitionDataID] [int] IDENTITY(1,1) NOT NULL,
	[FieldDefinitionID] [int] NOT NULL,
	[FieldDefinitionDataValue] [dbo].[html] NULL,
	[FieldDefinitionLabel] [varchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_FieldDefinitionData_FieldDefinitionDataID] PRIMARY KEY CLUSTERED 
(
	[FieldDefinitionDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[FieldDefinitionData]  WITH CHECK ADD  CONSTRAINT [FK_FieldDefinitionData_FieldDefinition_FieldDefinitionID] FOREIGN KEY([FieldDefinitionID])
REFERENCES [dbo].[FieldDefinition] ([FieldDefinitionID])
GO
ALTER TABLE [dbo].[FieldDefinitionData] CHECK CONSTRAINT [FK_FieldDefinitionData_FieldDefinition_FieldDefinitionID]