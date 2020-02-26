Alter Table dbo.RegionalSubbasinRevisionRequest
Add Constraint CK_RegionalSubbasinRevisionRequest_ClosedReqMustHaveCloseDate
Check (NOT (RegionalSubbasinRevisionRequestStatusID = 2 AND ClosedDate is null))
