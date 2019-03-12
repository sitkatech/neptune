SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetworkCatchment](
	[NetworkCatchmentID] [int] IDENTITY(1,1) NOT NULL,
	[OCSurveyCatchmentID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DownstreamCatchmentID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[DrainID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Watershed] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CatchmentGeometry] [geometry] NOT NULL,
 CONSTRAINT [PK_NetworkCatchment_NetworkCatchmentID] PRIMARY KEY CLUSTERED 
(
	[NetworkCatchmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_NetworkCatchment_OCSurveyCatchmentID_DrainID] UNIQUE NONCLUSTERED 
(
	[OCSurveyCatchmentID] ASC,
	[DrainID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
