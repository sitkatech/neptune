CREATE TABLE [dbo].[WaterQualityManagementPlanVerifySourceControlBMP](
	[WaterQualityManagementPlanVerifySourceControlBMPID] [int] IDENTITY(1,1) NOT NULL,
	[WaterQualityManagementPlanVerifyID] [int] NOT NULL,
	[SourceControlBMPID] [int] NOT NULL,
	[WaterQualityManagementPlanSourceControlCondition] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanVerifySourceControlBMP_WaterQualityManagementPlanVerifySourceControlBMPID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanVerifySourceControlBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifySourceControlBMP_SourceControlBMP_SourceControlBMPID] FOREIGN KEY([SourceControlBMPID])
REFERENCES [dbo].[SourceControlBMP] ([SourceControlBMPID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifySourceControlBMP] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifySourceControlBMP_SourceControlBMP_SourceControlBMPID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifySourceControlBMP]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerifySourceControlBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID] FOREIGN KEY([WaterQualityManagementPlanVerifyID])
REFERENCES [dbo].[WaterQualityManagementPlanVerify] ([WaterQualityManagementPlanVerifyID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerifySourceControlBMP] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerifySourceControlBMP_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID]