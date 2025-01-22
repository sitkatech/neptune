//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanDevelopmentType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum WaterQualityManagementPlanDevelopmentTypeEnum {
  NewDevelopment = 1,
  Redevelopment = 2
}

export const WaterQualityManagementPlanDevelopmentTypes: LookupTableEntry[]  = [
  { Name: "NewDevelopment", DisplayName: "New Development", Value: 1 },
  { Name: "Redevelopment", DisplayName: "Redevelopment", Value: 2 }
];
export const WaterQualityManagementPlanDevelopmentTypesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...WaterQualityManagementPlanDevelopmentTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
