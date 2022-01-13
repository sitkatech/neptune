SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanPhoto](
	[WaterQualityManagementPlanPhotoID] [int] IDENTITY(1,1) NOT NULL,
	[FileResourceID] [int] NOT NULL,
	[Caption] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UploadDate] [datetime] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanPhoto_WaterQualityManagementPlanPhotoID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanPhotoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanPhoto]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanPhoto_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanPhoto] CHECK CONSTRAINT [FK_WaterQualityManagementPlanPhoto_FileResource_FileResourceID]