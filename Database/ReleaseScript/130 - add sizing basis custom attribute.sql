insert into CustomAttributeType([CustomAttributeTypeName], [CustomAttributeDataTypeID], [MeasurementUnitTypeID], [IsRequired], [CustomAttributeTypeDescription], [CustomAttributeTypePurposeID], [CustomAttributeTypeOptionsSchema])
values ('Sizing Basis',5, NULL, 0, NULL, 1, '["Trash Capture","Water Quality"]')


declare @SizingBasis int;
select @SizingBasis = CustomAttributeTypeID from neptune.dbo.CustomAttributeType where CustomAttributeTypeName = 'Sizing Basis'

Insert into dbo.TreatmentBMPTypeCustomAttributeType([TreatmentBMPTypeID], [CustomAttributeTypeID])
values
(22, @SizingBasis), (26, @SizingBasis), (35, @SizingBasis)