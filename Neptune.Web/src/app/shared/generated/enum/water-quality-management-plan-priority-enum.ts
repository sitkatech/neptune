//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanPriority]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum WaterQualityManagementPlanPriorityEnum {
  High = 1,
  Low = 2
}

export const WaterQualityManagementPlanPriorities: LookupTableEntry[]  = [
  { Name: "High", DisplayName: "High", Value: 1 },
  { Name: "Low", DisplayName: "Low", Value: 2 }
];
export const WaterQualityManagementPlanPrioritiesAsSelectDropdownOptions = WaterQualityManagementPlanPriorities.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
