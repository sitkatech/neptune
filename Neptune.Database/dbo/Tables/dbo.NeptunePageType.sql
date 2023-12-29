CREATE TABLE [dbo].[NeptunePageType](
	[NeptunePageTypeID] [int] NOT NULL CONSTRAINT [PK_NeptunePageType_NeptunePageTypeID] PRIMARY KEY,
	[NeptunePageTypeName] [varchar](100) CONSTRAINT [AK_NeptunePageType_NeptunePageTypeName] UNIQUE,
	[NeptunePageTypeDisplayName] [varchar](100) CONSTRAINT [AK_NeptunePageType_NeptunePageTypeDisplayName] UNIQUE
)
