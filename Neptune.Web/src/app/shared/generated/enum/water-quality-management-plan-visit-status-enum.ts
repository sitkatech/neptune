//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanVisitStatus]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum WaterQualityManagementPlanVisitStatusEnum {
  InitialAnnualVerification = 1,
  FollowupVerification = 2
}

export const WaterQualityManagementPlanVisitStatuses: LookupTableEntry[]  = [
  { Name: "Initial Annual Verification", DisplayName: "Initial Annual Verification", Value: 1 },
  { Name: "Follow-up Verification", DisplayName: "Follow-up Verification", Value: 2 }
];
export const WaterQualityManagementPlanVisitStatusesAsSelectDropdownOptions = WaterQualityManagementPlanVisitStatuses.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
