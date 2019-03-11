SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceControlBMPAttribute](
	[SourceControlBMPAttributeID] [int] NOT NULL,
	[SourceControlBMPAttributeCategoryID] [int] NOT NULL,
	[SourceControlBMPAttributeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_SourceControlBMPAttribute_SourceControlBMPAttributeID] PRIMARY KEY CLUSTERED 
(
	[SourceControlBMPAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SourceControlBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_SourceControlBMPAttribute_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryID] FOREIGN KEY([SourceControlBMPAttributeCategoryID])
REFERENCES [dbo].[SourceControlBMPAttributeCategory] ([SourceControlBMPAttributeCategoryID])
GO
ALTER TABLE [dbo].[SourceControlBMPAttribute] CHECK CONSTRAINT [FK_SourceControlBMPAttribute_SourceControlBMPAttributeCategory_SourceControlBMPAttributeCategoryID]