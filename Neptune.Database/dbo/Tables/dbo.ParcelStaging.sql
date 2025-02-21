CREATE TABLE [dbo].[ParcelStaging](
	[ParcelStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ParcelStaging_ParcelStagingID] PRIMARY KEY,
	[ParcelNumber] [varchar](22),
	[Geometry] [geometry] NOT NULL,
	[ParcelAreaInSquareFeet] [float] NOT NULL,
	[ParcelAddress] [varchar](150) NULL,
	[ParcelCityState] [varchar](100) NULL,
	[ParcelZipCode] [varchar](5) NULL
)
GO