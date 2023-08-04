CREATE TABLE [dbo].[NeptuneHomePageImage](
	[NeptuneHomePageImageID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_NeptuneHomePageImage_NeptuneHomePageImageID] PRIMARY KEY,
	[FileResourceID] [int] NOT NULL CONSTRAINT [FK_NeptuneHomePageImage_FileResource_FileResourceID] FOREIGN KEY REFERENCES [dbo].[FileResource] ([FileResourceID]),
	[Caption] [varchar](300),
	[SortOrder] [int] NOT NULL
)