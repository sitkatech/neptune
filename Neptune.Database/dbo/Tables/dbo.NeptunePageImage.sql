CREATE TABLE [dbo].[NeptunePageImage](
	[NeptunePageImageID] [int] IDENTITY(1,1) NOT NULL,
	[NeptunePageID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL,
 CONSTRAINT [PK_NeptunePageImage_NeptunePageImageID] PRIMARY KEY CLUSTERED 
(
	[NeptunePageImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_NeptunePageImage_NeptunePageImageID_FileResourceID] UNIQUE NONCLUSTERED 
(
	[NeptunePageImageID] ASC,
	[FileResourceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[NeptunePageImage]  WITH CHECK ADD  CONSTRAINT [FK_NeptunePageImage_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[NeptunePageImage] CHECK CONSTRAINT [FK_NeptunePageImage_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[NeptunePageImage]  WITH CHECK ADD  CONSTRAINT [FK_NeptunePageImage_NeptunePage_NeptunePageID] FOREIGN KEY([NeptunePageID])
REFERENCES [dbo].[NeptunePage] ([NeptunePageID])
GO
ALTER TABLE [dbo].[NeptunePageImage] CHECK CONSTRAINT [FK_NeptunePageImage_NeptunePage_NeptunePageID]