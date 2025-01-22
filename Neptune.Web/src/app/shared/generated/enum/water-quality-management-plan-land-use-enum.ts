//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanLandUse]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum WaterQualityManagementPlanLandUseEnum {
  Residential = 1,
  Commercial = 2,
  Industrial = 3,
  Other = 4,
  Road = 5,
  Flood = 6,
  Municipal = 7,
  Park = 8,
  Mixed = 9
}

export const WaterQualityManagementPlanLandUses: LookupTableEntry[]  = [
  { Name: "Residential", DisplayName: "Residential", Value: 1 },
  { Name: "Commercial", DisplayName: "Commercial", Value: 2 },
  { Name: "Industrial", DisplayName: "Industrial", Value: 3 },
  { Name: "Other", DisplayName: "Other", Value: 4 },
  { Name: "Road", DisplayName: "Road", Value: 5 },
  { Name: "Flood", DisplayName: "Flood", Value: 6 },
  { Name: "Municipal", DisplayName: "Municipal", Value: 7 },
  { Name: "Park", DisplayName: "Park", Value: 8 },
  { Name: "Mixed", DisplayName: "Mixed", Value: 9 }
];
export const WaterQualityManagementPlanLandUsesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...WaterQualityManagementPlanLandUses.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
