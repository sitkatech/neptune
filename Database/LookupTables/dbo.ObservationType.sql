delete from dbo.ObservationType
go

INSERT dbo.ObservationType (ObservationTypeID, ObservationTypeName, ObservationTypeDisplayName, SortOrder, MeasurementUnitTypeID, HasBenchmarkAndThreshold, ThresholdPercentDecline, ThresholdPercentDeviation)
VALUES (1, N'InfiltrationRate', N'Infiltration Rate', 10, 20, 1, 1, 0),
(2, N'VegetativeCover', N'Vegetative Cover', 20, 11, 1, 0, 0),
(3, N'MaterialAccumulation', N'Material Accumulation', 30, 19, 1, 0, 0),
(4, N'VaultCapacity', N'Vault Capacity', 40, 19, 1, 1, 0),
(5, N'StandingWater', N'Standing Water', 50, 22, 0, 0, 0),
(6, N'Runoff', N'Runoff', 60, 21, 1, 0, 0),
(8, N'SedimentTrapCapacity', N'Sediment Trap Capacity', 70, 19, 1, 0, 0),
(9, N'WetBasinVegetativeCover', N'Vegetative Cover', 90, 11, 1, 0, 1),
(10, N'ConveyanceFunction', N'Conveyance Function', 100, 22, 0, 0, 0),
(11, N'Installation', N'Installation', 91, 22, 0, 0, 0)
