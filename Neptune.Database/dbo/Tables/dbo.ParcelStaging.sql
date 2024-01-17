CREATE TABLE [dbo].[ParcelStaging](
	[ParcelStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ParcelStaging_ParcelStagingID] PRIMARY KEY,
	[ParcelNumber] [varchar](22),
	[Geometry] [geometry] NOT NULL,
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