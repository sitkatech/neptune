CREATE TABLE [dbo].[NeptunePageImage](
	[NeptunePageImageID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_NeptunePageImage_NeptunePageImageID] PRIMARY KEY,
	[NeptunePageID] [int] NOT NULL CONSTRAINT [FK_NeptunePageImage_NeptunePage_NeptunePageID] FOREIGN KEY REFERENCES [dbo].[NeptunePage] ([NeptunePageID]),
	[FileResourceID] [int] NOT NULL CONSTRAINT [FK_NeptunePageImage_FileResource_FileResourceID] FOREIGN KEY REFERENCES [dbo].[FileResource] ([FileResourceID]),
	CONSTRAINT [AK_NeptunePageImage_NeptunePageID_FileResourceID] UNIQUE ([NeptunePageID], [FileResourceID])
)