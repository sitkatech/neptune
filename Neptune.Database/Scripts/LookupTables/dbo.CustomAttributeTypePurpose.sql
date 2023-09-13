MERGE INTO dbo.CustomAttributeTypePurpose AS Target
USING (VALUES
(2, 'OtherDesignAttributes', 'Other Design Attributes'),
(3, 'Maintenance', 'Maintenance Attributes')
)
AS Source (CustomAttributeTypePurposeID, CustomAttributeTypePurposeName, CustomAttributeTypePurposeDisplayName)
ON Target.CustomAttributeTypePurposeID = Source.CustomAttributeTypePurposeID
WHEN MATCHED THEN
UPDATE SET
	CustomAttributeTypePurposeName = Source.CustomAttributeTypePurposeName,
	CustomAttributeTypePurposeDisplayName = Source.CustomAttributeTypePurposeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (CustomAttributeTypePurposeID, CustomAttributeTypePurposeName, CustomAttributeTypePurposeDisplayName)
	VALUES (CustomAttributeTypePurposeID, CustomAttributeTypePurposeName, CustomAttributeTypePurposeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;