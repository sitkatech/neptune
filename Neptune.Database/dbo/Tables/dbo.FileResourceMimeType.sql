CREATE TABLE [dbo].[FileResourceMimeType](
	[FileResourceMimeTypeID] [int] NOT NULL CONSTRAINT [PK_FileResourceMimeType_FileResourceMimeTypeID] PRIMARY KEY,
	[FileResourceMimeTypeName] [varchar](100) CONSTRAINT [AK_FileResourceMimeType_FileResourceMimeTypeName] UNIQUE,
	[FileResourceMimeTypeDisplayName] [varchar](100) CONSTRAINT [AK_FileResourceMimeType_FileResourceMimeTypeDisplayName] UNIQUE,
	[FileResourceMimeTypeContentTypeName] [varchar](100),
	[FileResourceMimeTypeIconSmallFilename] [varchar](100) NULL,
	[FileResourceMimeTypeIconNormalFilename] [varchar](100) NULL,
)
