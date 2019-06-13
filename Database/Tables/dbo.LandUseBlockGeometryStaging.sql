SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LandUseBlockGeometryStaging](
	[LandUseBlockGeometryStagingID] [int] IDENTITY(1,1) NOT NULL,
	[PersonID] [int] NOT NULL,
	[StormwaterJurisdictionID] [int] NOT NULL,
	[FeatureClassName] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LandUseBlockGeometryStagingGeoJson] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SelectedProperty] [varchar](255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ShouldImport] [bit] NOT NULL,
 CONSTRAINT [PK_LandUseBlockGeometryStaging_LandUseBlockGeometryStagingID] PRIMARY KEY CLUSTERED 
(
	[LandUseBlockGeometryStagingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[LandUseBlockGeometryStaging]  WITH CHECK ADD  CONSTRAINT [FK_LandUseBlockGeometryStaging_Person_PersonID] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[LandUseBlockGeometryStaging] CHECK CONSTRAINT [FK_LandUseBlockGeometryStaging_Person_PersonID]
GO
ALTER TABLE [dbo].[LandUseBlockGeometryStaging]  WITH CHECK ADD  CONSTRAINT [FK_LandUseBlockGeometryStaging_StormwaterJurisdiction_StormwaterJurosdictionID] FOREIGN KEY([StormwaterJurisdictionID])
REFERENCES [dbo].[StormwaterJurisdiction] ([StormwaterJurisdictionID])
GO
ALTER TABLE [dbo].[LandUseBlockGeometryStaging] CHECK CONSTRAINT [FK_LandUseBlockGeometryStaging_StormwaterJurisdiction_StormwaterJurosdictionID]