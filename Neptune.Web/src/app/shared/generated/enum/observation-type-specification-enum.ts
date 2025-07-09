//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[ObservationTypeSpecification]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/forms/form-field/form-field.component"

export enum ObservationTypeSpecificationEnum {
  PassFail_PassFail_None = 1,
  DiscreteValues_HighTargetValue_DiscreteThresholdValue = 2,
  DiscreteValues_HighTargetValue_PercentFromBenchmark = 3,
  DiscreteValues_LowTargetValue_DiscreteThresholdValue = 4,
  DiscreteValues_LowTargetValue_PercentFromBenchmark = 5,
  DiscreteValues_SpecificTargetValue_DiscreteThresholdValue = 6,
  DiscreteValues_SpecificTargetValue_PercentFromBenchmark = 7,
  PercentValue_HighTargetValue_DiscreteThresholdValue = 14,
  PercentValue_HighTargetValue_PercentFromBenchmark = 15,
  PercentValue_LowTargetValue_DiscreteThresholdValue = 16,
  PercentValue_LowTargetValue_PercentFromBenchmark = 17,
  PercentValue_SpecificTargetValue_DiscreteThresholdValue = 18,
  PercentValue_SpecificTargetValue_PercentFromBenchmark = 19
}

export const ObservationTypeSpecifications: LookupTableEntry[]  = [
  { Name: "PassFail_PassFail_None", DisplayName: " PassFail_PassFail_None", Value: 1 },
  { Name: "DiscreteValues_HighTargetValue_DiscreteThresholdValue", DisplayName: " DiscreteValues_HighTargetValue_DiscreteThresholdValue", Value: 2 },
  { Name: "DiscreteValues_HighTargetValue_PercentFromBenchmark", DisplayName: " DiscreteValues_HighTargetValue_PercentFromBenchmark", Value: 3 },
  { Name: "DiscreteValues_LowTargetValue_DiscreteThresholdValue", DisplayName: " DiscreteValues_LowTargetValue_DiscreteThresholdValue", Value: 4 },
  { Name: "DiscreteValues_LowTargetValue_PercentFromBenchmark", DisplayName: " DiscreteValues_LowTargetValue_PercentFromBenchmark", Value: 5 },
  { Name: "DiscreteValues_SpecificTargetValue_DiscreteThresholdValue", DisplayName: " DiscreteValues_SpecificTargetValue_DiscreteThresholdValue", Value: 6 },
  { Name: "DiscreteValues_SpecificTargetValue_PercentFromBenchmark", DisplayName: " DiscreteValues_SpecificTargetValue_PercentFromBenchmark", Value: 7 },
  { Name: "PercentValue_HighTargetValue_DiscreteThresholdValue", DisplayName: " PercentValue_HighTargetValue_DiscreteThresholdValue", Value: 14 },
  { Name: "PercentValue_HighTargetValue_PercentFromBenchmark", DisplayName: " PercentValue_HighTargetValue_PercentFromBenchmark", Value: 15 },
  { Name: "PercentValue_LowTargetValue_DiscreteThresholdValue", DisplayName: " PercentValue_LowTargetValue_DiscreteThresholdValue", Value: 16 },
  { Name: "PercentValue_LowTargetValue_PercentFromBenchmark", DisplayName: " PercentValue_LowTargetValue_PercentFromBenchmark", Value: 17 },
  { Name: "PercentValue_SpecificTargetValue_DiscreteThresholdValue", DisplayName: " PercentValue_SpecificTargetValue_DiscreteThresholdValue", Value: 18 },
  { Name: "PercentValue_SpecificTargetValue_PercentFromBenchmark", DisplayName: " PercentValue_SpecificTargetValue_PercentFromBenchmark", Value: 19 }
];
export const ObservationTypeSpecificationsAsSelectDropdownOptions = ObservationTypeSpecifications.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
