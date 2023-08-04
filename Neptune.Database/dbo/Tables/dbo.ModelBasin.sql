CREATE TABLE [dbo].[ModelBasin](
	[ModelBasinID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ModelBasin_ModelBasinID] PRIMARY KEY,
	[ModelBasinKey] [int] NOT NULL CONSTRAINT [AK_ModelBasin_ModelBasinKey] UNIQUE,
	[ModelBasinGeometry] [geometry] NOT NULL,
	[LastUpdate] [datetime] NOT NULL,
	[ModelBasinState] [varchar](5),
	[ModelBasinRegion] [varchar](10)
)
