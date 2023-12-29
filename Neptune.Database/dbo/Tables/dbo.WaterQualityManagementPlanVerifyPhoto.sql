CREATE TABLE [dbo].[WaterQualityManagementPlanVerifyPhoto](
	[WaterQualityManagementPlanVerifyPhotoID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanVerifyPhotoID] PRIMARY KEY,
	[WaterQualityManagementPlanVerifyID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanVerify] ([WaterQualityManagementPlanVerifyID]),
	[WaterQualityManagementPlanPhotoID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanPhoto_WaterQualityManagementPlanPhotoID] FOREIGN KEY REFERENCES [dbo].[WaterQualityManagementPlanPhoto] ([WaterQualityManagementPlanPhotoID])
)