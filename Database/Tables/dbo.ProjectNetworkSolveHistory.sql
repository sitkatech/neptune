SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectNetworkSolveHistory](
	[ProjectNetworkSolveHistoryID] [int] IDENTITY(1,1) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[RequestedByPersonID] [int] NOT NULL,
	[ProjectNetworkSolveHistoryStatusTypeID] [int] NOT NULL,
	[ErrorMessage] [dbo].[html] NULL,
 CONSTRAINT [PK_ProjectNetworkSolveHistory_ProjectNetworkSolveHistoryID] PRIMARY KEY CLUSTERED 
(
	[ProjectNetworkSolveHistoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ProjectNetworkSolveHistory]  WITH CHECK ADD  CONSTRAINT [FK_ProjectNetworkSolveHistory_Person_RequestedByPersonID_PersonID] FOREIGN KEY([RequestedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[ProjectNetworkSolveHistory] CHECK CONSTRAINT [FK_ProjectNetworkSolveHistory_Person_RequestedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[ProjectNetworkSolveHistory]  WITH CHECK ADD  CONSTRAINT [FK_ProjectNetworkSolveHistory_Project_ProjectID] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[ProjectNetworkSolveHistory] CHECK CONSTRAINT [FK_ProjectNetworkSolveHistory_Project_ProjectID]
GO
ALTER TABLE [dbo].[ProjectNetworkSolveHistory]  WITH CHECK ADD  CONSTRAINT [FK_ProjectNetworkSolveHistory_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeID] FOREIGN KEY([ProjectNetworkSolveHistoryStatusTypeID])
REFERENCES [dbo].[ProjectNetworkSolveHistoryStatusType] ([ProjectNetworkSolveHistoryStatusTypeID])
GO
ALTER TABLE [dbo].[ProjectNetworkSolveHistory] CHECK CONSTRAINT [FK_ProjectNetworkSolveHistory_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeID]