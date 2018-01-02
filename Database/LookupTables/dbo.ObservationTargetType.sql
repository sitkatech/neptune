delete from dbo.ObservationTargetType
go

INSERT 
dbo.ObservationTargetType (ObservationTargetTypeID, ObservationTargetTypeName, ObservationTargetTypeDisplayName, SortOrder, ObservationTargetTypeDescription) VALUES
 (1, N'PassFail', N'Pass/Fail', 10, 'Observation is pass/fail'),
 (2, N'High', N'High Target Value', 20, 'Observing a high value is good'),
 (3, N'Low', N'Low Target Value', 30, 'Observing a low value is good'),
 (4, N'SpecificValue', N'Specific Target Value', 40, 'Observing a specific targeted value is good')