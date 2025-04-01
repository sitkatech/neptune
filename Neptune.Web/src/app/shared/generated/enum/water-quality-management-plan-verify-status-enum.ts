//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyStatus]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum WaterQualityManagementPlanVerifyStatusEnum {
  AdequateOAndMofWQMPisVerified = 1,
  DeficienciesarePresentandFollowupisRequired = 2
}

export const WaterQualityManagementPlanVerifyStatuses: LookupTableEntry[]  = [
  { Name: "Adequate O&M of WQMP is Verified", DisplayName: "Adequate O&M of WQMP is Verified", Value: 1 },
  { Name: "Deficiencies are Present and Follow-up is Required", DisplayName: "Deficiencies are Present and Follow-up is Required", Value: 2 }
];
export const WaterQualityManagementPlanVerifyStatusesAsSelectDropdownOptions = WaterQualityManagementPlanVerifyStatuses.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
