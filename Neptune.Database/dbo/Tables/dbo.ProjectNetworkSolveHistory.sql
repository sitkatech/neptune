CREATE TABLE [dbo].[ProjectNetworkSolveHistory](
	[ProjectNetworkSolveHistoryID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_ProjectNetworkSolveHistory_ProjectNetworkSolveHistoryID] PRIMARY KEY,
	[ProjectID] [int] NOT NULL CONSTRAINT [FK_ProjectNetworkSolveHistory_Project_ProjectID] FOREIGN KEY REFERENCES [dbo].[Project] ([ProjectID]),
	[RequestedByPersonID] [int] NOT NULL CONSTRAINT [FK_ProjectNetworkSolveHistory_Person_RequestedByPersonID_PersonID] FOREIGN KEY REFERENCES [dbo].[Person] ([PersonID]),
	[ProjectNetworkSolveHistoryStatusTypeID] [int] NOT NULL CONSTRAINT [FK_ProjectNetworkSolveHistory_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeID] FOREIGN KEY REFERENCES [dbo].[ProjectNetworkSolveHistoryStatusType] ([ProjectNetworkSolveHistoryStatusTypeID]),
	[LastUpdated] [datetime] NOT NULL,
	[ErrorMessage] [dbo].[html] NULL
)