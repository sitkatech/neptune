//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[SupportRequestType]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum SupportRequestTypeEnum {
  ReportBug = 1,
  ForgotLoginInfo = 2,
  NewOrganization = 3,
  ProvideFeedback = 4,
  RequestOrganizationNameChange = 5,
  Other = 6,
  RequestToChangeUserAccountPrivileges = 7
}

export const SupportRequestTypes: LookupTableEntry[]  = [
  { Name: "ReportBug", DisplayName: "Ran into a bug or problem with this system", Value: 1 },
  { Name: "ForgotLoginInfo", DisplayName: "Can't log in (forgot my username or password, account is locked, etc.)", Value: 2 },
  { Name: "NewOrganization", DisplayName: "Need an Organization added to the list", Value: 3 },
  { Name: "ProvideFeedback", DisplayName: "Provide Feedback on the site", Value: 4 },
  { Name: "RequestOrganizationNameChange", DisplayName: "Request a change to an Organization's name", Value: 5 },
  { Name: "Other", DisplayName: "Other", Value: 6 },
  { Name: "RequestToChangeUserAccountPrivileges", DisplayName: "Request to change user account privileges", Value: 7 }
];
export const SupportRequestTypesAsSelectDropdownOptions = [{ Value: null, Label: "- Select -", Disabled: true }, ...SupportRequestTypes.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption))];
