MERGE INTO dbo.TrashCaptureStatusType AS Target
USING (VALUES
(1, 'Full', 'Full', 10, 1, '18AF18'),
(2, 'Partial', 'Partial (>5mm but less than full sizing)', 20, 2, '5289FF'),
(3, 'None', 'No Trash Capture', 30, 3, '3D3D3E'),
(4, 'NotProvided', 'Not Provided', 40, 4, '878688')
)
AS Source (TrashCaptureStatusTypeID, TrashCaptureStatusTypeName, TrashCaptureStatusTypeDisplayName, TrashCaptureStatusTypeSortOrder, TrashCaptureStatusTypePriority, TrashCaptureStatusTypeColorCode)
ON Target.TrashCaptureStatusTypeID = Source.TrashCaptureStatusTypeID
WHEN MATCHED THEN
UPDATE SET
	TrashCaptureStatusTypeName = Source.TrashCaptureStatusTypeName,
	TrashCaptureStatusTypeDisplayName = Source.TrashCaptureStatusTypeDisplayName,
	TrashCaptureStatusTypeSortOrder = Source.TrashCaptureStatusTypeSortOrder,
	TrashCaptureStatusTypePriority = Source.TrashCaptureStatusTypePriority,
	TrashCaptureStatusTypeColorCode = Source.TrashCaptureStatusTypeColorCode
WHEN NOT MATCHED BY TARGET THEN
	INSERT (TrashCaptureStatusTypeID, TrashCaptureStatusTypeName, TrashCaptureStatusTypeDisplayName, TrashCaptureStatusTypeSortOrder, TrashCaptureStatusTypePriority, TrashCaptureStatusTypeColorCode)
	VALUES (TrashCaptureStatusTypeID, TrashCaptureStatusTypeName, TrashCaptureStatusTypeDisplayName, TrashCaptureStatusTypeSortOrder, TrashCaptureStatusTypePriority, TrashCaptureStatusTypeColorCode)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;