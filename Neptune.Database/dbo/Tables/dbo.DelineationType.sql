CREATE TABLE [dbo].[DelineationType](
	[DelineationTypeID] [int] NOT NULL CONSTRAINT [PK_DelineationType_DelineationTypeID] PRIMARY KEY,
	[DelineationTypeName] [varchar](100) CONSTRAINT [AK_DelineationType_DelineationTypeName] UNIQUE,
	[DelineationTypeDisplayName] [varchar](100) CONSTRAINT [AK_DelineationType_DelineationTypeDisplayName] UNIQUE
)
