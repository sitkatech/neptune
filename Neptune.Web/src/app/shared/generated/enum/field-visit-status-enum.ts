//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[FieldVisitStatus]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum FieldVisitStatusEnum {
  InProgress = 1,
  Complete = 2,
  Unresolved = 3,
  ReturnedToEdit = 4
}

export const FieldVisitStatuses: LookupTableEntry[]  = [
  { Name: "InProgress", DisplayName: "In Progress", Value: 1 },
  { Name: "Complete", DisplayName: "Complete", Value: 2 },
  { Name: "Unresolved", DisplayName: "Unresolved", Value: 3 },
  { Name: "ReturnedToEdit", DisplayName: "Returned to Edit", Value: 4 }
];
export const FieldVisitStatusesAsSelectDropdownOptions = FieldVisitStatuses.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
