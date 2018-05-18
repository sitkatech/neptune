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

using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using LtInfo.Common.Mvc;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FundingEvent
{
    public class EditViewData : NeptuneUserControlViewData
    {
        public  List<FundingSourceSimple> AllFundingSources { get; }
        public  IEnumerable<SelectListItem> AllFundingEventTypes { get; }
        public  int? TreatmentBMPID { get; }

        public EditViewData(Models.FundingEvent fundingEvent,
            List<FundingSourceSimple> allFundingSources, List<FundingEventType> allFundingEventTypes)
        {
            AllFundingSources = allFundingSources;
            TreatmentBMPID = fundingEvent.TreatmentBMPID;
            AllFundingEventTypes = allFundingEventTypes.ToSelectListWithEmptyFirstRow(x => x.FundingEventTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.FundingEventTypeDisplayName.ToString(CultureInfo.InvariantCulture), "Select a Funding Source");
        }

        public EditViewData(List<FundingSourceSimple> allFundingSources, List<FundingEventType> allFundingEventTypes, Models.TreatmentBMP treatmentBMP)
        {
            AllFundingSources = allFundingSources;
            AllFundingEventTypes = allFundingEventTypes.ToSelectListWithEmptyFirstRow(x => x.FundingEventTypeID.ToString(CultureInfo.InvariantCulture),
                x => x.FundingEventTypeDisplayName.ToString(CultureInfo.InvariantCulture), "Select a Funding Source");
            TreatmentBMPID = TreatmentBMPID;
        }
    }
}
