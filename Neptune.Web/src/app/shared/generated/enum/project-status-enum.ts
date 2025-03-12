//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[ProjectStatus]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum ProjectStatusEnum {
  Draft = 1
}

export const ProjectStatuses: LookupTableEntry[]  = [
  { Name: "Draft", DisplayName: "Draft", Value: 1 }
];
export const ProjectStatusesAsSelectDropdownOptions = ProjectStatuses.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
