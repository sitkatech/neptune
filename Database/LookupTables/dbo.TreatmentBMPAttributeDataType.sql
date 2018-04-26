delete from dbo.TreatmentBMPAttributeDataType
go

insert into dbo.TreatmentBMPAttributeDataType(TreatmentBMPAttributeDataTypeID, TreatmentBMPAttributeDataTypeName, TreatmentBMPAttributeDataTypeDisplayName)
values
(1, 'String', 'String'),
(2, 'Integer', 'Integer'),
(3, 'Decimal', 'Decimal'),
(4, 'DateTime', 'Date/Time'),
(5, 'PickFromList', 'Pick One from List'),
(6, 'MultiSelect', 'Select Many from List')