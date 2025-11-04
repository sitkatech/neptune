//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[NeptuneArea]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum NeptuneAreaEnum {
  Trash = 1,
  OCStormwaterTools = 2,
  Modeling = 3,
  Planning = 4,
  AI = 5
}

export const NeptuneAreas: LookupTableEntry[]  = [
  { Name: "Trash", DisplayName: "Trash Module", Value: 1 },
  { Name: "OCStormwaterTools", DisplayName: "Inventory Module", Value: 2 },
  { Name: "Modeling", DisplayName: "Modeling Module", Value: 3 },
  { Name: "Planning", DisplayName: "Planning Module", Value: 4 },
  { Name: "AI", DisplayName: "AI Module", Value: 5 }
];
export const NeptuneAreasAsSelectDropdownOptions = NeptuneAreas.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
