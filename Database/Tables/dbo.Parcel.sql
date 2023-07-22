SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parcel](
	[ParcelID] [int] IDENTITY(1,1) NOT NULL,
	[ParcelNumber] [varchar](22) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[OwnerName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParcelStreetNumber] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParcelAddress] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParcelZipCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LandUse] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SquareFeetHome] [int] NULL,
	[SquareFeetLot] [int] NULL,
	[ParcelAreaInAcres] [float] NOT NULL,
 CONSTRAINT [PK_Parcel_ParcelID] PRIMARY KEY CLUSTERED 
(
	[ParcelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
