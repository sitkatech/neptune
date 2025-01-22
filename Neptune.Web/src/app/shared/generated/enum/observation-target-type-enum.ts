//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[ObservationTargetType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum ObservationTargetTypeEnum {
  PassFail = 1,
  High = 2,
  Low = 3,
  SpecificValue = 4
}

export const ObservationTargetTypes: LookupTableEntry[]  = [
  { Name: "PassFail", DisplayName: "Observation is Pass/Fail", Value: 1 },
  { Name: "High", DisplayName: "Higher observed values result in higher score", Value: 2 },
  { Name: "Low", DisplayName: "Lower observed values result in higher score", Value: 3 },
  { Name: "SpecificValue", DisplayName: "Observed values exactly equal to the benchmark result in highest score", Value: 4 }
];
export const ObservationTargetTypesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...ObservationTargetTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
