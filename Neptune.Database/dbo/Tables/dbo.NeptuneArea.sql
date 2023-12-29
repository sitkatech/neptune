CREATE TABLE [dbo].[NeptuneArea](
	[NeptuneAreaID] [int] NOT NULL CONSTRAINT [PK_NeptuneArea_NeptuneAreaID] PRIMARY KEY,
	[NeptuneAreaName] [varchar](20) CONSTRAINT [AK_NeptuneArea_NeptuneAreaName] UNIQUE,
	[NeptuneAreaDisplayName] [varchar](40) CONSTRAINT [AK_NeptuneArea_NeptuneAreaDisplayName] UNIQUE,
	[SortOrder] [int] NOT NULL,
	[ShowOnPrimaryNavigation] [bit] NOT NULL
)
