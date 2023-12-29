CREATE TABLE [dbo].[SourceControlBMPAttribute](
	[SourceControlBMPAttributeID] [int] NOT NULL CONSTRAINT [PK_SourceControlBMPAttribute_SourceControlBMPAttributeID] PRIMARY KEY,
	[SourceControlBMPAttributeCategoryID] [int] NOT NULL CONSTRAINT [FK_SourceControlBMPAttribute_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryID] FOREIGN KEY REFERENCES [dbo].[SourceControlBMPAttributeCategory] ([SourceControlBMPAttributeCategoryID]),
	[SourceControlBMPAttributeName] [varchar](100)
)