/*-----------------------------------------------------------------------
<copyright file="StormwaterUserController.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.StormwaterUser;
using Index = Neptune.Web.Views.StormwaterUser.Index;
using IndexGridSpec = Neptune.Web.Views.StormwaterUser.IndexGridSpec;
using IndexViewData = Neptune.Web.Views.StormwaterUser.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class StormwaterUserController : NeptuneBaseController
    {
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.StormwaterUser);
            var viewData = new IndexViewData(CurrentPerson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<Person> IndexGridJsonData()
        {
            IndexGridSpec gridSpec;
            var persons = GetPersonsAndGridSpec(CurrentPerson, out gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<Person>(persons, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private static List<Person> GetPersonsAndGridSpec(Person currentPerson, out IndexGridSpec gridSpec)
        {
            gridSpec = new IndexGridSpec(currentPerson, HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.ToList().OrderBy(x => x.OrganizationDisplayName));
            return HttpRequestStorage.DatabaseEntities.People.Where(x => x.IsActive).ToList().OrderBy(x => x.FullNameLastFirst).ToList();
        }

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult Edit(PersonPrimaryKey personPrimaryKey)
        {
            var person = personPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(person);
            return ViewEdit(viewModel);
        }

        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(PersonPrimaryKey personPrimaryKey, EditViewModel viewModel)
        {
            var person = personPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }

            HttpRequestStorage.DatabaseEntities.StormwaterJurisdictionPeople.Load();

            viewModel.UpdateModel(person, HttpRequestStorage.DatabaseEntities.AllStormwaterJurisdictionPeople.Local);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var stormwaterRoles = Role.All.ToList();
            var stormwaterJurisdictions = HttpRequestStorage.DatabaseEntities.StormwaterJurisdictions.ToList();
            var viewData = new EditViewData(CurrentPerson, stormwaterRoles, stormwaterJurisdictions);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }
    }
}
