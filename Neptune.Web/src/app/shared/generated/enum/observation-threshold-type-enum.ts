//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[ObservationThresholdType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum ObservationThresholdTypeEnum {
  SpecificValue = 1,
  RelativeToBenchmark = 2,
  None = 3
}

export const ObservationThresholdTypes: LookupTableEntry[]  = [
  { Name: "SpecificValue", DisplayName: "Threshold is a specific value", Value: 1 },
  { Name: "RelativeToBenchmark", DisplayName: "Threshold is a relative percent of the benchmark value", Value: 2 },
  { Name: "None", DisplayName: "None", Value: 3 }
];
export const ObservationThresholdTypesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...ObservationThresholdTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
