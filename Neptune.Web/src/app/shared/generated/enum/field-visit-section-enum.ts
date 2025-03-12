//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[FieldVisitSection]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum FieldVisitSectionEnum {
  Inventory = 1,
  Assessment = 2,
  Maintenance = 3,
  PostMaintenanceAssessment = 4,
  VisitSummary = 5
}

export const FieldVisitSections: LookupTableEntry[]  = [
  { Name: "Inventory", DisplayName: "Inventory", Value: 1 },
  { Name: "Assessment", DisplayName: "Assessment", Value: 2 },
  { Name: "Maintenance", DisplayName: "Maintenance", Value: 3 },
  { Name: "PostMaintenanceAssessment", DisplayName: "Post-Maintenance Assessment", Value: 4 },
  { Name: "VisitSummary", DisplayName: "Visit Summary", Value: 5 }
];
export const FieldVisitSectionsAsSelectDropdownOptions = FieldVisitSections.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
