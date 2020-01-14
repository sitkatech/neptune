/*-----------------------------------------------------------------------
<copyright file="DelineationController.cs" company="Tahoe Regional Planning Agency">
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

using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.DelineationUpload;
using Neptune.Web.Views.Shared;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Controllers
{
    public class DelineationUploadController : NeptuneBaseController
    {
        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult UpdateDelineationGeometry()
        {
            var viewModel = new UpdateDelineationGeometryViewModel();
            return ViewUpdateDelineationGeometry(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult UpdateDelineationGeometry(UpdateDelineationGeometryViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewData = new UpdateDelineationGeometryViewData(CurrentPerson, null, null, CurrentPerson.GetStormwaterJurisdictionsPersonCanView());
                return RazorPartialView<UpdateDelineationGeometryErrors, UpdateDelineationGeometryViewData, UpdateDelineationGeometryViewModel>(viewData, viewModel);
            }

            viewModel.UpdateModel(CurrentPerson);

            return RedirectToAction(new SitkaRoute<DelineationUploadController>(c => c.ApproveDelineationGisUpload()));
        }

        private ViewResult ViewUpdateDelineationGeometry(UpdateDelineationGeometryViewModel viewModel)
        {
            var newGisUploadUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.UpdateDelineationGeometry());
            var approveGisUploadUrl = SitkaRoute<DelineationUploadController>.BuildUrlFromExpression(c => c.ApproveDelineationGisUpload());

            var viewData = new UpdateDelineationGeometryViewData(CurrentPerson, newGisUploadUrl, approveGisUploadUrl, CurrentPerson.GetStormwaterJurisdictionsPersonCanView());
            return RazorView<UpdateDelineationGeometry, UpdateDelineationGeometryViewData, UpdateDelineationGeometryViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveDelineationGisUpload()
        {
            var viewModel = new ApproveDelineationGisUploadViewModel();
            return ViewApproveDelineationGisUpload(viewModel);
        }

        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult ApproveDelineationGisUpload(ApproveDelineationGisUploadViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewUpdateDelineationGeometry(new UpdateDelineationGeometryViewModel());
            }

            var successfulUploadCount = viewModel.UpdateModel(CurrentPerson, out var stormwaterJurisdictionName);

            SetMessageForDisplay($"{successfulUploadCount} Delineations were successfully uploaded for Jurisdiction {stormwaterJurisdictionName}");

            HttpRequestStorage.DatabaseEntities.SaveChanges();

            HttpRequestStorage.DatabaseEntities.DelineationStagings.DeleteDelineationStaging(CurrentPerson.DelineationStagingsWhereYouAreTheUploadedByPerson);
            HttpRequestStorage.DatabaseEntities.SaveChanges();

            return RedirectToAction(new SitkaRoute<ManagerDashboardController>(c => c.Index()));
        }

        private PartialViewResult ViewApproveDelineationGisUpload(ApproveDelineationGisUploadViewModel viewModel)
        {
            var delineationStagings = CurrentPerson.DelineationStagingsWhereYouAreTheUploadedByPerson.ToList();

            var delineationUpoadGisReportFromStaging = DelineationUploadGisReportJsonResult.GetDelineationUpoadGisReportFromStaging(CurrentPerson, delineationStagings);

            var viewData = new ApproveDelineationGisUploadViewData(CurrentPerson, delineationUpoadGisReportFromStaging);
            return RazorPartialView<ApproveDelineationGisUpload, ApproveDelineationGisUploadViewData, ApproveDelineationGisUploadViewModel>(viewData, viewModel);

        }

        [HttpGet]
        [DelineationDeleteFeature]
        public PartialViewResult Delete(DelineationPrimaryKey delineationPrimaryKey)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            var viewModel = new ConfirmDialogFormViewModel(delineation.DelineationID);
            return ViewDelete(delineation, viewModel);
        }

        [HttpPost]
        [DelineationDeleteFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult Delete(DelineationPrimaryKey delineationPrimaryKey, ConfirmDialogFormViewModel viewModel)
        {
            var delineation = delineationPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewDelete(delineation, viewModel);
            }

            HttpRequestStorage.DatabaseEntities.Delineations.DeleteDelineation(delineation);
            return new ModalDialogFormJsonResult(SitkaRoute<DelineationController>.BuildUrlFromExpression(c => c.DelineationMap(null)));
        }

        private PartialViewResult ViewDelete(Delineation delineation, ConfirmDialogFormViewModel viewModel)
        {
            var canDelete = delineation.CanDelete(CurrentPerson);
            var confirmMessage = canDelete
                ? "Are you sure you want to delete the Delineation?"
                : ConfirmDialogFormViewData.GetStandardCannotDeleteMessage("Delineation", SitkaRoute<TreatmentBMPController>.BuildLinkFromExpression(x => x.Index(), "here"));

            var viewData = new ConfirmDialogFormViewData(confirmMessage, canDelete);
            return RazorPartialView<ConfirmDialogForm, ConfirmDialogFormViewData, ConfirmDialogFormViewModel>(viewData, viewModel);
        }
    }
}
