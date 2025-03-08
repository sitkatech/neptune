//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanVerifyType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum WaterQualityManagementPlanVerifyTypeEnum {
  JurisdictionPerformed = 1,
  SelfCertification = 2
}

export const WaterQualityManagementPlanVerifyTypes: LookupTableEntry[]  = [
  { Name: "Jurisdiction Performed", DisplayName: "Jurisdiction Performed", Value: 1 },
  { Name: "Self Certification", DisplayName: "Self Certification", Value: 2 }
];
export const WaterQualityManagementPlanVerifyTypesAsSelectDropdownOptions = WaterQualityManagementPlanVerifyTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
