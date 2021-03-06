delete from dbo.ObservationTypeCollectionMethod
go

INSERT 
dbo.ObservationTypeCollectionMethod (ObservationTypeCollectionMethodID, ObservationTypeCollectionMethodName, ObservationTypeCollectionMethodDisplayName, SortOrder, ObservationTypeCollectionMethodDescription) VALUES
 (1, N'DiscreteValue', N'Discrete Value Observation', 10, 'Observation is measured as one or many discrete values (e.g. time, height).'),
 --(2, N'Rate', N'Rate-based Observation', 20, 'Observation is measured as one or many rates values or as time/value pairs (e.g. infiltration rate or infiltrometer readings at elapsed time intervals).'),
 (3, N'PassFail', N'Pass/Fail Observation', 30, 'Observation is recorded as Pass/Fail (e.g. presence of standing water).'),
 (4, N'Percentage', N'Percent-based Observation', 40, 'Observation is measured as one or more percent values that total to less than 100% (e.g. percent coverage of key species).')
