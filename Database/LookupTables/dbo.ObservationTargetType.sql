delete from dbo.ObservationTargetType
go

INSERT 
dbo.ObservationTargetType (ObservationTargetTypeID, ObservationTargetTypeName, ObservationTargetTypeDisplayName, SortOrder, ObservationTargetTypeDescription) VALUES
 (1, N'YesNo', N'Yes/No', 10, 'Observation is pass/fail'),
 (2, N'HighTargetValue', N'High Target Value', 20, 'Observing a high value is good'),
 (3, N'LowTargetValue', N'Low Target Value', 30, 'Observing a low value is good'),
 (4, N'TargetValue', N'Target Value', 40, 'Observing a specific targeted value is good')