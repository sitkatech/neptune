CREATE TABLE [dbo].[WaterQualityManagementPlanPhoto](
	[WaterQualityManagementPlanPhotoID] [int] IDENTITY(1,1) NOT NULL CONSTRAINT [PK_WaterQualityManagementPlanPhoto_WaterQualityManagementPlanPhotoID] PRIMARY KEY,
	[FileResourceID] [int] NOT NULL CONSTRAINT [FK_WaterQualityManagementPlanPhoto_FileResource_FileResourceID] FOREIGN KEY REFERENCES [dbo].[FileResource] ([FileResourceID]),
	[Caption] [varchar](500) NULL,
	[UploadDate] [datetime] NOT NULL,
)