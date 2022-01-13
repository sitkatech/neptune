SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NeptunePage](
	[NeptunePageID] [int] IDENTITY(1,1) NOT NULL,
	[NeptunePageTypeID] [int] NOT NULL,
	[NeptunePageContent] [dbo].[html] NULL,
 CONSTRAINT [PK_NeptunePage_NeptunePageID] PRIMARY KEY CLUSTERED 
(
	[NeptunePageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[NeptunePage]  WITH CHECK ADD  CONSTRAINT [FK_NeptunePage_NeptunePageType_NeptunePageTypeID] FOREIGN KEY([NeptunePageTypeID])
REFERENCES [dbo].[NeptunePageType] ([NeptunePageTypeID])
GO
ALTER TABLE [dbo].[NeptunePage] CHECK CONSTRAINT [FK_NeptunePage_NeptunePageType_NeptunePageTypeID]