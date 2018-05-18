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
using Neptune.Web.Models;

namespace Neptune.Web.Views.FundingEventFundingSource
{
    public class EditViewData : NeptuneUserControlViewData
    {
        public  List<FundingSourceSimple> AllFundingSources { get; }
        public  FundingEvent FundingEvent { get; }
        public  List<FundingEventType> AllFundingEventTypes { get; }
        public  int? TreatmentBMPID { get; }
        public  int? FundingSourceID { get; }
        public  bool FromFundingSource { get; }

        public EditViewData(FundingEvent fundingEvent,
            List<FundingSourceSimple> allFundingSources, List<FundingEventType> allFundingEventTypes)
        {
            AllFundingSources = allFundingSources;
            TreatmentBMPID = fundingEvent.TreatmentBMPID;
            FundingSourceID = null;
            FundingEvent = fundingEvent;
            AllFundingEventTypes = allFundingEventTypes;
            
            var displayMode = FundingSourceID.HasValue ? EditorDisplayMode.FromFundingSource : EditorDisplayMode.FromTreatmentBMP;
            FromFundingSource = displayMode == EditorDisplayMode.FromFundingSource;
        }

        public enum EditorDisplayMode
        {
            FromTreatmentBMP,
            FromFundingSource
        }
    }
}
