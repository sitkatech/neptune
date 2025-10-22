CREATE TABLE [dbo].[WaterQualityManagementPlanDocumentVectorStore]
(
    [WaterQualityManagementPlanDocumentVectorStoreID]    INT IDENTITY(1,1) NOT NULL,
    [WaterQualityManagementPlanDocumentID]               INT NOT NULL,
    [OpenAIVectorStoreID]                                VARCHAR(50) NOT NULL,

    CONSTRAINT [PK_WaterQualityManagementPlanDocumentVectorStore] PRIMARY KEY ([WaterQualityManagementPlanDocumentVectorStoreID]),
    CONSTRAINT [FK_WaterQualityManagementPlanDocumentVectorStore_WaterQualityManagementPlanDocument_WaterQualityManagementPlanDocumentID] FOREIGN KEY ([WaterQualityManagementPlanDocumentID]) REFERENCES dbo.WaterQualityManagementPlanDocument([WaterQualityManagementPlanDocumentID]),
    CONSTRAINT [UQ_WaterQualityManagementPlanDocumentVectorStore_WaterQualityManagementPlanDocumentID] UNIQUE ([WaterQualityManagementPlanDocumentID]),
)
GO
