//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanDocumentType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum WaterQualityManagementPlanDocumentTypeEnum {
  FinalWQMP = 1,
  AsBuiltDrawings = 2,
  OMPlan = 3,
  Other = 4
}

export const WaterQualityManagementPlanDocumentTypes: LookupTableEntry[]  = [
  { Name: "FinalWQMP", DisplayName: "Final WQMP", Value: 1 },
  { Name: "AsBuiltDrawings", DisplayName: "As-built drawings", Value: 2 },
  { Name: "OMPlan", DisplayName: "O&M Plan", Value: 3 },
  { Name: "Other", DisplayName: "Other", Value: 4 }
];
export const WaterQualityManagementPlanDocumentTypesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...WaterQualityManagementPlanDocumentTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
