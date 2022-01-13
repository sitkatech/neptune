SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StormwaterBreadCrumbEntity](
	[StormwaterBreadCrumbEntityID] [int] NOT NULL,
	[StormwaterBreadCrumbEntityName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StormwaterBreadCrumbEntityDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GlyphIconClass] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ColorClass] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_StormwaterBreadCrumbEntity_StormwaterBreadCrumbEntityID] PRIMARY KEY CLUSTERED 
(
	[StormwaterBreadCrumbEntityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
