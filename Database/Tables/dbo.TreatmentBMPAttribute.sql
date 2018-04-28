SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPAttribute](
	[TreatmentBMPAttributeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPID] [int] NOT NULL,
	[TreatmentBMPTypeAttributeTypeID] [int] NOT NULL,
	[TreatmentBMPTypeID] [int] NOT NULL,
	[TreatmentBMPAttributeTypeID] [int] NOT NULL,
	[TreatmentBMPAttributeValue] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TreatmentBMPAttribute_TreatmentBMPAttributeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPAttributeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID] FOREIGN KEY([TreatmentBMPID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TenantID] FOREIGN KEY([TreatmentBMPID], [TenantID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPID], [TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMP] ([TreatmentBMPID], [TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMP_TreatmentBMPID_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID] FOREIGN KEY([TreatmentBMPAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPAttributeType] ([TreatmentBMPAttributeTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID] FOREIGN KEY([TreatmentBMPAttributeTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPAttributeType] ([TreatmentBMPAttributeTypeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID] FOREIGN KEY([TreatmentBMPTypeID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID_TenantID] FOREIGN KEY([TreatmentBMPTypeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPType] ([TreatmentBMPTypeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPType_TreatmentBMPTypeID_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID] FOREIGN KEY([TreatmentBMPTypeAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPTypeAttributeType] ([TreatmentBMPTypeAttributeTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeAttributeTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID] FOREIGN KEY([TreatmentBMPTypeID], [TreatmentBMPAttributeTypeID])
REFERENCES [dbo].[TreatmentBMPTypeAttributeType] ([TreatmentBMPTypeID], [TreatmentBMPAttributeTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttribute] CHECK CONSTRAINT [FK_TreatmentBMPAttribute_TreatmentBMPTypeAttributeType_TreatmentBMPTypeID_TreatmentBMPAttributeTypeID]