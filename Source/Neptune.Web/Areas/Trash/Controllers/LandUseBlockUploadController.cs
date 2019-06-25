/*-----------------------------------------------------------------------
<copyright file="LandUseBlockController.cs" company="Tahoe Regional Planning Agency">
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

using Neptune.Web.Common;
using Neptune.Web.Security;
using Neptune.Web.Areas.Trash.Views.LandUseBlockUpload;
using System.Web.Mvc;
using Neptune.Web.Controllers;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class LandUseBlockUploadController : NeptuneBaseController
    {

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult UpdateLandUseBlockGeometry()
        {
            var viewModel = new UpdateLandUseBlockGeometryViewModel { PersonID = CurrentPerson.PersonID };
            return ViewUpdateLandUseBlockGeometry(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult UpdateLandUseBlockGeometry(UpdateLandUseBlockGeometryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewUpdateLandUseBlockGeometry(viewModel);
            }

            viewModel.UpdateModel(CurrentPerson);

            SetMessageForDisplay("The Land Use Blocks were successfully added to the staging area. The staged Land Use Blocks will be processed and added to the system. You will receive an email notification when this process completes or if errors in the upload are discovered during processing.");

            return Redirect(SitkaRoute<LandUseBlockController>.BuildUrlFromExpression(c => c.Index()));
        }

        private ViewResult ViewUpdateLandUseBlockGeometry(UpdateLandUseBlockGeometryViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<LandUseBlockUploadController>.BuildUrlFromExpression(c => c.UpdateLandUseBlockGeometry());

            var viewData = new UpdateLandUseBlockGeometryViewData(CurrentPerson, newGisUploadUrl);
            return RazorView<UpdateLandUseBlockGeometry, UpdateLandUseBlockGeometryViewData, UpdateLandUseBlockGeometryViewModel>(viewData, viewModel);
        }
    }
}
