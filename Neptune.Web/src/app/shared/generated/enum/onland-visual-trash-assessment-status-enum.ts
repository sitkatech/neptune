//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentStatus]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum OnlandVisualTrashAssessmentStatusEnum {
  InProgress = 1,
  Complete = 2
}

export const OnlandVisualTrashAssessmentStatuses: LookupTableEntry[]  = [
  { Name: "InProgress", DisplayName: "In Progress", Value: 1 },
  { Name: "Complete", DisplayName: "Complete", Value: 2 }
];
export const OnlandVisualTrashAssessmentStatusesAsSelectDropdownOptions = OnlandVisualTrashAssessmentStatuses.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
