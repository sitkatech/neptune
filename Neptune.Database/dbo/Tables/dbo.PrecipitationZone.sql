CREATE TABLE [dbo].[PrecipitationZone](
	[PrecipitationZoneID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_PrecipitationZone_PrecipitationZoneID] PRIMARY KEY,
	[PrecipitationZoneKey] [int] NOT NULL CONSTRAINT [AK_PrecipitationZone_PrecipitationZoneKey] UNIQUE,
	[DesignStormwaterDepthInInches] [float] NOT NULL,
	[PrecipitationZoneGeometry] [geometry] NOT NULL,
	[LastUpdate] [datetime] NOT NULL
)
