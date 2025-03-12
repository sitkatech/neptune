//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[HydromodificationAppliesType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum HydromodificationAppliesTypeEnum {
  Applicable = 1,
  Exempt = 2
}

export const HydromodificationAppliesTypes: LookupTableEntry[]  = [
  { Name: "Applicable ", DisplayName: "Applicable", Value: 1 },
  { Name: "Exempt", DisplayName: "Exempt", Value: 2 }
];
export const HydromodificationAppliesTypesAsSelectDropdownOptions = HydromodificationAppliesTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
