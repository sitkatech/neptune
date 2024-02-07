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

using Neptune.WebMvc.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.FieldDefinition;
using Neptune.WebMvc.Views.Shared;
using Edit = Neptune.WebMvc.Views.FieldDefinition.Edit;
using EditViewData = Neptune.WebMvc.Views.FieldDefinition.EditViewData;
using EditViewModel = Neptune.WebMvc.Views.FieldDefinition.EditViewModel;
using IndexViewData = Neptune.WebMvc.Views.FieldDefinition.IndexViewData;

namespace Neptune.WebMvc.Controllers
{
    public class FieldDefinitionController : NeptuneBaseController<FieldDefinitionController>
    {
        public FieldDefinitionController(NeptuneDbContext dbContext, ILogger<FieldDefinitionController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [FieldDefinitionViewListFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson);
            return RazorView<Views.FieldDefinition.Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [FieldDefinitionViewListFeature]
        public GridJsonNetJObjectResult<FieldDefinitionType> IndexGridJsonData()
        {
            var gridSpec = new FieldDefinitionGridSpec(new FieldDefinitionViewListFeature().HasPermissionByPerson(CurrentPerson), _linkGenerator);
            var fieldDefinitionTypes = FieldDefinitionType.All.Where(x => new FieldDefinitionManageFeature().HasPermissionByPerson(CurrentPerson)).OrderBy(x => x.GetFieldDefinitionLabel()).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldDefinitionType>(fieldDefinitionTypes, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{fieldDefinitionTypeID}")]
        [FieldDefinitionManageFeature]
        public ViewResult Edit([FromRoute] int fieldDefinitionTypeID)
        {
            var fieldDefinitionType = FieldDefinitionType.AllLookupDictionary[fieldDefinitionTypeID];
            var fieldDefinitionData = FieldDefinitions.GetByFieldDefinitionType(_dbContext, fieldDefinitionTypeID);
            var viewModel = new EditViewModel(fieldDefinitionData);
            return ViewEdit(fieldDefinitionType, viewModel);
        }

        [HttpPost("{fieldDefinitionTypeID}")]
        [FieldDefinitionManageFeature]
        public async Task<IActionResult> Edit([FromRoute] int fieldDefinitionTypeID, string fieldDefinitionValue)
        {
            var viewModel = new EditViewModel()
            {
                FieldDefinitionValue = fieldDefinitionValue
            };

            var fieldDefinitionType = FieldDefinitionType.AllLookupDictionary[fieldDefinitionTypeID];
            if (!ModelState.IsValid)
            {
                return ViewEdit(fieldDefinitionType, viewModel);
            }
            var fieldDefinition = FieldDefinitions.GetByFieldDefinitionTypeWithChangeTracking(_dbContext, fieldDefinitionTypeID);
            if (fieldDefinition == null)
            {
                fieldDefinition = new FieldDefinition { FieldDefinitionTypeID = fieldDefinitionTypeID };
                await _dbContext.FieldDefinitions.AddAsync(fieldDefinition);
            }

            viewModel.UpdateModel(fieldDefinition);
            await _dbContext.SaveChangesAsync();

            SetMessageForDisplay("Field Definition successfully saved.");
            return RedirectToAction(new SitkaRoute<FieldDefinitionController>(_linkGenerator, x => x.Edit(fieldDefinition.FieldDefinitionTypeID)));
        }

        private ViewResult ViewEdit(FieldDefinitionType fieldDefinitionType, EditViewModel viewModel)
        {
            var viewData = new EditViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, fieldDefinitionType);
            return RazorView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{fieldDefinitionTypeID}")]
        [FieldDefinitionViewFeature]
        //[CrossAreaRoute]
        public PartialViewResult FieldDefinitionDetails([FromRoute] int fieldDefinitionTypeID)
        {
            var fieldDefinitionType = FieldDefinitionType.AllLookupDictionary[fieldDefinitionTypeID];
            var fieldDefinition = FieldDefinitions.GetByFieldDefinitionType(_dbContext, fieldDefinitionTypeID);
            var showEditLink = new FieldDefinitionManageFeature().HasPermissionByPerson(CurrentPerson); 
            var viewData = new FieldDefinitionDetailsViewData(fieldDefinitionType, fieldDefinition, showEditLink, _linkGenerator);
            return RazorPartialView<FieldDefinitionDetails, FieldDefinitionDetailsViewData>(viewData);
        }
    }
}
