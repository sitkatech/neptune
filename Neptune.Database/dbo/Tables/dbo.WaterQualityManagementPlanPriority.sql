CREATE TABLE [dbo].[WaterQualityManagementPlanPriority](
	[WaterQualityManagementPlanPriorityID] [int] NOT NULL,
	[WaterQualityManagementPlanPriorityName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[WaterQualityManagementPlanPriorityDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanPriorityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityDisplayName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanPriorityDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanPriority_WaterQualityManagementPlanPriorityName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanPriorityName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
