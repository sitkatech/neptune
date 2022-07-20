CREATE TABLE [dbo].[ParcelStaging] (
	[ParcelStagingID] [int] IDENTITY(1,1) NOT NULL constraint PK_ParcelStaging_ParcelStagingID primary key,
	[ParcelNumber] [varchar](22) NOT NULL,
	[ParcelStagingGeometry] [geometry] NOT NULL,
	[ParcelStagingAreaSquareFeet] [float] NULL,
	[OwnerName] [varchar](100) NULL,
	[ParcelStreetNumber] [varchar](10) NULL,
	[ParcelAddress] [varchar](150) NULL,
	[ParcelZipCode] [varchar](5) NULL,
	[LandUse] [varchar](4) NULL,
	[SquareFeetHome] [int] NULL,
	[SquareFeetLot] [int] NULL,
	[UploadedByPersonID] [int] NOT NULL constraint FK_ParcelStaging_Person_UploadedByPersonID_PersonID foreign key (UploadedByPersonID) references dbo.Person(PersonID),
)

