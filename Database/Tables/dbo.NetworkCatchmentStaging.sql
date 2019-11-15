SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetworkCatchmentStaging](
	[NetworkCatchmentStagingID] [int] IDENTITY(1,1) NOT NULL,
	[DrainID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Watershed] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CatchmentGeometry] [geometry] NULL,
	[OCSurveyCatchmentID] [int] NULL,
	[OCSurveyDownstreamCatchmentID] [int] NULL,
 CONSTRAINT [PK_NetworkCatchmentStaging_NetworkCatchmentStagingID] PRIMARY KEY CLUSTERED 
(
	[NetworkCatchmentStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
