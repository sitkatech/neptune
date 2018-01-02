delete from dbo.MeasurementUnitType

insert into dbo.MeasurementUnitType(MeasurementUnitTypeID, MeasurementUnitTypeName, MeasurementUnitTypeDisplayName, LegendDisplayName, SingularDisplayName, NumberOfSignificantDigits) values 
(1, 'Acres', 'acres', 'acres', 'Acre', 2),
(2, 'SquareFeet', 'square feet', 'sq ft', 'Square Foot', 2),
(3, 'Kilogram', 'kg', 'kg', 'Kilogram', 2),
(4, 'Number', 'number', null, 'Each Unit', 0),
(5, 'Percent', '%', '%', '%', 0),
(6, 'MilligamsPerLiter', 'mg/L', 'mg/L', 'Milligram Per Liter', 2),
(7, 'Meters', 'meters', 'meters', 'Meter', 1),
(8, 'Feet', 'feet', 'ft', 'Foot', 2),
(9, 'InchesPerHour', 'in/hr', 'in/hr', 'Inches Per Hour', 2),
(10, 'Seconds', 'seconds', 's', 'Second', 0),
(11, 'YesNo', 'Yes/No', 'yes/no', '', 0)