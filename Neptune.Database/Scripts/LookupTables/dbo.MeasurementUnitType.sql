MERGE INTO dbo.MeasurementUnitType AS Target
USING (VALUES
(1, 'Acres', 'acres', 'acres', 'Acre', 2, 1),
(2, 'SquareFeet', 'square feet', 'sq ft', 'Square Foot', 2, 1),
(3, 'Kilogram', 'kg', 'kg', 'Kilogram', 2, 1),
(4, 'Count', 'count', 'count', 'Each Unit', 0, 1),
(5, 'Percent', '%', '%', '%', 0, 0),
(6, 'MilligamsPerLiter', 'mg/L', 'mg/L', 'Milligram Per Liter', 2, 1),
(7, 'Meters', 'meters', 'meters', 'Meter', 1, 1),
(8, 'Feet', 'feet', 'ft', 'Foot', 2, 1),
(9, 'Inches', 'inches', 'in', 'inch', 2, 1),
(10, 'InchesPerHour', 'in/hr', 'in/hr', 'Inches Per Hour', 2, 1),
(11, 'Seconds', 'seconds', 's', 'Second', 0, 1),
(12, 'PercentDecline', '% decline from benchmark', '% decline from benchmark', '% decline from benchmark', 0, 0),
(13, 'PercentIncrease', '% increase from benchmark', '% increase from benchmark', '% increase from benchmark', 0, 0),
(14, 'PercentDeviation', '% of benchmark', '% of benchmark', '% of benchmark', 0, 0),
(15, 'Cubic Feet', 'cubic feet', 'cu ft', 'cu ft', 0, 1),
(16, 'Gallons', 'gallons', 'gallons', 'gallon', 0, 1),
(17, 'Minutes', 'minutes', 'minutes', 'minute', 0, 1),
(18, 'CubicFeetPerSecond', 'cubic feet per second', 'cfs', 'cfs', 0, 1),
(19, 'GallonsPerDay', 'gallons per day', 'gpd', 'gallon per day', 1, 1),
(20, 'Pounds', 'pounds', 'lb', 'pound', 1, 1),
(21, 'Tons', 'tons', 'cfs', 'cfs', 1, 1),
(22, 'CubicYards', 'cubic yards', 'cu yd', 'cubic yard', 1, 1)
)
AS Source (MeasurementUnitTypeID, MeasurementUnitTypeName, MeasurementUnitTypeDisplayName, LegendDisplayName, SingularDisplayName, NumberOfSignificantDigits, IncludeSpaceBeforeLegendLabel)
ON Target.MeasurementUnitTypeID = Source.MeasurementUnitTypeID
WHEN MATCHED THEN
UPDATE SET
	MeasurementUnitTypeName = Source.MeasurementUnitTypeName,
	MeasurementUnitTypeDisplayName = Source.MeasurementUnitTypeDisplayName,
	LegendDisplayName = Source.LegendDisplayName,
	SingularDisplayName = Source.SingularDisplayName,
	NumberOfSignificantDigits = Source.NumberOfSignificantDigits,
	IncludeSpaceBeforeLegendLabel = Source.IncludeSpaceBeforeLegendLabel
WHEN NOT MATCHED BY TARGET THEN
	INSERT (MeasurementUnitTypeID, MeasurementUnitTypeName, MeasurementUnitTypeDisplayName, LegendDisplayName, SingularDisplayName, NumberOfSignificantDigits, IncludeSpaceBeforeLegendLabel)
	VALUES (MeasurementUnitTypeID, MeasurementUnitTypeName, MeasurementUnitTypeDisplayName, LegendDisplayName, SingularDisplayName, NumberOfSignificantDigits, IncludeSpaceBeforeLegendLabel)
WHEN NOT MATCHED BY SOURCE THEN
	DELETE;