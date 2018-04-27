﻿/*-----------------------------------------------------------------------
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
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.PersonOrganization;
using LtInfo.Common;
using LtInfo.Common.MvcResults;

namespace Neptune.Web.Controllers
{
    public class PersonOrganizationController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult EditPersonOrganizationPrimaryContacts(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var organizationIDs = person.OrganizationsWhereYouAreThePrimaryContactPerson.Select(org => org.OrganizationID).ToList();
            var viewModel = new EditPersonOrganizationsViewModel(organizationIDs);
            return ViewEditPersonOrganizations(viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditPersonOrganizationPrimaryContacts(PersonPrimaryKey personPrimaryKey, EditPersonOrganizationsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEditPersonOrganizations(viewModel);
            }
            var person = personPrimaryKey.EntityObject;
            viewModel.UpdateModel(person, HttpRequestStorage.DatabaseEntities.Organizations.ToList());
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditPersonOrganizations(EditPersonOrganizationsViewModel viewModel)
        {
            var allOrganizations = HttpRequestStorage.DatabaseEntities.Organizations.GetActiveOrganizations().Select(x => new OrganizationSimple(x)).ToList();
            var viewData = new EditPersonOrganizationsViewData(allOrganizations);
            return RazorPartialView<EditPersonOrganizations, EditPersonOrganizationsViewData, EditPersonOrganizationsViewModel>(viewData, viewModel);
        }
    }
}
