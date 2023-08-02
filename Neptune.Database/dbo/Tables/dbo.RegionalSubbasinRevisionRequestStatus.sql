CREATE TABLE [dbo].[RegionalSubbasinRevisionRequestStatus](
	[RegionalSubbasinRevisionRequestStatusID] [int] NOT NULL,
	[RegionalSubbasinRevisionRequestStatusName] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[RegionalSubbasinRevisionRequestStatusDisplayName] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusID] PRIMARY KEY CLUSTERED 
(
	[RegionalSubbasinRevisionRequestStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusDisplayName] UNIQUE NONCLUSTERED 
(
	[RegionalSubbasinRevisionRequestStatusDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusName] UNIQUE NONCLUSTERED 
(
	[RegionalSubbasinRevisionRequestStatusName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
