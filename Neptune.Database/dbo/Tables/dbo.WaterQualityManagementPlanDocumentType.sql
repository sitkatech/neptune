CREATE TABLE [dbo].[WaterQualityManagementPlanDocumentType](
	[WaterQualityManagementPlanDocumentTypeID] [int] NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeID] PRIMARY KEY,
	[WaterQualityManagementPlanDocumentTypeName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeName] UNIQUE,
	[WaterQualityManagementPlanDocumentTypeDisplayName] [varchar](100) CONSTRAINT [AK_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeDisplayName] UNIQUE,
	IsRequired bit not null
)
