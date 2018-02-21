SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceActivity](
	[MaintenanceActivityID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[MaintenanceActivityDate] [date] NOT NULL,
	[PerformedByPersonID] [int] NOT NULL,
	[MaintenanceActivityDescription] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaintenanceActivityTypeID] [int] NOT NULL,
 CONSTRAINT [PK_MaintenanceActivity_MaintenanceActivityID] PRIMARY KEY CLUSTERED 
(
	[MaintenanceActivityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_MaintenanceActivity_MaintenanceActivityID_TenantID] UNIQUE NONCLUSTERED 
(
	[MaintenanceActivityID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MaintenanceActivity]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceActivity_MaintenanceActivityType_MaintenanceActivityTypeID] FOREIGN KEY([MaintenanceActivityTypeID])
REFERENCES [dbo].[MaintenanceActivityType] ([MaintenanceActivityTypeID])
GO
ALTER TABLE [dbo].[MaintenanceActivity] CHECK CONSTRAINT [FK_MaintenanceActivity_MaintenanceActivityType_MaintenanceActivityTypeID]
GO
ALTER TABLE [dbo].[MaintenanceActivity]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceActivity_Person_PerformedByPersonID_PersonID] FOREIGN KEY([PerformedByPersonID])
REFERENCES [dbo].[Person] ([PersonID])
GO
ALTER TABLE [dbo].[MaintenanceActivity] CHECK CONSTRAINT [FK_MaintenanceActivity_Person_PerformedByPersonID_PersonID]
GO
ALTER TABLE [dbo].[MaintenanceActivity]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceActivity_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[MaintenanceActivity] CHECK CONSTRAINT [FK_MaintenanceActivity_Tenant_TenantID]
GO
ALTER TABLE [dbo].[MaintenanceActivity]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceActivity_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[MaintenanceActivity] CHECK CONSTRAINT [FK_MaintenanceActivity_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[MaintenanceActivity]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceActivity_TreatmentBMP_TreatmentBMPID_TenantID] FOREIGN KEY([TreatmentBMPID], [TenantID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TenantID])
GO
ALTER TABLE [dbo].[MaintenanceActivity] CHECK CONSTRAINT [FK_MaintenanceActivity_TreatmentBMP_TreatmentBMPID_TenantID]