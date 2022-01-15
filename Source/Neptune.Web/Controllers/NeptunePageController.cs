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
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.NeptunePage;
using LtInfo.Common;
using LtInfo.Common.MvcResults;

namespace Neptune.Web.Controllers
{
    public class NeptunePageController : NeptuneBaseController
    {
        [NeptunePageViewListFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptunePageViewListFeature]
        public GridJsonNetJObjectResult<NeptunePage> IndexGridJsonData()
        {
            var gridSpec = new NeptunePageGridSpec(new NeptunePageViewListFeature().HasPermissionByPerson(CurrentPerson));
            var neptunePages = HttpRequestStorage.DatabaseEntities.NeptunePages.ToList()
                .Where(x => new NeptunePageManageFeature().HasPermission(CurrentPerson, x).HasPermission)
                .OrderBy(x => x.NeptunePageType.NeptunePageTypeDisplayName)
                .ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<NeptunePage>(neptunePages, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet]
        [NeptunePageManageFeature]
        [CrossAreaRoute]
        public PartialViewResult EditInDialog(NeptunePagePrimaryKey neptunePagePrimaryKey)
        {
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(neptunePage);
            return ViewEditInDialog(viewModel, neptunePage);
        }

        [HttpPost]
        [NeptunePageManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult EditInDialog(NeptunePagePrimaryKey neptunePagePrimaryKey, EditViewModel viewModel)
        {
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEditInDialog(viewModel, neptunePage);
            }
            viewModel.UpdateModel(neptunePage);
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEditInDialog(EditViewModel viewModel, NeptunePage neptunePage)
        {
            var ckEditorToolbar = CkEditorExtension.CkEditorToolbar.AllOnOneRowNoMaximize;
            var viewData = new EditViewData(ckEditorToolbar,
                SitkaRoute<FileResourceController>.BuildUrlFromExpression(x => x.CkEditorUploadFileResource(neptunePage)));
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [NeptunePageManageFeature]
        public PartialViewResult NeptunePageDetails(NeptunePagePrimaryKey neptunePagePrimaryKey)
        {
            var neptunePage = neptunePagePrimaryKey.EntityObject;
            var neptunePageContentHtmlString = neptunePage.NeptunePageContentHtmlString;
            if (!neptunePage.HasNeptunePageContent())
            {
                neptunePageContentHtmlString = new HtmlString(string.Format("No page content for Page \"{0}\".", neptunePage.NeptunePageType.NeptunePageTypeDisplayName));
            }
            var viewData = new NeptunePageDetailsViewData(neptunePageContentHtmlString);
            return RazorPartialView<NeptunePageDetails, NeptunePageDetailsViewData>(viewData);
        }
    }
}
