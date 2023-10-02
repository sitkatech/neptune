/*-----------------------------------------------------------------------
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

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Neptune.Common;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.Models;
using Neptune.Web.Common.Mvc;
using Neptune.Web.Security;
using Neptune.Web.Services;

namespace Neptune.Web.Views.Organization
{
    public class EditViewModel : FormViewModel, IValidatableObject
    {
        public int OrganizationID { get; set; }

        [Required]
        [StringLength(EFModels.Entities.Organization.FieldLengths.OrganizationName)]
        [DisplayName("Name")]
        public string OrganizationName { get; set; }

        [Required]
        [StringLength(EFModels.Entities.Organization.FieldLengths.OrganizationShortName)]
        [DisplayName("Short Name")]
        public string OrganizationShortName { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.OrganizationType)]
        [Required]
        public int? OrganizationTypeID { get; set; }

        [FieldDefinitionDisplay(FieldDefinitionTypeEnum.PrimaryContact)]
        public int? PrimaryContactPersonID { get; set; }

        [Url]
        [DisplayName("Home Page")]
        public string OrganizationUrl { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

        [DisplayName("Logo")]
        [SitkaFileExtensions("jpg|jpeg|gif|png")]
        public IFormFile? LogoFileResourceData { get; set; }

        [DisplayName("Keystone Organization Guid")]
        public Guid? OrganizationGuid { get; set; }

        /// <summary>
        /// Needed by the ModelBinder
        /// </summary>
        public EditViewModel()
        {
        }

        public EditViewModel(EFModels.Entities.Organization organization)
        {
            OrganizationID = organization.OrganizationID;
            OrganizationName = organization.OrganizationName;
            OrganizationShortName = organization.OrganizationShortName;
            OrganizationTypeID = organization.OrganizationTypeID;
            PrimaryContactPersonID = organization.PrimaryContactPerson?.PersonID;
            OrganizationUrl = organization.OrganizationUrl;

            IsActive = organization.IsActive;
            OrganizationGuid = organization.OrganizationGuid;
        }

        public async Task UpdateModel(EFModels.Entities.Organization organization, Person currentPerson, FileResourceService fileResourceService)
        {
            organization.OrganizationName = OrganizationName;
            organization.OrganizationShortName = OrganizationShortName;
            organization.OrganizationTypeID = OrganizationTypeID.Value;
            organization.IsActive = IsActive;
            organization.PrimaryContactPersonID = PrimaryContactPersonID;
            organization.OrganizationUrl = OrganizationUrl;
            if (LogoFileResourceData != null)
            {
                // if it already had a logo, we can delete it safely
                if (organization.LogoFileResource != null)
                {
                    await fileResourceService.DeleteFileResource(organization.LogoFileResource);
                }

                organization.LogoFileResource = await fileResourceService.CreateNewFromIFormFile(LogoFileResourceData, currentPerson);
            }

            var isSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(currentPerson);
            if (isSitkaAdmin)
            {
                organization.OrganizationGuid = OrganizationGuid;
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            if (LogoFileResourceData != null && LogoFileResourceData.Length > MaxLogoSizeInBytes)
            {
                var errorMessage = $"Logo is too large - must be less than {FileUtility.FormatBytes(MaxLogoSizeInBytes)}. Your logo was {FileUtility.FormatBytes(LogoFileResourceData.Length)}.";
                validationResults.Add(new SitkaValidationResult<EditViewModel, IFormFile>(errorMessage, x => x.LogoFileResourceData));
            }

            //todo: organization keystone validation
            //var isSitkaAdmin = new SitkaAdminFeature().HasPermissionByPerson(HttpRequestStorage.Person);
            //if (OrganizationGuid.HasValue && isSitkaAdmin)
            //{
            //    var organization = _dbContext.Organizations.SingleOrDefault(x => x.OrganizationGuid == OrganizationGuid);
            //    if (organization != null && organization.OrganizationID != OrganizationID)
            //    {
            //        validationResults.Add(new SitkaValidationResult<EditViewModel, Guid?>("This Guid is already associated with an Organization", x => x.OrganizationGuid));
            //    }
            //    else
            //    {
            //        try
            //        {
            //            var keystoneClient = new KeystoneDataClient();
            //            var keystoneOrganization = keystoneClient.GetOrganization(OrganizationGuid.Value);
            //        }
            //        catch (Exception)
            //        {
            //            validationResults.Add(new SitkaValidationResult<EditViewModel, Guid?>("Organization Guid not found in Keystone", x => x.OrganizationGuid));
            //        }

            //    }
            //}

            return validationResults;
        }

        public const int MaxLogoSizeInBytes = 1024 * 200;
    }
}
