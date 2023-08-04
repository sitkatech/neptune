CREATE TABLE [dbo].[UnderlyingHydrologicSoilGroup](
	[UnderlyingHydrologicSoilGroupID] [int] NOT NULL CONSTRAINT [PK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupID] PRIMARY KEY,
	[UnderlyingHydrologicSoilGroupName] [varchar](100) CONSTRAINT [AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupName] UNIQUE,
	[UnderlyingHydrologicSoilGroupDisplayName] [varchar](100) CONSTRAINT [AK_UnderlyingHydrologicSoilGroup_UnderlyingHydrologicSoilGroupDisplayName] UNIQUE
)
