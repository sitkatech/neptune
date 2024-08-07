﻿/*-----------------------------------------------------------------------
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

using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.Models;
using Neptune.WebMvc.Models;

namespace Neptune.WebMvc.Views.FundingEvent
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public FundingEventSimple FundingEvent { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(EFModels.Entities.FundingEvent fundingEvent)
        {
            FundingEvent = new FundingEventSimple(fundingEvent);
        }

        public EditViewModel(EFModels.Entities.TreatmentBMP treatmentBMP)
        {
            FundingEvent = new FundingEventSimple(treatmentBMP);
        }

        public void UpdateModel(EFModels.Entities.FundingEvent currentFundingEvent, DbSet<FundingEventFundingSource> allFundingEventFundingSources)
        {
            currentFundingEvent.Description = FundingEvent.Description;
            currentFundingEvent.FundingEventTypeID = FundingEvent.FundingEventTypeID;
            currentFundingEvent.Year = FundingEvent.Year;


            var fundingEventFundingSourcesToUpdate =
                FundingEvent?.FundingEventFundingSources?.Select(x => x.ToFundingEventFundingSource()).ToList() ?? new List<FundingEventFundingSource>();

            currentFundingEvent.FundingEventFundingSources.Merge(fundingEventFundingSourcesToUpdate,
                allFundingEventFundingSources,
                (z, w) => z.FundingEventID == w.FundingEventID && z.FundingSourceID == w.FundingSourceID,
                (z, w) => z.Amount = w.Amount);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (FundingEvent != null)
            {
                if (FundingEvent.FundingEventFundingSources?.GroupBy(y => y.FundingSourceID).Any(y => y.Count() > 1) ?? false)
                {
                    validationResults.Add(new ValidationResult("Each funding source can only be used once."));
                }
            }
            return validationResults;
        }
    }
}
