delete from dbo.ObservationThresholdType
go

INSERT 
dbo.ObservationThresholdType (ObservationThresholdTypeID, ObservationThresholdTypeName, ObservationThresholdTypeDisplayName, SortOrder, ObservationThresholdTypeDescription) VALUES
 (1, N'DiscreteValue', N'Discrete Value', 10, 'Threshold is measured as an discrete value (e.g. 3 ft of sediment accumulation)'),
 (2, N'PercentFromBenchmark', N'Percent From Benchmark', 20, 'Threshold is measured as a departure from the Benchmark value (e.g. 10% less vegetative cover than the Benchmark value)'),
 (3, N'None', N'None', 30, 'No Threshold value for this Observation type (e.g. Observation is Pass/Fail)')
