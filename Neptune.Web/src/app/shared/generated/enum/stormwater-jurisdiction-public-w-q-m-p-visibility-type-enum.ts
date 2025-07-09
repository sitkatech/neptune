//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[StormwaterJurisdictionPublicWQMPVisibilityType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum StormwaterJurisdictionPublicWQMPVisibilityTypeEnum {
  ActiveAndInactive = 1,
  ActiveOnly = 2,
  None = 3
}

export const StormwaterJurisdictionPublicWQMPVisibilityTypes: LookupTableEntry[]  = [
  { Name: "ActiveAndInactive", DisplayName: "Active and Inactive", Value: 1 },
  { Name: "ActiveOnly", DisplayName: "Active Only", Value: 2 },
  { Name: "None", DisplayName: "None", Value: 3 }
];
export const StormwaterJurisdictionPublicWQMPVisibilityTypesAsSelectDropdownOptions = StormwaterJurisdictionPublicWQMPVisibilityTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
