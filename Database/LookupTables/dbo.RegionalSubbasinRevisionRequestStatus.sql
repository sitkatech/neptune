delete from dbo.RegionalSubbasinRevisionRequestStatus

insert into dbo.RegionalSubbasinRevisionRequestStatus
	(RegionalSubbasinRevisionRequestStatusID, RegionalSubbasinRevisionRequestStatusName, RegionalSubbasinRevisionRequestStatusDisplayName)
VALUES
(1, 'Open', 'Open'),
(2, 'Closed', 'Closed')