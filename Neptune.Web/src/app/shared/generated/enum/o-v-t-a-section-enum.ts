//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[OVTASection]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum OVTASectionEnum {
  Instructions = 1,
  InitiateOVTA = 2,
  RecordObservations = 3,
  AddOrRemoveParcels = 4,
  RefineAssessmentArea = 5,
  FinalizeOVTA = 6
}

export const OVTASections: LookupTableEntry[]  = [
  { Name: "Instructions", DisplayName: "Instructions", Value: 1 },
  { Name: "InitiateOVTA", DisplayName: "Initiate OVTA", Value: 2 },
  { Name: "RecordObservations", DisplayName: "Record Observations", Value: 3 },
  { Name: "AddOrRemoveParcels", DisplayName: "Add or Remove Parcels", Value: 4 },
  { Name: "RefineAssessmentArea", DisplayName: "Refine Assessment Area", Value: 5 },
  { Name: "FinalizeOVTA", DisplayName: "Review and Finalize OVTA", Value: 6 }
];
export const OVTASectionsAsSelectDropdownOptions = OVTASections.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
