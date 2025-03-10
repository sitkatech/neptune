//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[FieldVisitType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum FieldVisitTypeEnum {
  DryWeather = 1,
  WetWeather = 2,
  PostStormAssessment = 3
}

export const FieldVisitTypes: LookupTableEntry[]  = [
  { Name: "DryWeather", DisplayName: "Dry Weather", Value: 1 },
  { Name: "WetWeather", DisplayName: "Wet Weather", Value: 2 },
  { Name: "PostStormAssessment", DisplayName: "Post-Storm Assessment", Value: 3 }
];
export const FieldVisitTypesAsSelectDropdownOptions = FieldVisitTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
