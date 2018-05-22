SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPTypeCustomAttributeType](
	[TreatmentBMPTypeCustomAttributeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[CustomAttributeTypeID] [int] NOT NULL,
	[SortOrder] [int] NULL,
 CONSTRAINT [PK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPTypeCustomAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeCustomAttributeTypeID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeCustomAttributeTypeID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeID] ASC,
	[CustomAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_CustomAttributeType_CustomAttributeTypeID] FOREIGN KEY([CustomAttributeTypeID])
REFERENCES [dbo].[CustomAttributeType] ([CustomAttributeTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_CustomAttributeType_CustomAttributeTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_CustomAttributeType_CustomAttributeTypeID_TenantID] FOREIGN KEY([CustomAttributeTypeID], [TenantID])
REFERENCES [dbo].[CustomAttributeType] ([CustomAttributeTypeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_CustomAttributeType_CustomAttributeTypeID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPType_TreatmentBMPTypeID_TenantID] FOREIGN KEY([TreatmentBMPTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeCustomAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeCustomAttributeType_TreatmentBMPType_TreatmentBMPTypeID_TenantID]