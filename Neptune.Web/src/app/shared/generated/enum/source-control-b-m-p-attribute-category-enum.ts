//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[SourceControlBMPAttributeCategory]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum SourceControlBMPAttributeCategoryEnum {
  HydrologicSourceControlandSiteDesignBMPs = 1,
  ApplicableRoutineNonStructuralSourceControlBMPs = 2,
  ApplicableRoutineStructuralSourceControlBMPs = 3
}

export const SourceControlBMPAttributeCategories: LookupTableEntry[]  = [
  { Name: "Hydrologic Source Control and Site Design BMPs", DisplayName: "Hydrologic Source Control and Site Design BMPs", Value: 1 },
  { Name: "Applicable Routine Non-Structural Source Control BMPs", DisplayName: "Applicable Routine Non-Structural Source Control BMPs", Value: 2 },
  { Name: "Applicable Routine Structural Source Control BMPs", DisplayName: "Applicable Routine Structural Source Control BMPs", Value: 3 }
];
export const SourceControlBMPAttributeCategoriesAsSelectDropdownOptions = SourceControlBMPAttributeCategories.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
