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

using Neptune.Web.Common;
using Microsoft.AspNetCore.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Security;
using Neptune.Web.Views.FieldDefinition;
using Neptune.Web.Views.Shared;
using Edit = Neptune.Web.Views.FieldDefinition.Edit;
using EditViewData = Neptune.Web.Views.FieldDefinition.EditViewData;
using EditViewModel = Neptune.Web.Views.FieldDefinition.EditViewModel;
using IndexViewData = Neptune.Web.Views.FieldDefinition.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class FieldDefinitionController : NeptuneBaseController<FieldDefinitionController>
    {
        public FieldDefinitionController(NeptuneDbContext dbContext, ILogger<FieldDefinitionController> logger, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator)
        {
        }

        [FieldDefinitionViewListFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson, _linkGenerator);
            return RazorView<Views.FieldDefinition.Index, IndexViewData>(viewData);
        }

        [FieldDefinitionViewListFeature]
        public GridJsonNetJObjectResult<FieldDefinitionType> IndexGridJsonData()
        {
            var gridSpec = new FieldDefinitionGridSpec(new FieldDefinitionViewListFeature().HasPermissionByPerson(CurrentPerson), _linkGenerator);
            var fieldDefinitionTypes = FieldDefinitionType.All.Where(x => new FieldDefinitionManageFeature().HasPermissionByPerson(CurrentPerson)).OrderBy(x => x.GetFieldDefinitionLabel()).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldDefinitionType>(fieldDefinitionTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{fieldDefinitionTypePrimaryKey}")]
        [FieldDefinitionManageFeature]
        public ViewResult Edit([FromRoute] FieldDefinitionTypePrimaryKey fieldDefinitionTypePrimaryKey)
        {
            var fieldDefinitionData = FieldDefinitions.GetFieldDefinitionByFieldDefinitionType(_dbContext, fieldDefinitionTypePrimaryKey.PrimaryKeyValue);
            var viewModel = new EditViewModel(fieldDefinitionData);
            return ViewEdit(fieldDefinitionTypePrimaryKey, viewModel);
        }

        [HttpPost("{fieldDefinitionTypePrimaryKey}")]
        [FieldDefinitionManageFeature]
        public async Task<IActionResult> Edit([FromRoute] FieldDefinitionTypePrimaryKey fieldDefinitionTypePrimaryKey, EditViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewEdit(fieldDefinitionTypePrimaryKey, viewModel);
            }
            var fieldDefinition = FieldDefinitions.GetFieldDefinitionByFieldDefinitionType(_dbContext, fieldDefinitionTypePrimaryKey.PrimaryKeyValue);
            if (fieldDefinition == null)
            {
                fieldDefinition = new FieldDefinition() { FieldDefinitionID = fieldDefinitionTypePrimaryKey.PrimaryKeyValue };
                _dbContext.FieldDefinitions.Add(fieldDefinition);
            }

            viewModel.UpdateModel(fieldDefinition);
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay("Field Definition successfully saved.");
            return RedirectToAction(new SitkaRoute<FieldDefinitionController>(x => x.Edit(fieldDefinition.FieldDefinitionTypeID), _linkGenerator));
        }

        private ViewResult ViewEdit(FieldDefinitionTypePrimaryKey fieldDefinitionTypePrimaryKey, EditViewModel viewModel)
        {
            var viewData = new EditViewData(CurrentPerson, fieldDefinitionTypePrimaryKey.EntityObject, _linkGenerator);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{fieldDefinitionTypeID}")]
        [FieldDefinitionViewFeature]
        //[CrossAreaRoute]
        public PartialViewResult FieldDefinitionDetails([FromRoute] int fieldDefinitionTypeID)
        {
            var fieldDefinitionType = FieldDefinitionType.AllLookupDictionary[fieldDefinitionTypeID];
            var fieldDefinition = FieldDefinitions.GetFieldDefinitionByFieldDefinitionType(_dbContext, fieldDefinitionTypeID);
            var showEditLink = new FieldDefinitionManageFeature().HasPermissionByPerson(CurrentPerson); 
            var viewData = new FieldDefinitionDetailsViewData(fieldDefinitionType, fieldDefinition, showEditLink, _linkGenerator);
            return RazorPartialView<FieldDefinitionDetails, FieldDefinitionDetailsViewData>(viewData);
        }
    }
}
