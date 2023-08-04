CREATE TABLE [dbo].[PrecipitationZoneStaging](
	[PrecipitationZoneStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_PrecipitationZoneStaging_PrecipitationZoneStagingID] PRIMARY KEY,
	[PrecipitationZoneKey] [int] NOT NULL CONSTRAINT [AK_PrecipitationZoneStaging_PrecipitationZoneKey] UNIQUE,
	[DesignStormwaterDepthInInches] [float] NOT NULL,
	[PrecipitationZoneGeometry] [geometry] NOT NULL
)
