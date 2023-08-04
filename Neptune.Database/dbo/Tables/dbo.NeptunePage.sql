CREATE TABLE [dbo].[NeptunePage](
	[NeptunePageID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_NeptunePage_NeptunePageID] PRIMARY KEY,
	[NeptunePageTypeID] [int] NOT NULL CONSTRAINT [FK_NeptunePage_NeptunePageType_NeptunePageTypeID] FOREIGN KEY REFERENCES [dbo].[NeptunePageType] ([NeptunePageTypeID]),
	[NeptunePageContent] [dbo].[html] NULL
)