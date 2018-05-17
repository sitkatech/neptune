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

namespace Neptune.Web.Views.TreatmentBMPFundingSource
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public List<TreatmentBMPFundingSourceSimple> TreatmentBMPFundingSources { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(List<Models.TreatmentBMPFundingSource> treatmentBMPFundingSources)
        {
            TreatmentBMPFundingSources = treatmentBMPFundingSources.Select(x => new TreatmentBMPFundingSourceSimple(x)).ToList();
        }

        public void UpdateModel(List<Models.TreatmentBMPFundingSource> currentTreatmentBMPFundingSources, IList<Models.TreatmentBMPFundingSource> allTreatmentBMPFundingSources)
        {
            var treatmentBMPFundingSourcesUpdates = new List<Models.TreatmentBMPFundingSource>();
            if (TreatmentBMPFundingSources != null)
            {
                // Completely rebuild the list
                treatmentBMPFundingSourcesUpdates = TreatmentBMPFundingSources.Select(x => x.ToTreatmentBMPFundingSource()).ToList();
            }

            currentTreatmentBMPFundingSources.Merge(treatmentBMPFundingSourcesUpdates,
                allTreatmentBMPFundingSources,
                (x, y) => x.TreatmentBMPID == y.TreatmentBMPID && x.FundingSourceID == y.FundingSourceID,
                (x, y) =>
                {
                    x.Amount = y.Amount;
                });
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();
            if (TreatmentBMPFundingSources != null)
            {
                if (TreatmentBMPFundingSources.GroupBy(x => x.FundingSourceID).Any(x => x.Count() > 1))
                {
                    validationResults.Add(new ValidationResult("Each funding source can only be used once."));
                }
            }
            return validationResults;
        }
    }
}
