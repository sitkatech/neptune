CREATE TABLE [dbo].[WaterQualityManagementPlanVerifyStatus](
	[WaterQualityManagementPlanVerifyStatusID] [int] NOT NULL,
	[WaterQualityManagementPlanVerifyStatusName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanVerifyStatus_WaterQualityManagementPlanVerifyStatusID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanVerifyStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
