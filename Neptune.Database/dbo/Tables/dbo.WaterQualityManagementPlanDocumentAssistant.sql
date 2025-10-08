CREATE TABLE [dbo].[WaterQualityManagementPlanDocumentAssistant]
(
    [WaterQualityManagementPlanDocumentAssistantID]    INT IDENTITY(1,1) NOT NULL,
    [WaterQualityManagementPlanDocumentID]             INT NOT NULL,
    [AssistantID]                                      VARCHAR(50) NOT NULL,

    CONSTRAINT [PK_WaterQualityManagementPlanDocumentAssistant] PRIMARY KEY ([WaterQualityManagementPlanDocumentAssistantID]),
    CONSTRAINT [FK_WaterQualityManagementPlanDocumentAssistant_WaterQualityManagementPlanDocument_WaterQualityManagementPlanDocumentID] FOREIGN KEY ([WaterQualityManagementPlanDocumentID]) REFERENCES dbo.WaterQualityManagementPlanDocument([WaterQualityManagementPlanDocumentID]),
    CONSTRAINT [UQ_WaterQualityManagementPlanDocumentAssistant_WaterQualityManagementPlanDocumentID] UNIQUE ([WaterQualityManagementPlanDocumentID]),
)
GO
