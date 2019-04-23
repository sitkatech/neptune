delete from dbo.TrashCaptureStatusType

insert into dbo.TrashCaptureStatusType (TrashCaptureStatusTypeID, TrashCaptureStatusTypeName, TrashCaptureStatusTypeDisplayName, TrashCaptureStatusTypeSortOrder, TrashCaptureStatusTypePriority, TrashCaptureStatusTypeColorCode)
values
(1, 'Full', 'Full', 10, 1, '18af18'),
(2, 'Partial', 'Partial (>5mm but less than full sizing)', 20, 2, '5289ff'),
(3, 'None', 'No Trash Capture', 30, 3, '3d3d3e'),
(4, 'NotProvided', 'Not Provided', 40, 4, '878688')