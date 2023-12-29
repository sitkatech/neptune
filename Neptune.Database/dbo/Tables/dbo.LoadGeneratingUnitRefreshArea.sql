CREATE TABLE [dbo].[LoadGeneratingUnitRefreshArea](
	[LoadGeneratingUnitRefreshAreaID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_LoadGeneratingUnitRefreshArea_LoadGeneratingUnitRefreshAreaID] PRIMARY KEY,
	[LoadGeneratingUnitRefreshAreaGeometry] [geometry] NOT NULL,
	[ProcessDate] [datetime] NULL
)
GO

create spatial index [SPATIAL_LoadGeneratingUnitRefreshArea_LoadGeneratingUnitRefreshAreaGeometry] on [dbo].[LoadGeneratingUnitRefreshArea]
(
	[LoadGeneratingUnitRefreshAreaGeometry]
)
with (BOUNDING_BOX=(1.82886e+006, 638268, 1.88009e+006, 697469))
