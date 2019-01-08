/*-----------------------------------------------------------------------
<copyright file="NeptuneHomePageImageController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using System.Web.Mvc;
using Neptune.Web.Security;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Views.NeptuneHomePageImage;
using Neptune.Web.Views.Shared;
using LtInfo.Common.MvcResults;

namespace Neptune.Web.Controllers
{
    public class NeptuneHomePageImageController : NeptuneBaseController
    {

        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult New()
        {
            var viewModel = new NewViewModel();
            return ViewNew(viewModel);
        }


        private PartialViewResult ViewNew(NewViewModel viewModel)
        {
            var viewData = new NewViewData();
            return RazorPartialView<New, NewViewData, NewViewModel>(viewData, viewModel);
        }


        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult New(NewViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewNew(viewModel);
            }
            var neptuneHomePageImage = new NeptuneHomePageImage(-1, null, 0);
            viewModel.UpdateModel(neptuneHomePageImage, CurrentPerson);
            HttpRequestStorage.DatabaseEntities.NeptuneHomePageImages.Add(neptuneHomePageImage);
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult Edit(NeptuneHomePageImagePrimaryKey neptuneHomePageImagePrimaryKey)
        {
            var neptuneHomePageImage = neptuneHomePageImagePrimaryKey.EntityObject;
            var viewModel = new EditViewModel(neptuneHomePageImage);
            return ViewEdit(neptuneHomePageImage, viewModel);
        }


        private PartialViewResult ViewEdit(NeptuneHomePageImage neptuneHomePageImage, EditViewModel viewModel)
        {
            var viewData = new EditViewData(neptuneHomePageImage);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }


        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Edit(NeptuneHomePageImagePrimaryKey neptuneHomePageImagePrimaryKey, EditViewModel viewModel)
        {
            var neptuneHomePageImage = neptuneHomePageImagePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(neptuneHomePageImage, viewModel);
            }
            viewModel.UpdateModel(neptuneHomePageImage, CurrentPerson);
            return new ModalDialogFormJsonResult();
        }


        [HttpGet]
        [NeptuneAdminFeature]
        public PartialViewResult DeleteNeptuneHomePageImage(NeptuneHomePageImagePrimaryKey neptuneHomePageImagePrimaryKey)
        {
            var neptuneHomePageImage = neptuneHomePageImagePrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(neptuneHomePageImage.NeptuneHomePageImageID);
            return ViewDeleteNeptuneHomePageImage(neptuneHomePageImage, viewModel);
        }


        private PartialViewResult ViewDeleteNeptuneHomePageImage(NeptuneHomePageImage neptuneHomePageImage, ConfirmDialogFormViewModel viewModel)
        {
            var confirmMessage =
                $"Are you sure you want to delete this image? ({neptuneHomePageImage.Caption})";
            var viewData = new ConfirmDialogFormViewData(confirmMessage, true);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }


        [HttpPost]
        [NeptuneAdminFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult DeleteNeptuneHomePageImage(NeptuneHomePageImagePrimaryKey neptuneHomePageImagePrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var neptuneHomePageImage = neptuneHomePageImagePrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDeleteNeptuneHomePageImage(neptuneHomePageImage, viewModel);
            }

            HttpRequestStorage.DatabaseEntities.NeptuneHomePageImages.DeleteNeptuneHomePageImage(neptuneHomePageImage);
            return new ModalDialogFormJsonResult();
        }
      

    }
}
