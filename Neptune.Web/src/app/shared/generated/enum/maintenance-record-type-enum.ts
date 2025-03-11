//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[MaintenanceRecordType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum MaintenanceRecordTypeEnum {
  Routine = 1,
  Corrective = 2
}

export const MaintenanceRecordTypes: LookupTableEntry[]  = [
  { Name: "Routine", DisplayName: "Routine", Value: 1 },
  { Name: "Corrective", DisplayName: "Corrective", Value: 2 }
];
export const MaintenanceRecordTypesAsSelectDropdownOptions = MaintenanceRecordTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
