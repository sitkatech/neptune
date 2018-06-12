SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanDevelopmentType](
	[WaterQualityManagementPlanDevelopmentTypeID] [int] NOT NULL,
	[WaterQualityManagementPlanDevelopmentTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[WaterQualityManagementPlanDevelopmentTypeDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanDevelopmentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanDevelopmentTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanDevelopmentType_WaterQualityManagementPlanDevelopmentTypeName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanDevelopmentTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
