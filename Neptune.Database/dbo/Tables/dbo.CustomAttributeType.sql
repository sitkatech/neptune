CREATE TABLE [dbo].[CustomAttributeType](
	[CustomAttributeTypeID] [int] IDENTITY(1,1) NOT NULL,
	[CustomAttributeTypeName] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CustomAttributeDataTypeID] [int] NOT NULL,
	[MeasurementUnitTypeID] [int] NULL,
	[IsRequired] [bit] NOT NULL,
	[CustomAttributeTypeDescription] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CustomAttributeTypePurposeID] [int] NOT NULL,
	[CustomAttributeTypeOptionsSchema] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_CustomAttributeType_CustomAttributeTypeID] PRIMARY KEY CLUSTERED 
(
	[CustomAttributeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_CustomAttributeType_CustomAttributeTypeName] UNIQUE NONCLUSTERED 
(
	[CustomAttributeTypeName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[CustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttributeType_CustomAttributeDataType_CustomAttributeDataTypeID] FOREIGN KEY([CustomAttributeDataTypeID])
REFERENCES [dbo].[CustomAttributeDataType] ([CustomAttributeDataTypeID])
GO
ALTER TABLE [dbo].[CustomAttributeType] CHECK CONSTRAINT [FK_CustomAttributeType_CustomAttributeDataType_CustomAttributeDataTypeID]
GO
ALTER TABLE [dbo].[CustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttributeType_CustomAttributeTypePurpose_CustomAttributeTypePurposeID] FOREIGN KEY([CustomAttributeTypePurposeID])
REFERENCES [dbo].[CustomAttributeTypePurpose] ([CustomAttributeTypePurposeID])
GO
ALTER TABLE [dbo].[CustomAttributeType] CHECK CONSTRAINT [FK_CustomAttributeType_CustomAttributeTypePurpose_CustomAttributeTypePurposeID]
GO
ALTER TABLE [dbo].[CustomAttributeType]  WITH CHECK ADD  CONSTRAINT [FK_CustomAttributeType_MeasurementUnitType_MeasurementUnitTypeID] FOREIGN KEY([MeasurementUnitTypeID])
REFERENCES [dbo].[MeasurementUnitType] ([MeasurementUnitTypeID])
GO
ALTER TABLE [dbo].[CustomAttributeType] CHECK CONSTRAINT [FK_CustomAttributeType_MeasurementUnitType_MeasurementUnitTypeID]
GO
ALTER TABLE [dbo].[CustomAttributeType]  WITH CHECK ADD  CONSTRAINT [CK_CustomAttributeType_PickListTypeOptionSchemaNotNull] CHECK  ((NOT ([CustomAttributeDataTypeID]=(6) OR [CustomAttributeDataTypeID]=(5)) AND [CustomAttributeTypeOptionsSchema] IS NULL OR ([CustomAttributeDataTypeID]=(6) OR [CustomAttributeDataTypeID]=(5)) AND [CustomAttributeTypeOptionsSchema] IS NOT NULL))
GO
ALTER TABLE [dbo].[CustomAttributeType] CHECK CONSTRAINT [CK_CustomAttributeType_PickListTypeOptionSchemaNotNull]