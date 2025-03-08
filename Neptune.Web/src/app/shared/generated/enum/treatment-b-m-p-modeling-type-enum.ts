//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[TreatmentBMPModelingType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum TreatmentBMPModelingTypeEnum {
  BioinfiltrationBioretentionWithRaisedUnderdrain = 1,
  BioretentionWithNoUnderdrain = 2,
  BioretentionWithUnderdrainAndImperviousLiner = 3,
  CisternsForHarvestAndUse = 4,
  ConstructedWetland = 5,
  DryExtendedDetentionBasin = 6,
  DryWeatherTreatmentSystems = 7,
  Drywell = 8,
  FlowDurationControlBasin = 9,
  FlowDurationControlTank = 10,
  HydrodynamicSeparator = 11,
  InfiltrationBasin = 12,
  InfiltrationTrench = 13,
  LowFlowDiversions = 14,
  PermeablePavement = 15,
  ProprietaryBiotreatment = 16,
  ProprietaryTreatmentControl = 17,
  SandFilters = 18,
  UndergroundInfiltration = 19,
  VegetatedFilterStrip = 20,
  VegetatedSwale = 21,
  WetDetentionBasin = 22
}

export const TreatmentBMPModelingTypes: LookupTableEntry[]  = [
  { Name: "BioinfiltrationBioretentionWithRaisedUnderdrain", DisplayName: "Bioinfiltration (bioretention with raised underdrain)", Value: 1 },
  { Name: "BioretentionWithNoUnderdrain", DisplayName: "Bioretention with no Underdrain", Value: 2 },
  { Name: "BioretentionWithUnderdrainAndImperviousLiner", DisplayName: "Bioretention with Underdrain and Impervious Liner", Value: 3 },
  { Name: "CisternsForHarvestAndUse", DisplayName: "Cisterns for Harvest and Use", Value: 4 },
  { Name: "ConstructedWetland", DisplayName: "Constructed Wetland", Value: 5 },
  { Name: "DryExtendedDetentionBasin", DisplayName: "Dry Extended Detention Basin", Value: 6 },
  { Name: "DryWeatherTreatmentSystems", DisplayName: "Dry Weather Treatment Systems", Value: 7 },
  { Name: "Drywell", DisplayName: "Drywell", Value: 8 },
  { Name: "FlowDurationControlBasin", DisplayName: "Flow Duration Control Basin", Value: 9 },
  { Name: "FlowDurationControlTank", DisplayName: "Flow Duration Control Tank", Value: 10 },
  { Name: "HydrodynamicSeparator", DisplayName: "Hydrodynamic Separator", Value: 11 },
  { Name: "InfiltrationBasin", DisplayName: "Infiltration Basin", Value: 12 },
  { Name: "InfiltrationTrench", DisplayName: "Infiltration Trench", Value: 13 },
  { Name: "LowFlowDiversions", DisplayName: "Low Flow Diversions", Value: 14 },
  { Name: "PermeablePavement", DisplayName: "Permeable Pavement", Value: 15 },
  { Name: "ProprietaryBiotreatment", DisplayName: "Proprietary Biotreatment", Value: 16 },
  { Name: "ProprietaryTreatmentControl", DisplayName: "Proprietary Treatment Control", Value: 17 },
  { Name: "SandFilters", DisplayName: "Sand Filters", Value: 18 },
  { Name: "UndergroundInfiltration", DisplayName: "Underground Infiltration", Value: 19 },
  { Name: "VegetatedFilterStrip", DisplayName: "Vegetated Filter Strip", Value: 20 },
  { Name: "VegetatedSwale", DisplayName: "Vegetated Swale", Value: 21 },
  { Name: "WetDetentionBasin", DisplayName: "Wet Detention Basin", Value: 22 }
];
export const TreatmentBMPModelingTypesAsSelectDropdownOptions = TreatmentBMPModelingTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
