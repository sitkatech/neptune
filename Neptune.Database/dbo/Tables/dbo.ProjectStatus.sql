CREATE TABLE [dbo].[ProjectStatus](
	[ProjectStatusID] [int] NOT NULL CONSTRAINT [PK_ProjectStatus_ProjectStatusID] PRIMARY KEY,
	[ProjectStatusName] [varchar](100) CONSTRAINT [AK_ProjectStatus_ProjectStatusName] UNIQUE,
	[ProjectStatusDisplayName] [varchar](100) CONSTRAINT [AK_ProjectStatus_ProjectStatusDisplayName] UNIQUE
)
