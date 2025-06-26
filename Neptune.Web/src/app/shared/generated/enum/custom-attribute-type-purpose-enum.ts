//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[CustomAttributeTypePurpose]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum CustomAttributeTypePurposeEnum {
  Modeling = 1,
  OtherDesignAttributes = 2,
  Maintenance = 3
}

export const CustomAttributeTypePurposes: LookupTableEntry[]  = [
  { Name: "Modeling", DisplayName: "Modeling Attributes", Value: 1 },
  { Name: "OtherDesignAttributes", DisplayName: "Other Design Attributes", Value: 2 },
  { Name: "Maintenance", DisplayName: "Maintenance Attributes", Value: 3 }
];
export const CustomAttributeTypePurposesAsSelectDropdownOptions = CustomAttributeTypePurposes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
