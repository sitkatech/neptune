/*-----------------------------------------------------------------------
<copyright file="NeptunePageController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.WebMvc.Views.NeptunePage;
using Index = Neptune.WebMvc.Views.NeptunePage.Index;
using Neptune.WebMvc.Services.Filters;

namespace Neptune.WebMvc.Controllers
{
    public class NeptunePageController : NeptuneBaseController<NeptunePageController>
    {
        public NeptunePageController(NeptuneDbContext dbContext, ILogger<NeptunePageController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [NeptunePageViewListFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [NeptunePageViewListFeature]
        public GridJsonNetJObjectResult<NeptunePage> IndexGridJsonData()
        {
            var gridSpec = new NeptunePageGridSpec(new NeptunePageViewListFeature().HasPermissionByPerson(CurrentPerson), _linkGenerator);
            var neptunePages = NeptunePages.List(_dbContext)
                .Where(x => new NeptuneAdminFeature().HasPermissionByPerson(CurrentPerson)).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<NeptunePage>(neptunePages, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{neptunePagePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("neptunePagePrimaryKey")]
        //        [CrossAreaRoute]
        public PartialViewResult EditInDialog([FromRoute] NeptunePagePrimaryKey neptunePagePrimaryKey)
        {
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(neptunePage);
            return ViewEdit(viewModel, neptunePage);
        }

        [HttpPost("{neptunePagePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("neptunePagePrimaryKey")]
        public async Task<IActionResult> EditInDialog([FromRoute] NeptunePagePrimaryKey neptunePagePrimaryKey, EditViewModel viewModel)
        {
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, neptunePage);
            }
            viewModel.UpdateModel(neptunePage);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel, NeptunePage neptunePage)
        {
            var tinyMCEToolbar = TinyMCEExtension.TinyMCEToolbarStyle.AllOnOneRowNoMaximize;
            var viewData = new EditViewData(_linkGenerator, tinyMCEToolbar, neptunePage);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{neptunePagePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("neptunePagePrimaryKey")]
//        [CrossAreaRoute]
        public PartialViewResult Edit([FromRoute] NeptunePagePrimaryKey neptunePagePrimaryKey)
        {
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(neptunePage);
            return ViewEdit(viewModel, neptunePage);
        }

        [HttpPost("{neptunePagePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("neptunePagePrimaryKey")]
        public async Task<ActionResult> Edit([FromRoute] NeptunePagePrimaryKey neptunePagePrimaryKey, [FromBody] EditViewModel viewModel)
        {
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel, neptunePage);
            }

            viewModel.UpdateModel(neptunePage);
            await _dbContext.SaveChangesAsync();

            // redirect to previous page
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpGet("{neptunePagePrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("neptunePagePrimaryKey")]
        public PartialViewResult NeptunePageDetails([FromRoute] NeptunePagePrimaryKey neptunePagePrimaryKey)
        {
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            var neptunePageContentHtmlString = neptunePage.NeptunePageContent;
            if (!neptunePage.HasNeptunePageContent())
            {
                neptunePageContentHtmlString = $"No page content for Page \"{neptunePage.NeptunePageType.NeptunePageTypeDisplayName}\".";
            }
            var viewData = new NeptunePageDetailsViewData(neptunePageContentHtmlString);
            return RazorPartialView<NeptunePageDetails, NeptunePageDetailsViewData>(viewData);
        }
    }
}
