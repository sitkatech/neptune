CREATE TABLE [dbo].[PrecipitationZoneStaging](
	[PrecipitationZoneStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_PrecipitationZoneStaging_PrecipitationZoneStagingID] PRIMARY KEY,
	[PrecipitationZoneKey] [int] NOT NULL CONSTRAINT [AK_PrecipitationZoneStaging_PrecipitationZoneKey] UNIQUE,
	[DesignStormwaterDepthInInches] [float] NOT NULL,
	[PrecipitationZoneGeometry] [geometry] NOT NULL
)
GO

create spatial index [SPATIAL_PrecipitationZoneStaging_PrecipitationZoneGeometry] on [dbo].[PrecipitationZoneStaging]
(
	[PrecipitationZoneGeometry]
)
with (BOUNDING_BOX=(1.82669e+006, 636092, 1.89215e+006, 698762))