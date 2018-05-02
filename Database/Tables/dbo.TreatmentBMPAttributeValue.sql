SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPAttributeValue](
	[TreatmentBMPAttributeValueID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPAttributeID] [int] NOT NULL,
	[AttributeValue] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
 CONSTRAINT [PK_TreatmentBMPAttributeValue_TreatmentBMPAttributeValueID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPAttributeValueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttributeValue_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeValue] CHECK CONSTRAINT [FK_TreatmentBMPAttributeValue_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttributeValue_TreatmentBMPAttribute_TreatmentBMPAttributeID] FOREIGN KEY([TreatmentBMPAttributeID])
REFERENCES [dbo].[TreatmentBMPAttribute] ([TreatmentBMPAttributeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeValue] CHECK CONSTRAINT [FK_TreatmentBMPAttributeValue_TreatmentBMPAttribute_TreatmentBMPAttributeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeValue]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttributeValue_TreatmentBMPAttribute_TreatmentBMPAttributeID_TenantID] FOREIGN KEY([TreatmentBMPAttributeID], [TenantID])
REFERENCES [dbo].[TreatmentBMPAttribute] ([TreatmentBMPAttributeID], [TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeValue] CHECK CONSTRAINT [FK_TreatmentBMPAttributeValue_TreatmentBMPAttribute_TreatmentBMPAttributeID_TenantID]