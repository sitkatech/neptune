CREATE TABLE [dbo].[StormwaterBreadCrumbEntity](
	[StormwaterBreadCrumbEntityID] [int] NOT NULL CONSTRAINT [PK_StormwaterBreadCrumbEntity_StormwaterBreadCrumbEntityID] PRIMARY KEY,
	[StormwaterBreadCrumbEntityName] [varchar](100) CONSTRAINT [AK_StormwaterBreadCrumbEntity_StormwaterBreadCrumbEntityName] UNIQUE,
	[StormwaterBreadCrumbEntityDisplayName] [varchar](100) CONSTRAINT [AK_StormwaterBreadCrumbEntity_StormwaterBreadCrumbEntityDisplayName] UNIQUE,
	[GlyphIconClass] [varchar](100),
	[ColorClass] [varchar](100)
)
