//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[PermitType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum PermitTypeEnum {
  PhaseIMS4 = 1,
  PhaseIIMS4 = 2,
  IGP = 3,
  IndividualPermit = 4,
  CalTransMS4 = 5,
  Other = 6
}

export const PermitTypes: LookupTableEntry[]  = [
  { Name: "PhaseIMS4", DisplayName: "Phase I MS4", Value: 1 },
  { Name: "PhaseIIMS4", DisplayName: "Phase II MS4", Value: 2 },
  { Name: "IGP", DisplayName: "IGP", Value: 3 },
  { Name: "IndividualPermit", DisplayName: "Individual Permit", Value: 4 },
  { Name: "CalTransMS4", DisplayName: "CalTrans MS4", Value: 5 },
  { Name: "Other", DisplayName: "Other", Value: 6 }
];
export const PermitTypesAsSelectDropdownOptions = PermitTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
