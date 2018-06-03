SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaintenanceRecord](
	[MaintenanceRecordID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[MaintenanceRecordDescription] [varchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaintenanceRecordTypeID] [int] NOT NULL,
 CONSTRAINT [PK_MaintenanceRecord_MaintenanceRecordID] PRIMARY KEY CLUSTERED 
(
	[MaintenanceRecordID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_MaintenanceRecord_MaintenanceRecordID_TenantID] UNIQUE NONCLUSTERED 
(
	[MaintenanceRecordID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_MaintenanceRecord_MaintenanceRecordID_TreatmentBMPID] UNIQUE NONCLUSTERED 
(
	[MaintenanceRecordID] ASC,
	[TreatmentBMPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[MaintenanceRecord]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecord_MaintenanceRecordType_MaintenanceRecordTypeID] FOREIGN KEY([MaintenanceRecordTypeID])
REFERENCES [dbo].[MaintenanceRecordType] ([MaintenanceRecordTypeID])
GO
ALTER TABLE [dbo].[MaintenanceRecord] CHECK CONSTRAINT [FK_MaintenanceRecord_MaintenanceRecordType_MaintenanceRecordTypeID]
GO
ALTER TABLE [dbo].[MaintenanceRecord]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecord_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[MaintenanceRecord] CHECK CONSTRAINT [FK_MaintenanceRecord_Tenant_TenantID]
GO
ALTER TABLE [dbo].[MaintenanceRecord]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[MaintenanceRecord] CHECK CONSTRAINT [FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[MaintenanceRecord]  WITH CHECK ADD  CONSTRAINT [FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID_TenantID] FOREIGN KEY([TreatmentBMPID], [TenantID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TenantID])
GO
ALTER TABLE [dbo].[MaintenanceRecord] CHECK CONSTRAINT [FK_MaintenanceRecord_TreatmentBMP_TreatmentBMPID_TenantID]