//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[DryWeatherFlowOverride]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum DryWeatherFlowOverrideEnum {
  No = 1,
  Yes = 2
}

export const DryWeatherFlowOverrides: LookupTableEntry[]  = [
  { Name: "No", DisplayName: "No - As Modeled", Value: 1 },
  { Name: "Yes", DisplayName: "Yes - DWF Effectively Eliminated", Value: 2 }
];
export const DryWeatherFlowOverridesAsSelectDropdownOptions = DryWeatherFlowOverrides.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
