SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParcelStaging](
	[ParcelStagingID] [int] IDENTITY(1,1) NOT NULL,
	[ParcelNumber] [varchar](22) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ParcelStagingGeometry] [geometry] NOT NULL,
	[ParcelStagingAreaSquareFeet] [float] NULL,
	[OwnerName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParcelStreetNumber] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParcelAddress] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ParcelZipCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LandUse] [varchar](4) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SquareFeetHome] [int] NULL,
	[SquareFeetLot] [int] NULL,
	[UploadedByPersonID] [int] NOT NULL,
 CONSTRAINT [PK_ParcelStaging_ParcelStagingID] PRIMARY KEY CLUSTERED 
(
	[ParcelStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ParcelStaging]  WITH CHECK ADD  CONSTRAINT [FK_ParcelStaging_Person_UploadedByPersonID_PersonID] FOREIGN KEY([UploadedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[ParcelStaging] CHECK CONSTRAINT [FK_ParcelStaging_Person_UploadedByPersonID_PersonID]