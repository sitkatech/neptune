//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[CustomAttributeDataType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum CustomAttributeDataTypeEnum {
  String = 1,
  Integer = 2,
  Decimal = 3,
  DateTime = 4,
  PickFromList = 5,
  MultiSelect = 6
}

export const CustomAttributeDataTypes: LookupTableEntry[]  = [
  { Name: "String", DisplayName: "String", Value: 1 },
  { Name: "Integer", DisplayName: "Integer", Value: 2 },
  { Name: "Decimal", DisplayName: "Decimal", Value: 3 },
  { Name: "DateTime", DisplayName: "Date/Time", Value: 4 },
  { Name: "PickFromList", DisplayName: "Pick One from List", Value: 5 },
  { Name: "MultiSelect", DisplayName: "Select Many from List", Value: 6 }
];
export const CustomAttributeDataTypesAsSelectDropdownOptions = CustomAttributeDataTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
