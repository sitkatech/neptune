/*-----------------------------------------------------------------------
<copyright file="EditVitalSignBasicsViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.Models;

namespace Neptune.WebMvc.Areas.Trash.Views.OnlandVisualTrashAssessmentArea
{
    public class EditBasicsViewModel : FormViewModel, IValidatableObject
    { 
        [DisplayName("Assessment Area Name")]
        [Required]
        public string AssessmentAreaName { get; set; }

        [DisplayName("Assessment Area Description")]
        public string AssessmentAreaDescription { get; set;}

        [Required]
        public int? AssessmentAreaID { get; set; }

        [Required]
        public int? StormwaterJurisdictionID { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditBasicsViewModel()
        {
        }

        public EditBasicsViewModel(EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            AssessmentAreaName = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName;
            AssessmentAreaDescription = onlandVisualTrashAssessmentArea.AssessmentAreaDescription;
            AssessmentAreaID = onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaID;
            StormwaterJurisdictionID = onlandVisualTrashAssessmentArea.StormwaterJurisdictionID;
        }

        public void UpdateModel(EFModels.Entities.OnlandVisualTrashAssessmentArea onlandVisualTrashAssessmentArea)
        {
            onlandVisualTrashAssessmentArea.OnlandVisualTrashAssessmentAreaName = AssessmentAreaName;
            onlandVisualTrashAssessmentArea.AssessmentAreaDescription = AssessmentAreaDescription;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<NeptuneDbContext>();
            if (dbContext.OnlandVisualTrashAssessmentAreas.AsNoTracking().Any(x => x.OnlandVisualTrashAssessmentAreaID != AssessmentAreaID && x.StormwaterJurisdictionID == StormwaterJurisdictionID && x.OnlandVisualTrashAssessmentAreaName == AssessmentAreaName))
            {
                yield return new SitkaValidationResult<EditBasicsViewModel, string>(
                    "The Assessment Area Name is already in use in this jurisdiction. Choose another name.", x=>x.AssessmentAreaName);
            }
        }
    }
}
