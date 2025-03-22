//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[TreatmentBMPAssessmentType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum TreatmentBMPAssessmentTypeEnum {
  Initial = 1,
  PostMaintenance = 2
}

export const TreatmentBMPAssessmentTypes: LookupTableEntry[]  = [
  { Name: "Initial", DisplayName: "Initial", Value: 1 },
  { Name: "PostMaintenance", DisplayName: "Post-Maintenance", Value: 2 }
];
export const TreatmentBMPAssessmentTypesAsSelectDropdownOptions = TreatmentBMPAssessmentTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
