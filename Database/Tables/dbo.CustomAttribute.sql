SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomAttribute](
	[CustomAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[TreatmentBMPTypeCustomAttributeTypeID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[CustomAttributeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_CustomAttribute_CustomAttributeID] PRIMARY KEY CLUSTERED 
(
	[CustomAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_CustomAttribute_CustomAttributeID_TenantID] UNIQUE NONCLUSTERED 
(
	[CustomAttributeID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_CustomAttribute_TreatmentBMPTypeID_TreatmentBMPTypeCustomAttributeTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPID] ASC,
	[TreatmentBMPTypeID] ASC,
	[CustomAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_CustomAttributeType_CustomAttributeTypeID] FOREIGN KEY([CustomAttributeTypeID])
REFERENCES [dbo].[CustomAttributeType] ([CustomAttributeTypeID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_CustomAttributeType_CustomAttributeTypeID]
GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_CustomAttributeType_CustomAttributeTypeID_TenantID] FOREIGN KEY([CustomAttributeTypeID], [TenantID])
REFERENCES [dbo].[CustomAttributeType] ([CustomAttributeTypeID], [TenantID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_CustomAttributeType_CustomAttributeTypeID_TenantID]
GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_Tenant_TenantID]
GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_TreatmentBMP_TreatmentBMPID_TenantID] FOREIGN KEY([TreatmentBMPID], [TenantID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TenantID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_TreatmentBMP_TreatmentBMPID_TenantID]
GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_TreatmentBMPType_TreatmentBMPTypeID_TenantID] FOREIGN KEY([TreatmentBMPTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID], [TenantID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_TreatmentBMPType_TreatmentBMPTypeID_TenantID]
GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID] FOREIGN KEY([TreatmentBMPTypeCustomAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPTypeCustomAttributeType] ([TreatmentBMPTypeCustomAttributeTypeID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID]
GO
ALTER TABLE [dbo].[CustomAttribute]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttribute_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID] FOREIGN KEY([TreatmentBMPTypeID], [CustomAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPTypeCustomAttributeType] ([TreatmentBMPTypeID], [CustomAttributeTypeID])
GO
ALTER TABLE [dbo].[CustomAttribute] CHECK CONSTRAINT [FK_CustomAttribute_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID]