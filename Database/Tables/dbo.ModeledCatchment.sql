SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModeledCatchment](
	[ModeledCatchmentID] [int] IDENTITY(1,1) NOT NULL,
	[ModeledCatchmentName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[Notes] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModeledCatchmentGeometry] [geometry] NULL,
 CONSTRAINT [PK_ModeledCatchment_ModeledCatchmentID] PRIMARY KEY CLUSTERED 
(
	[ModeledCatchmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_ModeledCatchment_ModeledCatchmentName_StormwaterJurisdictionID] UNIQUE NONCLUSTERED 
(
	[ModeledCatchmentName] ASC,
	[StormwaterJurisdictionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ModeledCatchment]  WITH CHECK ADD  CONSTRAINT [FK_ModeledCatchment_StormwaterJurisdiction_StormwaterJurisdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[ModeledCatchment] CHECK CONSTRAINT [FK_ModeledCatchment_StormwaterJurisdiction_StormwaterJurisdictionID]