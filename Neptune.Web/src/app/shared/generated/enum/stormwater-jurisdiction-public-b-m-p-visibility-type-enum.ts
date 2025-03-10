//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[StormwaterJurisdictionPublicBMPVisibilityType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum StormwaterJurisdictionPublicBMPVisibilityTypeEnum {
  VerifiedOnly = 1,
  None = 2
}

export const StormwaterJurisdictionPublicBMPVisibilityTypes: LookupTableEntry[]  = [
  { Name: "VerifiedOnly", DisplayName: "Verified Only", Value: 1 },
  { Name: "None", DisplayName: "None", Value: 2 }
];
export const StormwaterJurisdictionPublicBMPVisibilityTypesAsSelectDropdownOptions = StormwaterJurisdictionPublicBMPVisibilityTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
