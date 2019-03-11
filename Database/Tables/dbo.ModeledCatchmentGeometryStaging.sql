SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModeledCatchmentGeometryStaging](
	[ModeledCatchmentGeometryStagingID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[FeatureClassName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GeoJson] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SelectedProperty] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShouldImport] [bit] NOT NULL,
 CONSTRAINT [PK_ModeledCatchmentGeometryStaging_ModeledCatchmentGeometryStagingID] PRIMARY KEY CLUSTERED 
(
	[ModeledCatchmentGeometryStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ModeledCatchmentGeometryStaging]  WITH CHECK ADD  CONSTRAINT [FK_ModeledCatchmentGeometryStaging_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[ModeledCatchmentGeometryStaging] CHECK CONSTRAINT [FK_ModeledCatchmentGeometryStaging_Person_PersonID]