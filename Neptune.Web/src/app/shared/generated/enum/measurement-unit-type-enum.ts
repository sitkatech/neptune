//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[MeasurementUnitType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum MeasurementUnitTypeEnum {
  Acres = 1,
  SquareFeet = 2,
  Kilogram = 3,
  Count = 4,
  Percent = 5,
  MilligamsPerLiter = 6,
  Meters = 7,
  Feet = 8,
  Inches = 9,
  InchesPerHour = 10,
  Seconds = 11,
  PercentDecline = 12,
  PercentIncrease = 13,
  PercentDeviation = 14,
  CubicFeet = 15,
  Gallons = 16,
  Minutes = 17,
  CubicFeetPerSecond = 18,
  GallonsPerDay = 19,
  Pounds = 20,
  Tons = 21,
  CubicYards = 22
}

export const MeasurementUnitTypes: LookupTableEntry[]  = [
  { Name: "Acres", DisplayName: "acres", Value: 1 },
  { Name: "SquareFeet", DisplayName: "square feet", Value: 2 },
  { Name: "Kilogram", DisplayName: "kg", Value: 3 },
  { Name: "Count", DisplayName: "count", Value: 4 },
  { Name: "Percent", DisplayName: "%", Value: 5 },
  { Name: "MilligamsPerLiter", DisplayName: "mg/L", Value: 6 },
  { Name: "Meters", DisplayName: "meters", Value: 7 },
  { Name: "Feet", DisplayName: "feet", Value: 8 },
  { Name: "Inches", DisplayName: "inches", Value: 9 },
  { Name: "InchesPerHour", DisplayName: "in/hr", Value: 10 },
  { Name: "Seconds", DisplayName: "seconds", Value: 11 },
  { Name: "PercentDecline", DisplayName: "% decline from benchmark", Value: 12 },
  { Name: "PercentIncrease", DisplayName: "% increase from benchmark", Value: 13 },
  { Name: "PercentDeviation", DisplayName: "% of benchmark", Value: 14 },
  { Name: "Cubic Feet", DisplayName: "cubic feet", Value: 15 },
  { Name: "Gallons", DisplayName: "gallons", Value: 16 },
  { Name: "Minutes", DisplayName: "minutes", Value: 17 },
  { Name: "CubicFeetPerSecond", DisplayName: "cubic feet per second", Value: 18 },
  { Name: "GallonsPerDay", DisplayName: "gallons per day", Value: 19 },
  { Name: "Pounds", DisplayName: "pounds", Value: 20 },
  { Name: "Tons", DisplayName: "tons", Value: 21 },
  { Name: "CubicYards", DisplayName: "cubic yards", Value: 22 }
];
export const MeasurementUnitTypesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...MeasurementUnitTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
