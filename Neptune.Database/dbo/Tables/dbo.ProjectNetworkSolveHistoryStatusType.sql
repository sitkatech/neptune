CREATE TABLE [dbo].[ProjectNetworkSolveHistoryStatusType](
	[ProjectNetworkSolveHistoryStatusTypeID] [int] NOT NULL CONSTRAINT [PK_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeID] PRIMARY KEY,
	[ProjectNetworkSolveHistoryStatusTypeName] [varchar](100) CONSTRAINT [AK_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeName] UNIQUE,
	[ProjectNetworkSolveHistoryStatusTypeDisplayName] [varchar](100) CONSTRAINT [AK_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeDisplayName] UNIQUE
)