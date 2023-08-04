CREATE TABLE [dbo].[FileResource](
	[FileResourceID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_FileResource_FileResourceID] PRIMARY KEY,
	[FileResourceMimeTypeID] [int] NOT NULL CONSTRAINT [FK_FileResource_FileResourceMimeType_FileResourceMimeTypeID] FOREIGN KEY REFERENCES [dbo].[FileResourceMimeType] ([FileResourceMimeTypeID]),
	[OriginalBaseFilename] [varchar](255),
	[OriginalFileExtension] [varchar](255),
	[FileResourceGUID] [uniqueidentifier] NOT NULL CONSTRAINT [AK_FileResource_FileResourceGUID] UNIQUE,
	[FileResourceData] [varbinary](max) NOT NULL,
	[CreatePersonID] [int] NOT NULL CONSTRAINT [FK_FileResource_Person_CreatePersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[CreateDate] [datetime] NOT NULL
)