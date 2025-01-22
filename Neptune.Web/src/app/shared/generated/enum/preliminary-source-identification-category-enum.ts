//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[PreliminarySourceIdentificationCategory]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum PreliminarySourceIdentificationCategoryEnum {
  Vehicles = 1,
  InadequateWasteContainerManagement = 2,
  PedestrianLitter = 3,
  IllegalDumping = 4
}

export const PreliminarySourceIdentificationCategories: LookupTableEntry[]  = [
  { Name: "Vehicles", DisplayName: "Vehicles", Value: 1 },
  { Name: "InadequateWasteContainerManagement", DisplayName: "Inadequate Waste Container Management", Value: 2 },
  { Name: "PedestrianLitter", DisplayName: "Pedestrian Litter", Value: 3 },
  { Name: "IllegalDumping", DisplayName: "Illegal Dumping", Value: 4 }
];
export const PreliminarySourceIdentificationCategoriesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...PreliminarySourceIdentificationCategories.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
