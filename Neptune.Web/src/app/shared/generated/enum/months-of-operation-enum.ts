//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[MonthsOfOperation]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum MonthsOfOperationEnum {
  Summer = 1,
  Winter = 2,
  Both = 3
}

export const MonthsOfOperations: LookupTableEntry[]  = [
  { Name: "Summer", DisplayName: "Summer", Value: 1 },
  { Name: "Winter", DisplayName: "Winter", Value: 2 },
  { Name: "Both", DisplayName: "Both", Value: 3 }
];
export const MonthsOfOperationsAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...MonthsOfOperations.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
