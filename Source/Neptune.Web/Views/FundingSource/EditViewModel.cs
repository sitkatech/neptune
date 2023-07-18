﻿/*-----------------------------------------------------------------------
<copyright file="EditViewModel.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.Models;
using Neptune.Web.Common;
using Neptune.Web.Models;

namespace Neptune.Web.Views.FundingSource
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        [Required]
        public int FundingSourceID { get; set; }

        [Required]
        [StringLength(Models.FundingSource.FieldLengths.FundingSourceName)]
        [DisplayName("Name")]
        public string FundingSourceName { get; set; }

        [Required(ErrorMessage = "The Organization is required.")]
        public int? OrganizationID { get; set; }

        [Required(ErrorMessage = "Specify whether the Funding Source is Active.")]
        [DisplayName("Active?")]
        public bool? IsActive { get; set; }

        [StringLength(Models.FundingSource.FieldLengths.FundingSourceDescription)]
        [DisplayName("Description")]
        public string FundingSourceDescription { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(Models.FundingSource fundingSource)
        {
            FundingSourceID = fundingSource.FundingSourceID;
            FundingSourceName = fundingSource.FundingSourceName;
            FundingSourceDescription = fundingSource.FundingSourceDescription;
            OrganizationID = fundingSource.OrganizationID;
            IsActive = fundingSource.IsActive;
        }

        public void UpdateModel(Models.FundingSource fundingSource, Person currentPerson)
        {
            fundingSource.FundingSourceName = FundingSourceName;
            fundingSource.FundingSourceDescription = FundingSourceDescription;
            fundingSource.OrganizationID = OrganizationID ?? ModelObjectHelpers.NotYetAssignedID; // should never be null due to Required Validation Attribute
            fundingSource.IsActive = IsActive ?? false; // should never be null due to Required Validation Attribute
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();

            var existingFundingSources = HttpRequestStorage.DatabaseEntities.FundingSources.Where(x => x.OrganizationID == OrganizationID).ToList();
            if (!Models.FundingSource.IsFundingSourceNameUnique(existingFundingSources, FundingSourceName, FundingSourceID))
            {
                errors.Add(new SitkaValidationResult<EditViewModel, string>(NeptuneValidationMessages.FundingSourceNameUnique, x => x.FundingSourceName));
            }

            var currentPerson = HttpRequestStorage.Person;
            if (new List<Models.Role> {Models.Role.Admin, Models.Role.SitkaAdmin}.All(x => x.RoleID != currentPerson.RoleID) && currentPerson.OrganizationID != OrganizationID)
            {
                var errorMessage = $"You cannnot create a {FieldDefinitionType.FundingSource.GetFieldDefinitionLabel()} for an {FieldDefinitionType.Organization.GetFieldDefinitionLabel()} other than your own.";
                errors.Add(new SitkaValidationResult<EditViewModel, int?>(errorMessage, x => x.OrganizationID));
            }



            return errors;
        }
    }
}
