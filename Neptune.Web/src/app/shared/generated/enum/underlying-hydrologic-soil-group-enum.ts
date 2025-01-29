//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[UnderlyingHydrologicSoilGroup]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum UnderlyingHydrologicSoilGroupEnum {
  A = 1,
  B = 2,
  C = 3,
  D = 4,
  Liner = 5
}

export const UnderlyingHydrologicSoilGroups: LookupTableEntry[]  = [
  { Name: "A", DisplayName: "A", Value: 1 },
  { Name: "B", DisplayName: "B", Value: 2 },
  { Name: "C", DisplayName: "C", Value: 3 },
  { Name: "D", DisplayName: "D", Value: 4 },
  { Name: "Liner", DisplayName: "Liner", Value: 5 }
];
export const UnderlyingHydrologicSoilGroupsAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...UnderlyingHydrologicSoilGroups.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
