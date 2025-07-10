//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[PriorityLandUseType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum PriorityLandUseTypeEnum {
  Commercial = 1,
  HighDensityResidential = 2,
  Industrial = 3,
  MixedUrban = 4,
  CommercialRetail = 5,
  PublicTransportationStations = 6,
  ALU = 7
}

export const PriorityLandUseTypes: LookupTableEntry[]  = [
  { Name: "Commercial", DisplayName: "Commercial", Value: 1 },
  { Name: "HighDensityResidential", DisplayName: "High Density Residential", Value: 2 },
  { Name: "Industrial", DisplayName: "Industrial", Value: 3 },
  { Name: "MixedUrban", DisplayName: "Mixed Urban", Value: 4 },
  { Name: "CommercialRetail", DisplayName: "Commercial-Retail", Value: 5 },
  { Name: "Public Transportation Stations", DisplayName: "Public Transportation Stations", Value: 6 },
  { Name: "ALU", DisplayName: "ALU", Value: 7 }
];
export const PriorityLandUseTypesAsSelectDropdownOptions = PriorityLandUseTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
