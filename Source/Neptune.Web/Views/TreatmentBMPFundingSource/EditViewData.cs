/*-----------------------------------------------------------------------
<copyright file="EditTreatmentBMPFundingSourceRequestsViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

namespace Neptune.Web.Views.TreatmentBMPFundingSource
{
    public class EditViewData : NeptuneUserControlViewData
    {
        public readonly List<FundingSourceSimple> AllFundingSources;
        public readonly List<TreatmentBMPSimple> AllTreatmentBMPs;
        public readonly int? TreatmentBMPID;
        public readonly int? FundingSourceID;
        public readonly bool FromFundingSource;

        private EditViewData(List<TreatmentBMPSimple> allTreatmentBMPs,
            List<FundingSourceSimple> allFundingSources,
            int? projectID,
            int? fundingSourceID)
        {
            AllFundingSources = allFundingSources;
            TreatmentBMPID = projectID;
            FundingSourceID = fundingSourceID;
            AllTreatmentBMPs = allTreatmentBMPs;
            
            var displayMode = FundingSourceID.HasValue ? EditorDisplayMode.FromFundingSource : EditorDisplayMode.FromTreatmentBMP;
            FromFundingSource = displayMode == EditorDisplayMode.FromFundingSource;
        }

        public EditViewData(TreatmentBMPSimple treatmentBMP,
            List<FundingSourceSimple> allFundingSources)
            : this(new List<TreatmentBMPSimple> { treatmentBMP }, allFundingSources, treatmentBMP.TreatmentBMPID, null)
        {
        }

        public EditViewData(FundingSourceSimple fundingSource, List<TreatmentBMPSimple> allTreatmentBMPs)
            : this(allTreatmentBMPs, new List<FundingSourceSimple> {fundingSource}, null, fundingSource.FundingSourceID)
        {
        }

        public enum EditorDisplayMode
        {
            FromTreatmentBMP,
            FromFundingSource
        }
    }
}
