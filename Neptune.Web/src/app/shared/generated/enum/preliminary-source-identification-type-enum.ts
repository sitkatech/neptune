//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[PreliminarySourceIdentificationType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum PreliminarySourceIdentificationTypeEnum {
  MovingVehicles = 1,
  ParkedCars = 2,
  UncoveredLoads = 3,
  VehiclesOther = 4,
  OverflowingReceptacles = 5,
  TrashDispersal = 6,
  InadequateWasteContainerManagementOther = 7,
  Restaurants = 8,
  ConvenienceStores = 9,
  LiquorStores = 10,
  BusStops = 11,
  SpecialEvents = 12,
  PedestrianLitterOther = 13,
  IllegalDumpingOnLand = 14,
  Homelessencampments = 15,
  IllegalDumpingOther = 16
}

export const PreliminarySourceIdentificationTypes: LookupTableEntry[]  = [
  { Name: "MovingVehicles", DisplayName: "Moving Vehicles", Value: 1 },
  { Name: "ParkedCars", DisplayName: "Parked Cars", Value: 2 },
  { Name: "UncoveredLoads", DisplayName: "Uncovered Loads", Value: 3 },
  { Name: "VehiclesOther", DisplayName: "Vehicles (Other)", Value: 4 },
  { Name: "OverflowingReceptacles", DisplayName: "Overflowing or uncovered receptacles/dumpsters", Value: 5 },
  { Name: "TrashDispersal", DisplayName: "Dispersal of household trash and recyclables around the collection process", Value: 6 },
  { Name: "InadequateWasteContainerManagementOther", DisplayName: "Inadequate Waste Container Management (Other)", Value: 7 },
  { Name: "Restaurants", DisplayName: "Restaurants", Value: 8 },
  { Name: "ConvenienceStores", DisplayName: "Convenience Stores", Value: 9 },
  { Name: "LiquorStores", DisplayName: "Liquor Stores", Value: 10 },
  { Name: "BusStops", DisplayName: "Bus Stops", Value: 11 },
  { Name: "SpecialEvents", DisplayName: "Special Events", Value: 12 },
  { Name: "PedestrianLitterOther", DisplayName: "Pedestrian Litter (Other)", Value: 13 },
  { Name: "IllegalDumpingOnLand", DisplayName: "Illegal dumping on-land", Value: 14 },
  { Name: "Homelessencampments", DisplayName: "Homeless encampments", Value: 15 },
  { Name: "IllegalDumpingOther", DisplayName: "Illegal Dumping (Other)", Value: 16 }
];
export const PreliminarySourceIdentificationTypesAsSelectDropdownOptions = PreliminarySourceIdentificationTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
