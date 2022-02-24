SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectDocument](
	[ProjectDocumentID] [int] IDENTITY(1,1) NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[DisplayName] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[UploadDate] [date] NOT NULL,
	[DocumentDescription] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_ProjectDocument_ProjectDocumentID] PRIMARY KEY CLUSTERED 
(
	[ProjectDocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectDocument]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDocument_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[ProjectDocument] CHECK CONSTRAINT [FK_ProjectDocument_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[ProjectDocument]  WITH CHECK ADD  CONSTRAINT [FK_ProjectDocument_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectDocument] CHECK CONSTRAINT [FK_ProjectDocument_Project_ProjectID]