delete from dbo.FieldVisitStatus

insert into dbo.FieldVisitStatus(FieldVisitStatusID, FieldVisitStatusName, FieldVisitStatusDisplayName)
values
(1, 'InProgress', 'In Progress'),
(2, 'Complete', 'Complete'),
(3, 'Unresolved', 'Unresolved'),
(4, 'ReturnedToEdit', 'Returned to Edit')