CREATE TABLE [dbo].[WaterQualityManagementPlanPermitTerm](
	[WaterQualityManagementPlanPermitTermID] [int] NOT NULL,
	[WaterQualityManagementPlanPermitTermName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[WaterQualityManagementPlanPermitTermDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[SortOrder] [int] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanPermitTermID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermDisplayName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanPermitTermDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanPermitTerm_WaterQualityManagementPlanPermitTermName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanPermitTermName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
