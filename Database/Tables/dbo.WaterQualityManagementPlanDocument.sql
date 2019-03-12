SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanDocument](
	[WaterQualityManagementPlanDocumentID] [int] IDENTITY(1,1) NOT NULL,
	[WaterQualityManagementPlanID] [int] NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[DisplayName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UploadDate] [datetime] NOT NULL,
	[WaterQualityManagementPlanDocumentTypeID] [int] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanDocument_WaterQualityManagementPlanDocumentID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanDocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_WaterQualityManagementPlanDocument_DisplayName_WaterQualityManagementPlanID] UNIQUE NONCLUSTERED 
(
	[DisplayName] ASC,
	[WaterQualityManagementPlanID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanDocument]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanDocument_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanDocument] CHECK CONSTRAINT [FK_WaterQualityManagementPlanDocument_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanDocument]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanDocument_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanDocument] CHECK CONSTRAINT [FK_WaterQualityManagementPlanDocument_WaterQualityManagementPlan_WaterQualityManagementPlanID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanDocument]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanDocument_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeID] FOREIGN KEY([WaterQualityManagementPlanDocumentTypeID])
REFERENCES [dbo].[WaterQualityManagementPlanDocumentType] ([WaterQualityManagementPlanDocumentTypeID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanDocument] CHECK CONSTRAINT [FK_WaterQualityManagementPlanDocument_WaterQualityManagementPlanDocumentType_WaterQualityManagementPlanDocumentTypeID]