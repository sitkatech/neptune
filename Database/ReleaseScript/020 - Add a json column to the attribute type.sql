alter table dbo.TreatmentBMPAttributeType add TreatmentBMPAttributeTypeOptionsSchema varchar(max) null
go

ALTER TABLE dbo.TreatmentBMPAttributeType  WITH CHECK ADD  CONSTRAINT CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull CHECK  ((TreatmentBMPAttributeDataTypeID != 5 AND TreatmentBMPAttributeTypeOptionsSchema IS NULL) OR (TreatmentBMPAttributeDataTypeID = 5 AND TreatmentBMPAttributeTypeOptionsSchema IS NOT NULL))
GO
ALTER TABLE dbo.TreatmentBMPAttributeType CHECK CONSTRAINT CK_TreatmentBMPAttributeType_PickListTypeOptionSchemaNotNull
GO
