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
        public List<FundingEventSimple> FundingEvents { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(List<Models.FundingEvent> fundingEvents)
        {
            FundingEvents = fundingEvents.Select(x => new FundingEventSimple(x)).ToList();
        }

        public void UpdateModel(List<Models.FundingEvent> currentFundingEvents, IList<Models.FundingEvent> allFundingEvents, IList<Models.FundingEventFundingSource> allFundingEventFundingSources)
        {
            var fundingEventsUpdates = new List<Models.FundingEvent>();
            if (FundingEvents != null)
            {
                // Completely rebuild the list
                fundingEventsUpdates = FundingEvents.Select(x => x.ToFundingEvent()).ToList();
            }

            currentFundingEvents.Merge(fundingEventsUpdates,
                allFundingEvents,
                (x, y) => x.FundingEventID == y.FundingEventID,
                (x, y) =>
                {
                    x.Year = y.Year;

                });

            // todo: probably doesn't actually work lol
            currentFundingEvents.ForEach(x =>
            {
                var updatedFundingEvent =
                    fundingEventsUpdates.SingleOrDefault(y => y.FundingEventID == x.FundingEventID);
                if (updatedFundingEvent == null)
                    return;

                var fundingEventFundingSourcesToUpdate = updatedFundingEvent.FundingEventFundingSources;

                x.FundingEventFundingSources.Merge(fundingEventFundingSourcesToUpdate, allFundingEventFundingSources,
                    (z,w) => z.FundingEventID == w.FundingEventID && z.FundingSourceID == w.FundingSourceID,
                    
                    (z,w) => z.Amount = w.Amount);
            });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            if (FundingEvents != null)
            {
                if (FundingEvents.Any(
                    x=>x.FundingEventFundingSources.GroupBy(y => y.FundingSourceID).Any(y => y.Count() > 1)))
                {
                    validationResults.Add(new ValidationResult("Each funding source can only be used once per funding event."));
                }
            }
            return validationResults;
        }
    }
}
