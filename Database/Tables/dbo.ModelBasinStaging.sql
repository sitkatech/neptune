SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModelBasinStaging](
	[ModelBasinStagingID] [int] IDENTITY(1,1) NOT NULL,
	[ModelBasinKey] [int] NOT NULL,
	[ModelBasinName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ModelBasinGeometry] [geometry] NOT NULL,
 CONSTRAINT [PK_ModelBasinStaging_ModelBasinStagingID] PRIMARY KEY CLUSTERED 
(
	[ModelBasinStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ModelBasinStaging_ModelBasinKey] UNIQUE NONCLUSTERED 
(
	[ModelBasinKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
