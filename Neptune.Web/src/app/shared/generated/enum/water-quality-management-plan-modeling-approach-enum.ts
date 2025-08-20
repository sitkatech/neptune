//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanModelingApproach]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum WaterQualityManagementPlanModelingApproachEnum {
  Detailed = 1,
  Simplified = 2
}

export const WaterQualityManagementPlanModelingApproaches: LookupTableEntry[]  = [
  { Name: "Detailed", DisplayName: "Detailed", Value: 1 },
  { Name: "Simplified", DisplayName: "Simplified", Value: 2 }
];
export const WaterQualityManagementPlanModelingApproachesAsSelectDropdownOptions = WaterQualityManagementPlanModelingApproaches.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
