delete from dbo.ObservationTypeCollectionMethod
go

INSERT 
dbo.ObservationTypeCollectionMethod (ObservationTypeCollectionMethodID, ObservationTypeCollectionMethodName, ObservationTypeCollectionMethodDisplayName, SortOrder) VALUES
 (1, N'DiscreteValue', N'Measure one or many discrete values', 10),
 (2, N'Rate', N'Measure one or many rates as discrete values or time/value pairs', 20),
 (3, N'PassFail', N'Record Observation as Pass/Fail', 30),
 (4, N'Percentage', N'Measure one or many percent values', 40)
