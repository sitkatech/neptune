SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PreliminarySourceIdentificationCategory](
	[PreliminarySourceIdentificationCategoryID] [int] NOT NULL,
	[PreliminarySourceIdentificationCategoryName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PreliminarySourceIdentificationCategoryDisplayName] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryID] PRIMARY KEY CLUSTERED 
(
	[PreliminarySourceIdentificationCategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryDisplayName] UNIQUE NONCLUSTERED 
(
	[PreliminarySourceIdentificationCategoryDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_PreliminarySourceIdentificationCategory_PreliminarySourceIdentificationCategoryName] UNIQUE NONCLUSTERED 
(
	[PreliminarySourceIdentificationCategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
