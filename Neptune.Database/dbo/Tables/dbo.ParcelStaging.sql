CREATE TABLE [dbo].[ParcelStaging](
	[ParcelStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ParcelStaging_ParcelStagingID] PRIMARY KEY,
	[ParcelNumber] [varchar](22),
	[ParcelStagingGeometry] [geometry] NOT NULL,
	[ParcelStagingAreaSquareFeet] [float] NOT NULL,
	[OwnerName] [varchar](100) NULL,
	[ParcelStreetNumber] [varchar](10) NULL,
	[ParcelAddress] [varchar](150) NULL,
	[ParcelZipCode] [varchar](5) NULL,
	[LandUse] [varchar](4) NULL,
	[SquareFeetHome] [int] NULL,
	[SquareFeetLot] [int] NULL,
	[UploadedByPersonID] [int] NOT NULL CONSTRAINT [FK_ParcelStaging_Person_UploadedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID])
)
GO

CREATE SPATIAL INDEX [SPATIAL_ParcelStaging_ParcelStagingGeometry] ON [dbo].[ParcelStaging]
(
	[ParcelStagingGeometry]
)USING  GEOMETRY_GRID 
WITH (BOUNDING_BOX =(1826744.02047756, 636159.772575649, 1892152.0783707, 698755.88120627), GRIDS =(LEVEL_1 = MEDIUM,LEVEL_2 = MEDIUM,LEVEL_3 = MEDIUM,LEVEL_4 = MEDIUM), 
CELLS_PER_OBJECT = 16, PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
GO