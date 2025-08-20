//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[FundingEventType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum FundingEventTypeEnum {
  PlanningAndDesign = 1,
  CapitalConstruction = 2,
  RoutineMaintenance = 3,
  RehabilitativeMaintenance = 4,
  Retrofit = 5
}

export const FundingEventTypes: LookupTableEntry[]  = [
  { Name: "PlanningAndDesign", DisplayName: "Planning & Design", Value: 1 },
  { Name: "CapitalConstruction", DisplayName: "Capital Construction", Value: 2 },
  { Name: "RoutineMaintenance", DisplayName: "Routine Assessment and Maintenance", Value: 3 },
  { Name: "RehabilitativeMaintenance", DisplayName: "Rehabilitative Maintenance", Value: 4 },
  { Name: "Retrofit", DisplayName: "Retrofit", Value: 5 }
];
export const FundingEventTypesAsSelectDropdownOptions = FundingEventTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
