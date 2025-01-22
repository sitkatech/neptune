//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[TrashCaptureStatusType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum TrashCaptureStatusTypeEnum {
  Full = 1,
  Partial = 2,
  None = 3,
  NotProvided = 4
}

export const TrashCaptureStatusTypes: LookupTableEntry[]  = [
  { Name: "Full", DisplayName: "Full", Value: 1 },
  { Name: "Partial", DisplayName: "Partial (>5mm but less than full sizing)", Value: 2 },
  { Name: "None", DisplayName: "No Trash Capture", Value: 3 },
  { Name: "NotProvided", DisplayName: "Not Provided", Value: 4 }
];
export const TrashCaptureStatusTypesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...TrashCaptureStatusTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
