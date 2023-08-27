/*-----------------------------------------------------------------------
<copyright file="EditFundingEventFundingSourceRequestsViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Globalization;
using LtInfo.Common.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Neptune.EFModels.Entities;
using Neptune.Models.DataTransferObjects;

namespace Neptune.Web.Views.FundingEvent
{
    public class EditViewData : NeptuneUserControlViewData
    {
        public  List<FundingSourceSimpleDto> AllFundingSources { get; }
        public  IEnumerable<SelectListItem> AllFundingEventTypes { get; }
        public  int? TreatmentBMPID { get; }

        public EditViewData(EFModels.Entities.FundingEvent fundingEvent, List<FundingSourceSimpleDto> allFundingSources, IEnumerable<FundingEventType> allFundingEventTypes)
        {
            TreatmentBMPID = fundingEvent.TreatmentBMPID;
            AllFundingSources = allFundingSources;
            AllFundingEventTypes = allFundingEventTypes.ToSelectListWithDisabledEmptyFirstRow(
                x => x.FundingEventTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.FundingEventTypeDisplayName, "Select a Funding Event Type");
        }

        public EditViewData(List<FundingSourceSimpleDto> allFundingSources, IEnumerable<FundingEventType> allFundingEventTypes, EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            TreatmentBMPID = treatmentBMP.TreatmentBMPID;
            AllFundingSources = allFundingSources;
            AllFundingEventTypes = allFundingEventTypes.ToSelectListWithEmptyFirstRow(x => x.FundingEventTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.FundingEventTypeDisplayName.ToString(CultureInfo.InvariantCulture), "Select a Funding Event Type");
        }
    }
}
