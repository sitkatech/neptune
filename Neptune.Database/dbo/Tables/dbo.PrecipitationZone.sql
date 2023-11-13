CREATE TABLE [dbo].[PrecipitationZone](
	[PrecipitationZoneID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_PrecipitationZone_PrecipitationZoneID] PRIMARY KEY,
	[PrecipitationZoneKey] [int] NOT NULL CONSTRAINT [AK_PrecipitationZone_PrecipitationZoneKey] UNIQUE,
	[DesignStormwaterDepthInInches] [float] NOT NULL,
	[PrecipitationZoneGeometry] [geometry] NOT NULL,
	[LastUpdate] [datetime] NOT NULL
)
GO

create spatial index [SPATIAL_PrecipitationZone_PrecipitationZoneGeometry] on [dbo].[PrecipitationZone]
(
	[PrecipitationZoneGeometry]
)
with (BOUNDING_BOX=(1.82669e+006, 636092, 1.89215e+006, 698762))
