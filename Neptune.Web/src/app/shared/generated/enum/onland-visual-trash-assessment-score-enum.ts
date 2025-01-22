//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[OnlandVisualTrashAssessmentScore]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum OnlandVisualTrashAssessmentScoreEnum {
  A = 1,
  B = 2,
  C = 3,
  D = 4
}

export const OnlandVisualTrashAssessmentScores: LookupTableEntry[]  = [
  { Name: "A", DisplayName: "A", Value: 1 },
  { Name: "B", DisplayName: "B", Value: 2 },
  { Name: "C", DisplayName: "C", Value: 3 },
  { Name: "D", DisplayName: "D", Value: 4 }
];
export const OnlandVisualTrashAssessmentScoresAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...OnlandVisualTrashAssessmentScores.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
