delete from dbo.ObservationTypeSpecification
go

INSERT 
dbo.ObservationTypeSpecification (ObservationTypeSpecificationID, ObservationTypeSpecificationName, ObservationTypeSpecificationDisplayName, SortOrder, ObservationTypeCollectionMethodID, ObservationTargetTypeID, ObservationThresholdTypeID) VALUES
(1, N'PassFail_PassFail_None', N' PassFail_PassFail_None', 10, 3, 1, 3),

(2, N'DiscreteValues_HighTargetValue_DiscreteThresholdValue', N' DiscreteValues_HighTargetValue_DiscreteThresholdValue', 20, 1, 2, 1),
(3, N'DiscreteValues_HighTargetValue_PercentFromBenchmark', N' DiscreteValues_HighTargetValue_PercentFromBenchmark', 30, 1, 2, 2),
(4, N'DiscreteValues_LowTargetValue_DiscreteThresholdValue', N' DiscreteValues_LowTargetValue_DiscreteThresholdValue', 40, 1, 3, 1),
(5, N'DiscreteValues_LowTargetValue_PercentFromBenchmark', N' DiscreteValues_LowTargetValue_PercentFromBenchmark', 50, 1, 3, 2),
(6, N'DiscreteValues_SpecificTargetValue_DiscreteThresholdValue', N' DiscreteValues_SpecificTargetValue_DiscreteThresholdValue', 60, 1, 4, 1),
(7, N'DiscreteValues_SpecificTargetValue_PercentFromBenchmark', N' DiscreteValues_SpecificTargetValue_PercentFromBenchmark', 70, 1, 4, 2),

--(8, N'MultipleTimeValuePairs_HighTargetValue_DiscreteThresholdValue', N' MultipleTimeValuePairs_HighTargetValue_DiscreteThresholdValue', 80, 2, 2, 1),
--(9, N'MultipleTimeValuePairs_HighTargetValue_PercentFromBenchmark', N' MultipleTimeValuePairs_HighTargetValue_PercentFromBenchmark', 90, 2, 2, 2),
--(10, N'MultipleTimeValuePairs_LowTargetValue_DiscreteThresholdValue', N' MultipleTimeValuePairs_LowTargetValue_DiscreteThresholdValue', 100, 2, 3, 1),
--(11, N'MultipleTimeValuePairs_LowTargetValue_PercentFromBenchmark', N' MultipleTimeValuePairs_LowTargetValue_PercentFromBenchmark', 110, 2, 3, 2),
--(12, N'MultipleTimeValuePairs_SpecificTargetValue_DiscreteThresholdValue', N' MultipleTimeValuePairs_SpecificTargetValue_DiscreteThresholdValue', 120, 2, 4, 1),
--(13, N'MultipleTimeValuePairs_SpecificTargetValue_PercentFromBenchmark', N' MultipleTimeValuePairs_SpecificTargetValue_PercentFromBenchmark', 130, 2, 4, 2),

(14, N'PercentValue_HighTargetValue_DiscreteThresholdValue', N' PercentValue_HighTargetValue_DiscreteThresholdValue', 140, 4, 2, 1),
(15, N'PercentValue_HighTargetValue_PercentFromBenchmark', N' PercentValue_HighTargetValue_PercentFromBenchmark', 150, 4, 2, 2),
(16, N'PercentValue_LowTargetValue_DiscreteThresholdValue', N' PercentValue_LowTargetValue_DiscreteThresholdValue', 160, 4, 3, 1),
(17, N'PercentValue_LowTargetValue_PercentFromBenchmark', N' PercentValue_LowTargetValue_PercentFromBenchmark', 170, 4, 3, 2),
(18, N'PercentValue_SpecificTargetValue_DiscreteThresholdValue', N' PercentValue_SpecificTargetValue_DiscreteThresholdValue', 180, 4, 4, 1),
(19, N'PercentValue_SpecificTargetValue_PercentFromBenchmark', N' PercentValue_SpecificTargetValue_PercentFromBenchmark', 190, 4, 4, 2)
