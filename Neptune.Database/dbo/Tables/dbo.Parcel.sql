CREATE TABLE [dbo].[Parcel](
	[ParcelID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_Parcel_ParcelID] PRIMARY KEY,
	[ParcelNumber] [varchar](22),
	[ParcelAddress] [varchar](150) NULL,
	[ParcelCityState] [varchar](100) NULL,
	[ParcelZipCode] [varchar](5) NULL,
	[ParcelAreaInAcres] [float] NOT NULL,
	[LastUpdate] [datetime] NOT NULL default('6/1/2023')
)