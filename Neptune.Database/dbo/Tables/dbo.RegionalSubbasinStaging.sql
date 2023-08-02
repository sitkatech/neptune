CREATE TABLE [dbo].[RegionalSubbasinStaging](
	[RegionalSubbasinStagingID] [int] IDENTITY(1,1) NOT NULL,
	[DrainID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Watershed] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CatchmentGeometry] [geometry] NULL,
	[OCSurveyCatchmentID] [int] NULL,
	[OCSurveyDownstreamCatchmentID] [int] NULL,
 CONSTRAINT [PK_RegionalSubbasinStaging_RegionalSubbasinStagingID] PRIMARY KEY CLUSTERED 
(
	[RegionalSubbasinStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
