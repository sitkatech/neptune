//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[StormwaterBreadCrumbEntity]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum StormwaterBreadCrumbEntityEnum {
  TreatmentBMP = 1,
  Jurisdiction = 3,
  Users = 4,
  Assessments = 5,
  FieldVisits = 6,
  FieldRecords = 7,
  WaterQualityManagementPlan = 8,
  Parcel = 9,
  OnlandVisualTrashAssessment = 10
}

export const StormwaterBreadCrumbEntities: LookupTableEntry[]  = [
  { Name: "TreatmentBMP", DisplayName: "Treatment BMP", Value: 1 },
  { Name: "Jurisdiction", DisplayName: "Jurisdiction", Value: 3 },
  { Name: "Users", DisplayName: "Users", Value: 4 },
  { Name: "Assessments", DisplayName: "Assessments", Value: 5 },
  { Name: "FieldVisits", DisplayName: "Field Visits", Value: 6 },
  { Name: "FieldRecords", DisplayName: "Field Records", Value: 7 },
  { Name: "WaterQualityManagementPlan", DisplayName: "Water Quality Management Plan", Value: 8 },
  { Name: "Parcel", DisplayName: "Parcel", Value: 9 },
  { Name: "OnlandVisualTrashAssessment", DisplayName: "Onland Visual Trash Assessment", Value: 10 }
];
export const StormwaterBreadCrumbEntitiesAsSelectDropdownOptions = StormwaterBreadCrumbEntities.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
