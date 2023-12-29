CREATE TABLE [dbo].[SourceControlBMPAttributeCategory](
	[SourceControlBMPAttributeCategoryID] [int] NOT NULL CONSTRAINT [PK_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryID] PRIMARY KEY,
	[SourceControlBMPAttributeCategoryShortName] [varchar](50) CONSTRAINT [PK_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryShortName] UNIQUE,
	[SourceControlBMPAttributeCategoryName] [varchar](100) CONSTRAINT [PK_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryName] UNIQUE
)
