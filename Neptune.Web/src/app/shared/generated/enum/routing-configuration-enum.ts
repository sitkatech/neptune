//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[RoutingConfiguration]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum RoutingConfigurationEnum {
  Online = 1,
  Offline = 2
}

export const RoutingConfigurations: LookupTableEntry[]  = [
  { Name: "Online", DisplayName: "Online", Value: 1 },
  { Name: "Offline", DisplayName: "Offline", Value: 2 }
];
export const RoutingConfigurationsAsSelectDropdownOptions = RoutingConfigurations.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
