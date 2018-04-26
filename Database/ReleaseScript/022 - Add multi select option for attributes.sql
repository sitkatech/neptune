ALTER TABLE dbo.TreatmentBMPAttributeType  drop  CONSTRAINT CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull

go

ALTER TABLE dbo.TreatmentBMPAttributeType  WITH CHECK ADD  CONSTRAINT CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull CHECK  ((TreatmentBMPAttributeDataTypeID not in (5, 6) AND TreatmentBMPAttributeTypeOptionsSchema IS NULL) OR (TreatmentBMPAttributeDataTypeID in (5, 6) AND TreatmentBMPAttributeTypeOptionsSchema IS NOT NULL))
GO
ALTER TABLE dbo.TreatmentBMPAttributeType CHECK CONSTRAINT CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull
GO

ALTER TABLE [dbo].[TreatmentBMPAttribute] DROP CONSTRAINT [AK_TreatmentBMPAttribute_TreatmentBMPTypeID_TreatmentBMPTypeAttributeTypeID]
GO
