SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaterQualityManagementPlanVisitStatus](
	[WaterQualityManagementPlanVisitStatusID] [int] NOT NULL,
	[TenantID] [int] NOT NULL,
	[WaterQualityManagementPlanVisitStatusName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_WaterQualityManagementPlanVisitStatus_WaterQualityManagementPlanVisitStatusID] PRIMARY KEY CLUSTERED 
(
	[WaterQualityManagementPlanVisitStatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVisitStatus]  WITH CHECK ADD  CONSTRAINT [FK_WaterQualityManagementPlanVisitStatus_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[WaterQualityManagementPlanVisitStatus] CHECK CONSTRAINT [FK_WaterQualityManagementPlanVisitStatus_Tenant_TenantID]