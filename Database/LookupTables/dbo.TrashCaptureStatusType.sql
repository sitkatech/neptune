delete from dbo.TrashCaptureStatusType

insert into dbo.TrashCaptureStatusType (TrashCaptureStatusTypeID, TrashCaptureStatusTypeName, TrashCaptureStatusTypeDisplayName, TrashCaptureStatusTypeSortOrder, TrashCaptureStatusTypePriority)
values
(1, 'Full', 'Full', 10, 1),
(2, 'Partial', 'Partial (>5mm but less than full sizing)', 20, 2),
(3, 'None', 'Not a Trash Capture BMP', 30, 3),
(4, 'NotProvided', 'Not Provided', 40, 4)