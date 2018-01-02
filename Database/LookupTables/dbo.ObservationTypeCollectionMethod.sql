delete from dbo.ObservationTypeCollectionMethod
go

INSERT 
dbo.ObservationTypeCollectionMethod (ObservationTypeCollectionMethodID, ObservationTypeCollectionMethodName, ObservationTypeCollectionMethodDisplayName, SortOrder) VALUES
 (1, N'DiscreteValue', N'Measure one or many discrete values', 10),
 (2, N'MultipleTimeValue', N'Measure one or many time/value pairs', 20),
 (3, N'PassFail', N'Record Obervation as Pass/Fail', 30),
 (4, N'PercentValue', N'Measure a single percent value', 40)
