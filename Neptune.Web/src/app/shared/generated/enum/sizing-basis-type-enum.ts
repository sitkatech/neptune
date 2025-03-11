//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[SizingBasisType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum SizingBasisTypeEnum {
  FullTrashCapture = 1,
  WaterQuality = 2,
  Other = 3,
  NotProvided = 4
}

export const SizingBasisTypes: LookupTableEntry[]  = [
  { Name: "FullTrashCapture", DisplayName: "Full Trash Capture", Value: 1 },
  { Name: "WaterQuality", DisplayName: "Water Quality", Value: 2 },
  { Name: "Other", DisplayName: "Other (less than Water Quality)", Value: 3 },
  { Name: "NotProvided", DisplayName: "Not Provided", Value: 4 }
];
export const SizingBasisTypesAsSelectDropdownOptions = SizingBasisTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
