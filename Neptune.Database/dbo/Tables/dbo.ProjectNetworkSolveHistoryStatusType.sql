CREATE TABLE [dbo].[ProjectNetworkSolveHistoryStatusType](
	[ProjectNetworkSolveHistoryStatusTypeID] [int] NOT NULL,
	[ProjectNetworkSolveHistoryStatusTypeName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[ProjectNetworkSolveHistoryStatusTypeDisplayName] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeID] PRIMARY KEY CLUSTERED 
(
	[ProjectNetworkSolveHistoryStatusTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectNetworkSolveHistoryStatusType_ProjectNetworkSolveHistoryStatusTypeName] UNIQUE NONCLUSTERED 
(
	[ProjectNetworkSolveHistoryStatusTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_ProjectNetworkSolveHistoryStatusTypeProjectNetworkSolveHistoryStatusTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[ProjectNetworkSolveHistoryStatusTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
