//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[NotificationType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/form-field/form-field.component"

export enum NotificationTypeEnum {
  Custom = 1
}

export const NotificationTypes: LookupTableEntry[]  = [
  { Name: "Custom", DisplayName: "Custom Notification", Value: 1 }
];
export const NotificationTypesAsSelectDropdownOptions = NotificationTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
