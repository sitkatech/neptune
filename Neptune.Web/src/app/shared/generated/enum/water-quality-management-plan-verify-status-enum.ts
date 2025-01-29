//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyStatus]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum WaterQualityManagementPlanVerifyStatusEnum {
  VerifyAdequateOAndMofWQMP = 1,
  DeficienciesarePresentandFollowupisRequired = 2
}

export const WaterQualityManagementPlanVerifyStatuses: LookupTableEntry[]  = [
  { Name: "Verify Adequate O&M of WQMP", DisplayName: "Verify Adequate O&M of WQMP", Value: 1 },
  { Name: "Deficiencies are Present and Follow-up is Required", DisplayName: "Deficiencies are Present and Follow-up is Required", Value: 2 }
];
export const WaterQualityManagementPlanVerifyStatusesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...WaterQualityManagementPlanVerifyStatuses.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
