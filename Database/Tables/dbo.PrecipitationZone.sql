SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrecipitationZone](
	[PrecipitationZoneID] [int] IDENTITY(1,1) NOT NULL,
	[PrecipitationZoneKey] [int] NOT NULL,
	[DesignStormwaterDepthInInches] [float] NOT NULL,
	[PrecipitationZoneGeometry] [geometry] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_PrecipitationZone_PrecipitationZoneID] PRIMARY KEY CLUSTERED 
(
	[PrecipitationZoneID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_PrecipitationZone_PrecipitationZoneKey] UNIQUE NONCLUSTERED 
(
	[PrecipitationZoneKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
