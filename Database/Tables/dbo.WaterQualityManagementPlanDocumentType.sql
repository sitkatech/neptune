SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanDocumentType](
	[WaterQualityManagementPlanDocumentTypeID] [int] NOT NULL,
	[WaterQualityManagementPlanDocumentTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[WaterQualityManagementPlanDocumentTypeDisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[IsRequired] [bit] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanDocumentTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeDisplayName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanDocumentTypeDisplayName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeName] UNIQUE NONCLUSTERED 
(
	[WaterQualityManagementPlanDocumentTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
