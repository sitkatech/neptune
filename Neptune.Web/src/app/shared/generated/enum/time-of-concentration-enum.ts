//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[TimeOfConcentration]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum TimeOfConcentrationEnum {
  FiveMinutes = 1,
  TenMinutes = 2,
  FifteenMinutes = 3,
  TwentyMinutes = 4,
  ThirtyMinutes = 5,
  FortyFiveMinutes = 6,
  SixtyMinutes = 7
}

export const TimeOfConcentrations: LookupTableEntry[]  = [
  { Name: "FiveMinutes", DisplayName: "5", Value: 1 },
  { Name: "TenMinutes", DisplayName: "10", Value: 2 },
  { Name: "FifteenMinutes", DisplayName: "15", Value: 3 },
  { Name: "TwentyMinutes", DisplayName: "20", Value: 4 },
  { Name: "ThirtyMinutes", DisplayName: "30", Value: 5 },
  { Name: "FortyFiveMinutes", DisplayName: "45", Value: 6 },
  { Name: "SixtyMinutes", DisplayName: "60", Value: 7 }
];
export const TimeOfConcentrationsAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...TimeOfConcentrations.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
