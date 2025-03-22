//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[DelineationType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum DelineationTypeEnum {
  Centralized = 1,
  Distributed = 2
}

export const DelineationTypes: LookupTableEntry[]  = [
  { Name: "Centralized", DisplayName: "Centralized", Value: 1 },
  { Name: "Distributed", DisplayName: "Distributed", Value: 2 }
];
export const DelineationTypesAsSelectDropdownOptions = DelineationTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
