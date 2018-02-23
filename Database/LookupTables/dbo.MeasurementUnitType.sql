delete from dbo.MeasurementUnitType

insert into dbo.MeasurementUnitType(MeasurementUnitTypeID, MeasurementUnitTypeName, MeasurementUnitTypeDisplayName, LegendDisplayName, SingularDisplayName, NumberOfSignificantDigits, IncludeSpaceBeforeLegendLabel) values 
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
(12, 'PercentDecline', '% decline', '% decline', '% decline', 0, 0),
(13, 'PercentIncrease', '% increase', '% increase', '% increase', 0, 0),
(14, 'PercentDeviation', '% deviation', '% deviation', '% deviation', 0, 0),
(15, 'Cubic Feet', 'cubic feet', 'cu ft', 'cu ft', 0, 0),
(16, 'Gallons', 'gallons', 'gallons', 'gallon', 0, 0),
(17, 'Minutes', 'minutes', 'minutes', 'minute', 0, 0),
(18, 'CubicFeetPerSecond', 'cubic feet per second', 'cfs', 'cfs', 0, 0)
