CREATE TABLE [dbo].[LandUseBlockStaging](
	[LandUseBlockStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_LandUseBlockStaging_LandUseBlockStagingID] PRIMARY KEY,
	[PriorityLandUseType] [varchar](255) NULL,
	[LandUseDescription] [varchar](500) NULL,
	[LandUseBlockStagingGeometry] [geometry] NOT NULL,
	[TrashGenerationRate] [decimal](4, 1) NULL,
	[LandUseForTGR] [varchar](80) NULL,
	[MedianHouseholdIncome] [numeric](18, 0) NULL,
	[StormwaterJurisdiction] [varchar](255),
	[PermitType] [varchar](255) NULL,
	[UploadedByPersonID] [int] NOT NULL CONSTRAINT [FK_LandUseBlockStaging_Person_UploadedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
)