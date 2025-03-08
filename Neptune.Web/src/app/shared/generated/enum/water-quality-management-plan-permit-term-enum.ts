//  IMPORTANT:
//  This file is generated. Your changes will be lost.
//  Source Table: [dbo].[WaterQualityManagementPlanPermitTerm]

import { LookupTableEntry } from "src/app/shared/models/lookup-table-entry";
import { SelectDropdownOption } from "src/app/shared/components/inputs/select-dropdown/select-dropdown.component"

export enum WaterQualityManagementPlanPermitTermEnum {
  NorthOCFirstTerm1990 = 1,
  NorthOCSecondTerm1996 = 2,
  NorthOCThirdTerm2002 = 3,
  NorthOCFourthTerm2009 = 4,
  SouthOCFirstTerm1990 = 5,
  SouthOCSecondTerm1996 = 6,
  SouthOCThirdTerm2002 = 7,
  SouthOCFourthTerm2009 = 8,
  SouthOCFithTerm2015 = 9
}

export const WaterQualityManagementPlanPermitTerms: LookupTableEntry[]  = [
  { Name: "NorthOCFirstTerm1990", DisplayName: "North OC 1st Term - 1990", Value: 1 },
  { Name: "NorthOCSecondTerm1996", DisplayName: "North OC 2nd Term - 1996", Value: 2 },
  { Name: "NorthOCThirdTerm2002", DisplayName: "North OC 3rd Term - 2002 (2003 Model WQMP)", Value: 3 },
  { Name: "NorthOCFourthTerm2009", DisplayName: "North OC 4th Term - 2009 (2011 Model WQMP and TGD)", Value: 4 },
  { Name: "SouthOCFirstTerm1990", DisplayName: "South OC 1st Term - 1990", Value: 5 },
  { Name: "SouthOCSecondTerm1996", DisplayName: "South OC 2nd Term - 1996", Value: 6 },
  { Name: "SouthOCThirdTerm2002", DisplayName: "South OC 3rd Term - 2002 (2003 Model WQMP)", Value: 7 },
  { Name: "SouthOCFourthTerm2009", DisplayName: "South OC 4th Term - 2009 (2013 Model WQMP, TGD, and 2012 HMP)", Value: 8 },
  { Name: "SouthOCFithTerm2015", DisplayName: "South OC 5th Term - 2015 (2017 Model WQMP, TGD, and HMP)", Value: 9 }
];
export const WaterQualityManagementPlanPermitTermsAsSelectDropdownOptions = WaterQualityManagementPlanPermitTerms.map((x) => ({ Value: x.Value, Label: x.DisplayName } as SelectDropdownOption));
