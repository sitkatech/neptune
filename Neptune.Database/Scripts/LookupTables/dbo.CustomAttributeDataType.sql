MERGE INTO dbo.CustomAttributeDataType AS Target
USING (VALUES
(1, 'String', 'String'),
(2, 'Integer', 'Integer'),
(3, 'Decimal', 'Decimal'),
(4, 'DateTime', 'Date/Time'),
(5, 'PickFromList', 'Pick One from List'),
(6, 'MultiSelect', 'Select Many from List')
)
AS Source (CustomAttributeDataTypeID, CustomAttributeDataTypeName, CustomAttributeDataTypeDisplayName)
ON Target.CustomAttributeDataTypeID = Source.CustomAttributeDataTypeID
WHEN MATCHED THEN
UPDATE SET
	CustomAttributeDataTypeName = Source.CustomAttributeDataTypeName,
	CustomAttributeDataTypeDisplayName = Source.CustomAttributeDataTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (CustomAttributeDataTypeID, CustomAttributeDataTypeName, CustomAttributeDataTypeDisplayName)
	VALUES (CustomAttributeDataTypeID, CustomAttributeDataTypeName, CustomAttributeDataTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;