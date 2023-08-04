MERGE INTO dbo.SizingBasisType AS Target
USING (VALUES
(1, 'FullTrashCapture', 'Full Trash Capture'),
(2, 'WaterQuality', 'Water Quality'),
(3, 'Other', 'Other (less than Water Quality)'),
(4, 'NotProvided', 'Not Provided')
)
AS Source (SizingBasisTypeID, SizingBasisTypeName, SizingBasisTypeDisplayName)
ON Target.SizingBasisTypeID = Source.SizingBasisTypeID
WHEN MATCHED THEN
UPDATE SET
	SizingBasisTypeName = Source.SizingBasisTypeName,
	SizingBasisTypeDisplayName = Source.SizingBasisTypeDisplayName
WHEN NOT MATCHED BY TARGET THEN
	INSERT (SizingBasisTypeID, SizingBasisTypeName, SizingBasisTypeDisplayName)
	VALUES (SizingBasisTypeID, SizingBasisTypeName, SizingBasisTypeDisplayName)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;