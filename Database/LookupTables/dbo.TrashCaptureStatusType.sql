delete from dbo.TrashCaptureStatusType

insert into dbo.TrashCaptureStatusType (TrashCaptureStatusTypeID, TrashCaptureStatusTypeName, TrashCaptureStatusTypeDisplayName, TrashCaptureStatusTypeSortOrder)
values
(1, 'Full', 'Full', 10),
(2, 'Partial', 'Partial', 20),
(3, 'None', 'None', 30),
(4, 'NotProvided', 'Not Provided', 40)