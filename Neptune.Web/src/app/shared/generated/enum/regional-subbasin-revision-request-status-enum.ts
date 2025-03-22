//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[RegionalSubbasinRevisionRequestStatus]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum RegionalSubbasinRevisionRequestStatusEnum {
  Open = 1,
  Closed = 2
}

export const RegionalSubbasinRevisionRequestStatuses: LookupTableEntry[]  = [
  { Name: "Open", DisplayName: "Open", Value: 1 },
  { Name: "Closed", DisplayName: "Closed", Value: 2 }
];
export const RegionalSubbasinRevisionRequestStatusesAsSelectDropdownOptions = RegionalSubbasinRevisionRequestStatuses.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
