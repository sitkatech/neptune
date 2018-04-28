SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TreatmentBMPAttributeType](
	[TreatmentBMPAttributeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[TenantID] [int] NOT NULL,
	[TreatmentBMPAttributeTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TreatmentBMPAttributeDataTypeID] [int] NOT NULL,
	[MeasurementUnitTypeID] [int] NULL,
	[IsRequired] [bit] NOT NULL,
	[TreatmentBMPAttributeTypeDescription] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TreatmentBMPAttributeTypePurposeID] [int] NOT NULL,
	[TreatmentBMPAttributeTypeOptionsSchema] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID] PRIMARY KEY CLUSTERED 
(
	[TreatmentBMPAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeID_TenantID] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAttributeTypeID] ASC,
	[TenantID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypeName] UNIQUE NONCLUSTERED 
(
	[TreatmentBMPAttributeTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttributeType_MeasurementUnitType_MeasurementUnitTypeID] FOREIGN KEY([MeasurementUnitTypeID])
REFERENCES [dbo].[MeasurementUnitType] ([MeasurementUnitTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPAttributeType_MeasurementUnitType_MeasurementUnitTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttributeType_Tenant_TenantID] FOREIGN KEY([TenantID])
REFERENCES [dbo].[Tenant] ([TenantID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPAttributeType_Tenant_TenantID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttributeType_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeID] FOREIGN KEY([TreatmentBMPAttributeDataTypeID])
REFERENCES [dbo].[TreatmentBMPAttributeDataType] ([TreatmentBMPAttributeDataTypeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPAttributeType_TreatmentBMPAttributeDataType_TreatmentBMPAttributeDataTypeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypePurpose_TreatmentBMPAttributeTypePurposeID] FOREIGN KEY([TreatmentBMPAttributeTypePurposeID])
REFERENCES [dbo].[TreatmentBMPAttributeTypePurpose] ([TreatmentBMPAttributeTypePurposeID])
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType] CHECK CONSTRAINT [FK_TreatmentBMPAttributeType_TreatmentBMPAttributeTypePurpose_TreatmentBMPAttributeTypePurposeID]
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType]  WITH CHECK ADD  CONSTRAINT [CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull] CHECK  ((NOT ([TreatmentBMPAttributeDataTypeID]=(6) OR [TreatmentBMPAttributeDataTypeID]=(5)) AND [TreatmentBMPAttributeTypeOptionsSchema] IS NULL OR ([TreatmentBMPAttributeDataTypeID]=(6) OR [TreatmentBMPAttributeDataTypeID]=(5)) AND [TreatmentBMPAttributeTypeOptionsSchema] IS NOT NULL))
GO
ALTER TABLE [dbo].[TreatmentBMPAttributeType] CHECK CONSTRAINT [CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull]