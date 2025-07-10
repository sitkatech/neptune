//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[ObservationTypeCollectionMethod]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum ObservationTypeCollectionMethodEnum {
  DiscreteValue = 1,
  PassFail = 3,
  Percentage = 4
}

export const ObservationTypeCollectionMethods: LookupTableEntry[]  = [
  { Name: "DiscreteValue", DisplayName: "Discrete Value Observation", Value: 1 },
  { Name: "PassFail", DisplayName: "Pass/Fail Observation", Value: 3 },
  { Name: "Percentage", DisplayName: "Percent-based Observation", Value: 4 }
];
export const ObservationTypeCollectionMethodsAsSelectDropdownOptions = ObservationTypeCollectionMethods.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
