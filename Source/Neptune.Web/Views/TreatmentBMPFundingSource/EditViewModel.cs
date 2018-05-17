/*-----------------------------------------------------------------------
<copyright file="EditProjectFundingSourceRequestsViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FundingEventFundingSource
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public List<FundingEventFundingSourceSimple> FundingEventFundingSources { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(List<Models.FundingEventFundingSource> fundingEventFundingSources)
        {
            FundingEventFundingSources = fundingEventFundingSources.Select(x => new FundingEventFundingSourceSimple(x)).ToList();
        }

        public void UpdateModel(List<Models.FundingEventFundingSource> currentFundingEventFundingSources, IList<Models.FundingEventFundingSource> allFundingEventFundingSources)
        {
            var fundingEventFundingSourcesUpdates = new List<Models.FundingEventFundingSource>();
            if (FundingEventFundingSources != null)
            {
                // Completely rebuild the list
                fundingEventFundingSourcesUpdates = FundingEventFundingSources.Select(x => x.ToFundingEventFundingSource()).ToList();
            }

            currentFundingEventFundingSources.Merge(fundingEventFundingSourcesUpdates,
                allFundingEventFundingSources,
                (x, y) => x.FundingEventID == y.FundingEventID && x.FundingSourceID == y.FundingSourceID,
                (x, y) =>
                {
                    x.Amount = y.Amount;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            if (FundingEventFundingSources != null)
            {
                if (FundingEventFundingSources.GroupBy(x => x.FundingSourceID).Any(x => x.Count() > 1))
                {
                    validationResults.Add(new ValidationResult("Each funding source can only be used once."));
                }
            }
            return validationResults;
        }
    }
}
