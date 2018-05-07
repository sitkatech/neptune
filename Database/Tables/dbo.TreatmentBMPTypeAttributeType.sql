SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPTypeAttributeType](
	[TreatmentBMPTypeAttributeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[TreatmentBMPAttributeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPTypeAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeAttributeTypeID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPTypeID] ASC,
	[TreatmentBMPAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAttributeType_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAttributeType_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAttributeType_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID] FOREIGN KEY([TreatmentBMPAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPAttributeType] ([TreatmentBMPAttributeTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAttributeType_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPTypeAttributeType_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID] FOREIGN KEY([TreatmentBMPAttributeTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPAttributeType] ([TreatmentBMPAttributeTypeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPTypeAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPTypeAttributeType_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID]
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