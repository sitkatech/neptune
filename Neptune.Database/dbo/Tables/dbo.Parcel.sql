CREATE TABLE [dbo].[Parcel](
	[ParcelID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Parcel_ParcelID] PRIMARY KEY,
	[ParcelNumber] [varchar](22),
	[OwnerName] [varchar](100) NULL,
	[ParcelStreetNumber] [varchar](10) NULL,
	[ParcelAddress] [varchar](150) NULL,
	[ParcelZipCode] [varchar](5) NULL,
	[LandUse] [varchar](4) NULL,
	[SquareFeetHome] [int] NULL,
	[SquareFeetLot] [int] NULL,
	[ParcelAreaInAcres] [float] NOT NULL
)