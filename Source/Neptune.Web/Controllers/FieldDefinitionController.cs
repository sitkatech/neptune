/*-----------------------------------------------------------------------
<copyright file="FieldDefinitionController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using System.Linq;
using System.Web.Mvc;
using Neptune.Web.Security;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.FieldDefinition;
using Neptune.Web.Views.Shared;
using LtInfo.Common;
using LtInfo.Common.MvcResults;

namespace Neptune.Web.Controllers
{
    public class FieldDefinitionController : NeptuneBaseController
    {
        [FieldDefinitionViewListFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [FieldDefinitionViewListFeature]
        public GridJsonNetJObjectResult<FieldDefinitionType> IndexGridJsonData()
        {
            var actions = GetFieldDefinitionsAndGridSpec(out var gridSpec, CurrentPerson);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldDefinitionType>(actions, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private static List<FieldDefinitionType> GetFieldDefinitionsAndGridSpec(out FieldDefinitionGridSpec gridSpec, Person currentPerson)
        {
            gridSpec = new FieldDefinitionGridSpec(new FieldDefinitionViewListFeature().HasPermissionByPerson(currentPerson));
            return FieldDefinitionType.All.Where(x => new FieldDefinitionManageFeature().HasPermission(currentPerson, x).HasPermission).OrderBy(x => x.GetFieldDefinitionLabel()).ToList();
        }

        [HttpGet]
        [FieldDefinitionManageFeature]
        public ViewResult Edit(FieldDefinitionTypePrimaryKey fieldDefinitionTypePrimaryKey)
        {
            var fieldDefinitionData = HttpRequestStorage.DatabaseEntities.FieldDefinitions.GetFieldDefinitionByFieldDefinitionType(fieldDefinitionTypePrimaryKey);            
            var viewModel = new EditViewModel(fieldDefinitionData);
            return ViewEdit(fieldDefinitionTypePrimaryKey, viewModel);
        }

        [HttpPost]
        [FieldDefinitionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(FieldDefinitionTypePrimaryKey fieldDefinitionTypePrimaryKey, EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(fieldDefinitionTypePrimaryKey, viewModel);
            }
            var fieldDefinition = HttpRequestStorage.DatabaseEntities.FieldDefinitions.GetFieldDefinitionByFieldDefinitionType(fieldDefinitionTypePrimaryKey);
            if (fieldDefinition == null)
            {
                fieldDefinition = new FieldDefinition(fieldDefinitionTypePrimaryKey.EntityObject);
                HttpRequestStorage.DatabaseEntities.FieldDefinitions.Add(fieldDefinition);
            }

            viewModel.UpdateModel(fieldDefinition);
            SetMessageForDisplay("Field Definition successfully saved.");
            return RedirectToAction(new SitkaRoute<FieldDefinitionController>(x => x.Edit(fieldDefinition.FieldDefinitionTypeID)));
        }

        private ViewResult ViewEdit(FieldDefinitionTypePrimaryKey fieldDefinitionTypePrimaryKey, EditViewModel viewModel)
        {
            var viewData = new EditViewData(CurrentPerson, fieldDefinitionTypePrimaryKey.EntityObject);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [FieldDefinitionViewFeature]
        [CrossAreaRoute]
        public PartialViewResult FieldDefinitionDetails(int fieldDefinitionTypeID)
        {
            var fieldDefinitionType = FieldDefinitionType.AllLookupDictionary[fieldDefinitionTypeID];
            var fieldDefinition = HttpRequestStorage.DatabaseEntities.FieldDefinitions.SingleOrDefault(x => x.FieldDefinitionTypeID == fieldDefinitionTypeID);
            var showEditLink = new FieldDefinitionManageFeature().HasPermission(CurrentPerson, fieldDefinitionType).HasPermission; 
            var viewData = new FieldDefinitionDetailsViewData(fieldDefinitionType, fieldDefinition, showEditLink);
            return RazorPartialView<FieldDefinitionDetails, FieldDefinitionDetailsViewData>(viewData);
        }
    }
}
