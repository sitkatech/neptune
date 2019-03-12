SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanStatus](
	[WaterQualityManagementPlanStatusID] [int] NOT NULL,
	[WaterQualityManagementPlanStatusName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[WaterQualityManagementPlanStatusDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusDisplayName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanStatusDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanStatus_WaterQualityManagementPlanStatusName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanStatusName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
