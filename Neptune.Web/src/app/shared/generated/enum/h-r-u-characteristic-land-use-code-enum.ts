//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[HRUCharacteristicLandUseCode]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum HRUCharacteristicLandUseCodeEnum {
  COMM = 1,
  EDU = 2,
  IND = 3,
  UTIL = 4,
  RESSFH = 5,
  RESSFL = 6,
  RESMF = 7,
  TRFWY = 8,
  TRANS = 9,
  TROTH = 10,
  OSAGIR = 11,
  OSAGNI = 12,
  OSDEV = 13,
  OSIRR = 14,
  OSLOW = 15,
  OSFOR = 16,
  OSWET = 17,
  OSVAC = 18,
  WATER = 19
}

export const HRUCharacteristicLandUseCodes: LookupTableEntry[]  = [
  { Name: "COMM", DisplayName: "Commercial", Value: 1 },
  { Name: "EDU", DisplayName: "Education", Value: 2 },
  { Name: "IND", DisplayName: "Industrial", Value: 3 },
  { Name: "UTIL", DisplayName: "Utility", Value: 4 },
  { Name: "RESSFH", DisplayName: "Residential - Single Family High Density", Value: 5 },
  { Name: "RESSFL", DisplayName: "Residential - Single Family Low Density", Value: 6 },
  { Name: "RESMF", DisplayName: "Residential - MultiFamily", Value: 7 },
  { Name: "TRFWY", DisplayName: "Transportation - Freeway", Value: 8 },
  { Name: "TRANS", DisplayName: "Transportation - Local Road", Value: 9 },
  { Name: "TROTH", DisplayName: "Transportation - Other", Value: 10 },
  { Name: "OSAGIR", DisplayName: "Open Space - Irrigated Agriculture", Value: 11 },
  { Name: "OSAGNI", DisplayName: "Open Space - Non-Irrigated Agriculture", Value: 12 },
  { Name: "OSDEV", DisplayName: "Open Space - Low Density Development", Value: 13 },
  { Name: "OSIRR", DisplayName: "Open Space - Irrigated Recreation", Value: 14 },
  { Name: "OSLOW", DisplayName: "Open Space - Low Canopy Vegetation", Value: 15 },
  { Name: "OSFOR", DisplayName: "Open Space - Forest", Value: 16 },
  { Name: "OSWET", DisplayName: "Open Space - Wetlands", Value: 17 },
  { Name: "OSVAC", DisplayName: "Open Space - Vacant Land", Value: 18 },
  { Name: "WATER", DisplayName: "Water", Value: 19 }
];
export const HRUCharacteristicLandUseCodesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...HRUCharacteristicLandUseCodes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
