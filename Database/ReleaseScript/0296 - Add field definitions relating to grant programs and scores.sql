insert into dbo.FieldDefinitionType (FieldDefinitionTypeID, FieldDefinitionTypeName, FieldDefinitionTypeDisplayName)
values
	(117, 'OCTA M2 Tier 2 Grant Program', 'OCTA M2 Tier 2 Grant Program'),
	(118, 'SEA Score', 'SEA Score'),
	(119, 'TPI Score', 'TPI Score'),
	(120, 'WQLRI', 'WQLRI'),
	(121, 'Pollutant Contribution to SEA', 'Pollutant Contribution to SEA')

go

insert into dbo.FieldDefinition (FieldDefinitionTypeID, FieldDefinitionValue)
values 
	(117, 'OCTA M2 Tier 2 Grant Program'),
	(118, 'SEA Score'),
	(119, 'TPI Score'),
	(120, 'WQLRI'),
	(121, 'Pollutant Contribution to SEA')