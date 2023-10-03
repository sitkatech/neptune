/*-----------------------------------------------------------------------
<copyright file="PersonOrganizationController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.PersonOrganization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Services.Filters;

namespace Neptune.WebMvc.Controllers
{
    public class PersonOrganizationController : NeptuneBaseController<PersonOrganizationController>
    {
        public PersonOrganizationController(NeptuneDbContext dbContext, ILogger<PersonOrganizationController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet("{personPrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public PartialViewResult EditPersonOrganizationPrimaryContacts([FromRoute] PersonPrimaryKey personPrimaryKey)
        {
            var organizationIDs = Organizations.ListByPrimaryContactPersonID(_dbContext, personPrimaryKey.PrimaryKeyValue).Select(org => org.OrganizationID).ToList();
            var viewModel = new EditPersonOrganizationsViewModel(organizationIDs);
            return ViewEditPersonOrganizations(viewModel);
        }

        [HttpPost("{personPrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("personPrimaryKey")]
        public async Task<IActionResult> EditPersonOrganizationPrimaryContacts([FromRoute] PersonPrimaryKey personPrimaryKey, EditPersonOrganizationsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditPersonOrganizations(viewModel);
            }
            var person = personPrimaryKey.EntityObject;
            var organizations = _dbContext.Organizations;
            viewModel.UpdateModel(person, organizations);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditPersonOrganizations(EditPersonOrganizationsViewModel viewModel)
        {
            var allOrganizations = Organizations.ListActive(_dbContext).Select(x => x.AsSimpleDto()).ToList();
            var viewData = new EditPersonOrganizationsViewData(allOrganizations);
            return RazorPartialView<EditPersonOrganizations, EditPersonOrganizationsViewData, EditPersonOrganizationsViewModel>(viewData, viewModel);
        }
    }
}
