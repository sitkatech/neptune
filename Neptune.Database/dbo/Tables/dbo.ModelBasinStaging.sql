CREATE TABLE [dbo].[ModelBasinStaging](
	[ModelBasinStagingID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ModelBasinStaging_ModelBasinStagingID] PRIMARY KEY,
	[ModelBasinKey] [int] NOT NULL CONSTRAINT [AK_ModelBasinStaging_ModelBasinKey] UNIQUE,
	[ModelBasinGeometry] [geometry] NOT NULL,
	[ModelBasinState] [varchar](5),
	[ModelBasinRegion] [varchar](10)
)
