insert into dbo.FieldDefinitionType (FieldDefinitionTypeID, FieldDefinitionTypeName, FieldDefinitionTypeDisplayName)
values
	(137, 'LandUseBasedWaterQualityScore', 'Land Use Based Water Quality Score'),
	(138, 'ReceivingWaterScore', 'Receiving Water Score')

go

insert into dbo.FieldDefinition (FieldDefinitionTypeID, FieldDefinitionValue)
values 
	(137, 'Land Use Based Water Quality Score'),
	(138, 'Receiving Water Score')