SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SourceControlBMP](
	[SourceControlBMPID] [int] IDENTITY(1,1) NOT NULL,
	[WaterQualityManagementPlanID] [int] NOT NULL,
	[SourceControlBMPAttributeID] [int] NOT NULL,
	[IsPresent] [bit] NULL,
	[SourceControlBMPNote] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_SourceControlBMP_SourceControlBMPID] PRIMARY KEY CLUSTERED 
(
	[SourceControlBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[SourceControlBMP]  WITH CHECK ADD  CONSTRAINT [FK_SourceControlBMP_SourceControlBMPAttribute_SourceControlBMPAttributeID] FOREIGN KEY([SourceControlBMPAttributeID])
REFERENCES [dbo].[SourceControlBMPAttribute] ([SourceControlBMPAttributeID])
GO
ALTER TABLE [dbo].[SourceControlBMP] CHECK CONSTRAINT [FK_SourceControlBMP_SourceControlBMPAttribute_SourceControlBMPAttributeID]
GO
ALTER TABLE [dbo].[SourceControlBMP]  WITH CHECK ADD  CONSTRAINT [FK_SourceControlBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID] FOREIGN KEY([WaterQualityManagementPlanID])
REFERENCES [dbo].[WaterQualityManagementPlan] ([WaterQualityManagementPlanID])
GO
ALTER TABLE [dbo].[SourceControlBMP] CHECK CONSTRAINT [FK_SourceControlBMP_WaterQualityManagementPlan_WaterQualityManagementPlanID]