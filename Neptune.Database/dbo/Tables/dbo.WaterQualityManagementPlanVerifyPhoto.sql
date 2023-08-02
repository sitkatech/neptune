CREATE TABLE [dbo].[WaterQualityManagementPlanVerifyPhoto](
	[WaterQualityManagementPlanVerifyPhotoID] [int] IDENTITY(1,1) NOT NULL,
	[WaterQualityManagementPlanVerifyID] [int] NOT NULL,
	[WaterQualityManagementPlanPhotoID] [int] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanVerifyPhotoID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanVerifyPhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyPhoto]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanPhoto_WaterQualityManagementPlanPhotoID] FOREIGN KEY([WaterQualityManagementPlanPhotoID])
REFERENCES [dbo].[WaterQualityManagementPlanPhoto] ([WaterQualityManagementPlanPhotoID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyPhoto] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanPhoto_WaterQualityManagementPlanPhotoID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyPhoto]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID] FOREIGN KEY([WaterQualityManagementPlanVerifyID])
REFERENCES [dbo].[WaterQualityManagementPlanVerify] ([WaterQualityManagementPlanVerifyID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifyPhoto] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifyPhoto_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID]