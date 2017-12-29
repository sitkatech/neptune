delete from dbo.ObservationTypeSpecification
go

INSERT 
dbo.ObservationTypeSpecification (ObservationTypeSpecificationID, ObservationTypeSpecificationName, ObservationTypeSpecificationDisplayName, SortOrder, ObservationTypeCollectionMethodID, ObservationTargetTypeID, ObservationThresholdTypeID) VALUES
(1, N'YesNo_YesNo_None', N' YesNo_YesNo_None', 10, 3, 1, 3),
(2, N'SingleValue_HighTargetValue_Absolute', N' SingleValue_HighTargetValue_Absolute', 20, 1, 2, 1),
(3, N'MultipleTimeValue_HighTargetValue_Absolute', N' MultipleTimeValue_HighTargetValue_Absolute', 30, 2, 2, 1),
(4, N'YesNo_HighTargetValue_Absolute', N' YesNo_HighTargetValue_Absolute', 40, 3, 2, 1),
(5, N'PercentValue_HighTargetValue_Absolute', N' PercentValue_HighTargetValue_Absolute', 50, 4, 2, 1),
(6, N'SingleValue_HighTargetValue_PercentFromBenchmark', N' SingleValue_HighTargetValue_PercentFromBenchmark', 60, 1, 2, 2),
(7, N'MultipleTimeValue_HighTargetValue_PercentFromBenchmark', N' MultipleTimeValue_HighTargetValue_PercentFromBenchmark', 70, 2, 2, 2),
(8, N'PercentValue_HighTargetValue_PercentFromBenchmark', N' PercentValue_HighTargetValue_PercentFromBenchmark', 80, 4, 2, 2),
(9, N'SingleValue_LowTargetValue_Absolute', N' SingleValue_LowTargetValue_Absolute', 90, 1, 3, 1),
(10, N'MultipleTimeValue_LowTargetValue_Absolute', N' MultipleTimeValue_LowTargetValue_Absolute', 100, 2, 3, 1),
(11, N'YesNo_LowTargetValue_Absolute', N' YesNo_LowTargetValue_Absolute', 110, 3, 3, 1),
(12, N'PercentValue_LowTargetValue_Absolute', N' PercentValue_LowTargetValue_Absolute', 120, 4, 3, 1),
(13, N'SingleValue_LowTargetValue_PercentFromBenchmark', N' SingleValue_LowTargetValue_PercentFromBenchmark', 130, 1, 3, 2),
(14, N'MultipleTimeValue_LowTargetValue_PercentFromBenchmark', N' MultipleTimeValue_LowTargetValue_PercentFromBenchmark', 140, 2, 3, 2),
(15, N'PercentValue_LowTargetValue_PercentFromBenchmark', N' PercentValue_LowTargetValue_PercentFromBenchmark', 150, 4, 3, 2),
(16, N'SingleValue_TargetValue_Absolute', N' SingleValue_TargetValue_Absolute', 160, 1, 4, 1),
(17, N'MultipleTimeValue_TargetValue_Absolute', N' MultipleTimeValue_TargetValue_Absolute', 170, 2, 4, 1),
(18, N'YesNo_TargetValue_Absolute', N' YesNo_TargetValue_Absolute', 180, 3, 4, 1),
(19, N'PercentValue_TargetValue_Absolute', N' PercentValue_TargetValue_Absolute', 190, 4, 4, 1),
(20, N'SingleValue_TargetValue_PercentFromBenchmark', N' SingleValue_TargetValue_PercentFromBenchmark', 200, 1, 4, 2),
(21, N'MultipleTimeValue_TargetValue_PercentFromBenchmark', N' MultipleTimeValue_TargetValue_PercentFromBenchmark', 210, 2, 4, 2),
(22, N'PercentValue_TargetValue_PercentFromBenchmark', N' PercentValue_TargetValue_PercentFromBenchmark', 220, 4, 4, 2)
