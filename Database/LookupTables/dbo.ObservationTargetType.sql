delete from dbo.ObservationTargetType
go

INSERT 
dbo.ObservationTargetType (ObservationTargetTypeID, ObservationTargetTypeName, ObservationTargetTypeDisplayName, SortOrder) VALUES
 (1, N'PassFail', N'Observation is Pass/Fail', 10),
 (2, N'High', N'Higher observed values result in higher score', 20),
 (3, N'Low', N'Lower observed values result in higher score', 30),
 (4, N'SpecificValue', N'Observed values exactly equal to the benchmark result in highest score', 40)