CREATE TABLE [dbo].[RegionalSubbasinStaging](
	[RegionalSubbasinStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_RegionalSubbasinStaging_RegionalSubbasinStagingID] PRIMARY KEY,
	[DrainID] [varchar](10) NULL,
	[Watershed] [varchar](100) NULL,
	[CatchmentGeometry] [geometry] NULL,
	[OCSurveyCatchmentID] [int] NULL,
	[OCSurveyDownstreamCatchmentID] [int] NULL
)
