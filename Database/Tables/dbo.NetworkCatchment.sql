SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetworkCatchment](
	[NetworkCatchmentID] [int] IDENTITY(1,1) NOT NULL,
	[DrainID] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Watershed] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CatchmentGeometry] [geometry] NOT NULL,
	[OCSurveyCatchmentIDN] [int] NOT NULL,
	[OCSurveyDownstreamCatchmentIDN] [int] NULL,
 CONSTRAINT [PK_NetworkCatchment_NetworkCatchmentID] PRIMARY KEY CLUSTERED 
(
	[NetworkCatchmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_NetworkCatchment_OCSurveyCatchmentIDN] UNIQUE NONCLUSTERED 
(
	[OCSurveyCatchmentIDN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[NetworkCatchment]  WITH CHECK ADD  CONSTRAINT [FK_NetworkCatchment_NetworkCatchment_OCSurveyDownstreamCatchmentIDN_OCSurveyCatchmentIDN] FOREIGN KEY([OCSurveyDownstreamCatchmentIDN])
REFERENCES [dbo].[NetworkCatchment] ([OCSurveyCatchmentIDN])
GO
ALTER TABLE [dbo].[NetworkCatchment] CHECK CONSTRAINT [FK_NetworkCatchment_NetworkCatchment_OCSurveyDownstreamCatchmentIDN_OCSurveyCatchmentIDN]