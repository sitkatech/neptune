CREATE TABLE [dbo].[RegionalSubbasinRevisionRequestStatus](
	[RegionalSubbasinRevisionRequestStatusID] [int] NOT NULL CONSTRAINT [PK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusID] PRIMARY KEY,
	[RegionalSubbasinRevisionRequestStatusName] [varchar](100) CONSTRAINT [AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusName] UNIQUE,
	[RegionalSubbasinRevisionRequestStatusDisplayName] [varchar](100) CONSTRAINT [AK_RegionalSubbasinRevisionRequestStatus_RegionalSubbasinRevisionRequestStatusDisplayName] UNIQUE
)