CREATE TABLE [dbo].[HRULog](
	[HRULogID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_HRULog_HRULogID] PRIMARY KEY,
	[RequestDate] DATETIME NOT NULL,
	[Success] bit not null,
    [HRURequest] VARCHAR(MAX) NOT NULL,
    [HRUResponse] VARCHAR(MAX) NULL
)