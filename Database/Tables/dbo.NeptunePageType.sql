SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NeptunePageType](
	[NeptunePageTypeID] [int] NOT NULL,
	[NeptunePageTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NeptunePageTypeDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[NeptunePageRenderTypeID] [int] NOT NULL,
 CONSTRAINT [PK_NeptunePageType_NeptunePageTypeID] PRIMARY KEY CLUSTERED 
(
	[NeptunePageTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_NeptunePageType_NeptunePageTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[NeptunePageTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_NeptunePageType_NeptunePageTypeName] UNIQUE NONCLUSTERED 
(
	[NeptunePageTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[NeptunePageType]  WITH CHECK ADD  CONSTRAINT [FK_NeptunePageType_NeptunePageRenderType_NeptunePageRenderTypeID] FOREIGN KEY([NeptunePageRenderTypeID])
REFERENCES [dbo].[NeptunePageRenderType] ([NeptunePageRenderTypeID])
GO
ALTER TABLE [dbo].[NeptunePageType] CHECK CONSTRAINT [FK_NeptunePageType_NeptunePageRenderType_NeptunePageRenderTypeID]