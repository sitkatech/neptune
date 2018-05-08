SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPTypeAttributeType](
	[TreatmentBMPTypeAttributeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[CustomAttributeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPTypeAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeAttributeTypeID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_CustomAttributeTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeID] ASC,
	[CustomAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAttributeType_CustomAttributeType_CustomAttributeTypeID] FOREIGN KEY([CustomAttributeTypeID])
REFERENCES [dbo].[CustomAttributeType] ([CustomAttributeTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAttributeType_CustomAttributeType_CustomAttributeTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAttributeType_CustomAttributeType_CustomAttributeTypeID_TenantID] FOREIGN KEY([CustomAttributeTypeID], [TenantID])
REFERENCES [dbo].[CustomAttributeType] ([CustomAttributeTypeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAttributeType_CustomAttributeType_CustomAttributeTypeID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAttributeType_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAttributeType_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAttributeType_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAttributeType_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAttributeType_TreatmentBMPType_TreatmentBMPTypeID_TenantID] FOREIGN KEY([TreatmentBMPTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAttributeType_TreatmentBMPType_TreatmentBMPTypeID_TenantID]