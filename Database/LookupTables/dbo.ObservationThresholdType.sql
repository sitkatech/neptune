delete from dbo.ObservationThresholdType
go

INSERT 
dbo.ObservationThresholdType (ObservationThresholdTypeID, ObservationThresholdTypeName, ObservationThresholdTypeDisplayName, SortOrder) VALUES
 (1, N'SpecificValue', N'Threshold is a specific value', 10),
 (2, N'RelativeToBenchmark', N'Threshold is a relative percent of the benchmark value', 20),
 (3, N'None', N'None', 30)
