SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanVerify](
	[WaterQualityManagementPlanVerifyID] [int] IDENTITY(1,1) NOT NULL,
	[WaterQualityManagementPlanID] [int] NOT NULL,
	[WaterQualityManagementPlanVerifyTypeID] [int] NOT NULL,
	[WaterQualityManagementPlanVisitStatusID] [int] NOT NULL,
	[FileResourceID] [int] NULL,
	[WaterQualityManagementPlanVerifyStatusID] [int] NULL,
	[LastEditedByPersonID] [int] NOT NULL,
	[SourceControlCondition] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EnforcementOrFollowupActions] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LastEditedDate] [datetime] NOT NULL,
	[IsDraft] [bit] NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanVerifyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerify_FileResource_FileResourceID] FOREIGN KEY([FileResourceID])
REFERENCES [dbo].[FileResource] ([FileResourceID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerify_FileResource_FileResourceID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerify_Person_LastEditedByPersonID_PersonID] FOREIGN KEY([LastEditedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerify_Person_LastEditedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlan_WaterQualityManagementPlanID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyStatus_WaterQualityManagementPlanVerifyStatusID] FOREIGN KEY([WaterQualityManagementPlanVerifyStatusID])
REFERENCES [dbo].[WaterQualityManagementPlanVerifyStatus] ([WaterQualityManagementPlanVerifyStatusID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyStatus_WaterQualityManagementPlanVerifyStatusID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyType_WaterQualityManagementPlanVerifyTypeID] FOREIGN KEY([WaterQualityManagementPlanVerifyTypeID])
REFERENCES [dbo].[WaterQualityManagementPlanVerifyType] ([WaterQualityManagementPlanVerifyTypeID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVerifyType_WaterQualityManagementPlanVerifyTypeID]
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVisitStatus_WaterQualityManagementPlanVisitStatusID] FOREIGN KEY([WaterQualityManagementPlanVisitStatusID])
REFERENCES [dbo].[WaterQualityManagementPlanVisitStatus] ([WaterQualityManagementPlanVisitStatusID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVerify] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVerify_WaterQualityManagementPlanVisitStatus_WaterQualityManagementPlanVisitStatusID]