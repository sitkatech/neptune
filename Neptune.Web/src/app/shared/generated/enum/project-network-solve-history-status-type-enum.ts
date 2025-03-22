//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[ProjectNetworkSolveHistoryStatusType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum ProjectNetworkSolveHistoryStatusTypeEnum {
  Queued = 1,
  Succeeded = 2,
  Failed = 3
}

export const ProjectNetworkSolveHistoryStatusTypes: LookupTableEntry[]  = [
  { Name: "Queued", DisplayName: "Queued", Value: 1 },
  { Name: "Succeeded", DisplayName: "Succeeded", Value: 2 },
  { Name: "Failed", DisplayName: "Failed", Value: 3 }
];
export const ProjectNetworkSolveHistoryStatusTypesAsSelectDropdownOptions = ProjectNetworkSolveHistoryStatusTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
