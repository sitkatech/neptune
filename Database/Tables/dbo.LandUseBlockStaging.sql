SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LandUseBlockStaging](
	[LandUseBlockStagingID] [int] IDENTITY(1,1) NOT NULL,
	[PriorityLandUseType] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LandUseDescription] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LandUseBlockStagingGeometry] [geometry] NOT NULL,
	[TrashGenerationRate] [decimal](4, 1) NULL,
	[LandUseForTGR] [varchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MedianHouseholdIncome] [numeric](18, 0) NULL,
	[StormwaterJurisdiction] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PermitType] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UploadedByPersonID] [int] NOT NULL,
 CONSTRAINT [PK_LandUseBlockStaging_LandUseBlockStagingID] PRIMARY KEY CLUSTERED 
(
	[LandUseBlockStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[LandUseBlockStaging]  WITH CHECK ADD  CONSTRAINT [FK_LandUseBlockStaging_Person_UploadedByPersonID_PersonID] FOREIGN KEY([UploadedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[LandUseBlockStaging] CHECK CONSTRAINT [FK_LandUseBlockStaging_Person_UploadedByPersonID_PersonID]