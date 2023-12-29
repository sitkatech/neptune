MERGE INTO dbo.CustomAttributeDataType AS Target
USING (VALUES
(1, 'String', 'String', 0, 0),
(2, 'Integer', 'Integer', 0, 1),
(3, 'Decimal', 'Decimal', 0, 1),
(4, 'DateTime', 'Date/Time', 0, 0),
(5, 'PickFromList', 'Pick One from List', 1, 0),
(6, 'MultiSelect', 'Select Many from List', 1, 0)
)
AS Source (CustomAttributeDataTypeID, CustomAttributeDataTypeName, CustomAttributeDataTypeDisplayName, HasOptions, HasMeasurementUnit)
ON Target.CustomAttributeDataTypeID = Source.CustomAttributeDataTypeID
WHEN MATCHED THEN
UPDATE SET
	CustomAttributeDataTypeName = Source.CustomAttributeDataTypeName,
	CustomAttributeDataTypeDisplayName = Source.CustomAttributeDataTypeDisplayName,
	HasOptions = Source.HasOptions,
	HasMeasurementUnit = Source.HasMeasurementUnit
WHEN NOT MATCHED BY TARGET THEN
	INSERT (CustomAttributeDataTypeID, CustomAttributeDataTypeName, CustomAttributeDataTypeDisplayName, HasOptions, HasMeasurementUnit)
	VALUES (CustomAttributeDataTypeID, CustomAttributeDataTypeName, CustomAttributeDataTypeDisplayName, HasOptions, HasMeasurementUnit)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;