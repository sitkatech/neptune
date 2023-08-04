CREATE TABLE [dbo].[LoadGeneratingUnitRefreshArea](
	[LoadGeneratingUnitRefreshAreaID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_LoadGeneratingUnitRefreshArea_LoadGeneratingUnitRefreshAreaID] PRIMARY KEY,
	[LoadGeneratingUnitRefreshAreaGeometry] [geometry] NOT NULL,
	[ProcessDate] [datetime] NULL
)
