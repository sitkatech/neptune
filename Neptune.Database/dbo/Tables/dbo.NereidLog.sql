CREATE TABLE [dbo].[NereidLog](
	[NereidLogID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_NereidLog_NereidLogID] PRIMARY KEY,
	[RequestDate] DATETIME NOT NULL,
    [NereidRequest] VARCHAR(MAX) NOT NULL,
    [NereidResponse] VARCHAR(MAX) NULL
)