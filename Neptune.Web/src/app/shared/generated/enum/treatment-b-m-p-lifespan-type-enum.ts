//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[TreatmentBMPLifespanType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum TreatmentBMPLifespanTypeEnum {
  Unspecified = 1,
  Perpetuity = 2,
  FixedEndDate = 3
}

export const TreatmentBMPLifespanTypes: LookupTableEntry[]  = [
  { Name: "Unspecified", DisplayName: "Unspecified/Voluntary", Value: 1 },
  { Name: "Perpetuity", DisplayName: "Perpetuity/Life of Project", Value: 2 },
  { Name: "FixedEndDate", DisplayName: "Fixed End Date", Value: 3 }
];
export const TreatmentBMPLifespanTypesAsSelectDropdownOptions = TreatmentBMPLifespanTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
