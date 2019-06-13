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

using System;
using LtInfo.Common.DesignByContract;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.LandUseBlockUpload;
using System.Web.Mvc;
using Hangfire;
using Neptune.Web.ScheduledJobs;

namespace Neptune.Web.Controllers
{
    public class LandUseBlockUploadController : NeptuneBaseController
    {
        [SitkaAdminFeature]
        public RedirectResult TriggerHangfireJob()
        {

            BackgroundJob.Schedule(() =>
                ScheduledBackgroundJobBootstrapper.RunLandUseBlockUploadBackgroundJob(), TimeSpan.FromSeconds(30));

            return RedirectToAction(new SitkaRoute<LandUseBlockController>(c => c.Index()));
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult UpdateLandUseBlockGeometry()
        {
            var viewModel = new UpdateLandUseBlockGeometryViewModel();
            return ViewUpdateLandUseBlockGeometry(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult UpdateLandUseBlockGeometry(UpdateLandUseBlockGeometryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewData = new UpdateLandUseBlockGeometryViewData(CurrentPerson, null);
                return RazorPartialView<UpdateLandUseBlockGeometryErrors, UpdateLandUseBlockGeometryViewData, UpdateLandUseBlockGeometryViewModel>(viewData, viewModel);
            }
            viewModel.UpdateModel(CurrentPerson);

            SetMessageForDisplay("The Land USe Blocks were successfully updated and will be added to the system after processing.");

            return RedirectToAction(new SitkaRoute<LandUseBlockController>(c => c.Index()));
        }

        private ViewResult ViewUpdateLandUseBlockGeometry(UpdateLandUseBlockGeometryViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<LandUseBlockUploadController>.BuildUrlFromExpression(c => c.UpdateLandUseBlockGeometry());

            var viewData = new UpdateLandUseBlockGeometryViewData(CurrentPerson, newGisUploadUrl);
            return RazorView<UpdateLandUseBlockGeometry, UpdateLandUseBlockGeometryViewData, UpdateLandUseBlockGeometryViewModel>(viewData, viewModel);
        }


        [JurisdictionManageFeature]
        public JsonResult UploadGisReport(StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey,
            LandUseBlockGeometryStagingPrimaryKey landUseBlockGeometryStagingPrimaryKey,
            string selectedProperty)
        {
            var stormwaterJurisdiction = stormwaterJurisdictionPrimaryKey.EntityObject;
            var landUseBlockGeometryStaging = landUseBlockGeometryStagingPrimaryKey.EntityObject;

            Check.Assert(landUseBlockGeometryStaging.PersonID == CurrentPerson.PersonID, "LandUseBlock Geometry Staging must belong to the current person");

            return Json(LandUseBlockUploadGisReportJsonResult.GetLandUseBlockUpoadGisReportFromStaging(CurrentPerson, stormwaterJurisdiction, landUseBlockGeometryStaging, selectedProperty));
        }
    }
}