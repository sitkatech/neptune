/*-----------------------------------------------------------------------
<copyright file="ManagerDashboardController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;

using System.Collections.Generic;
using System.Linq;
using LtInfo.Common;
using LtInfo.Common.MvcResults;
using Neptune.Web.Views.Shared.ProjectControls;


namespace Neptune.Web.Controllers
{
    public class BulkRowController : NeptuneBaseController
    {
        [CrossAreaRoute]
        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult MarkTreatmentBMPAsVerifiedModal()
        {
            return new ContentResult();
        }

        [CrossAreaRoute]
        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult MarkTreatmentBMPAsVerifiedModal(BulkRowTreatmentBMPViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => viewModel.EntityIDList.Contains(x.TreatmentBMPID)).ToList();
            treatmentBMPs.ForEach(x => x.MarkAsVerified(CurrentPerson));
            var numberOfVerifiedTreatmentBMPs = treatmentBMPs.Count;
            SetMessageForDisplay($"{numberOfVerifiedTreatmentBMPs} BMPs were successfully verified.");
            return new ModalDialogFormJsonResult();
        }

        [CrossAreaRoute]
        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult MarkBMPDelineationAsVerifiedModal()
        {
            return new ContentResult();
        }

        [CrossAreaRoute]
        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult MarkBMPDelineationAsVerifiedModal(BulkRowTreatmentBMPViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            var bmpDelineations = HttpRequestStorage.DatabaseEntities.Delineations.Where(x => viewModel.EntityIDList.Contains(x.DelineationID)).ToList();
            bmpDelineations.ForEach(x => x.MarkAsVerified(CurrentPerson));

            NereidUtilities.MarkDelineationDirty(bmpDelineations, HttpRequestStorage.DatabaseEntities);

            var numberOfVerifiedBMPDelineations = bmpDelineations.Count;

            SetMessageForDisplay($"{numberOfVerifiedBMPDelineations} BMP Delineations were successfully verified.");
            
            return new ModalDialogFormJsonResult();
        }

        [CrossAreaRoute]
        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult MarkFieldVistsVerifiedModal()
        {
            return new ContentResult();
        }

        [CrossAreaRoute]
        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult MarkFieldVistsVerifiedModal(BulkRowFieldVisitViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            var fieldVisits = HttpRequestStorage.DatabaseEntities.FieldVisits.Where(x => viewModel.EntityIDList.Contains(x.FieldVisitID)).ToList();
            fieldVisits.ForEach(x => x.VerifyFieldVisit(CurrentPerson));
            SetMessageForDisplay($"{fieldVisits.Count} Field Visits were successfully verified.");
            return new ModalDialogFormJsonResult();
        }




        [CrossAreaRoute]
        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult BulkRowTreatmentBMPs()
        {
            return new ContentResult();
        }


        [CrossAreaRoute]
        [HttpPost]
        [JurisdictionManageFeature]
        public PartialViewResult BulkRowTreatmentBMPs(BulkRowTreatmentBMPViewModel viewModel)
        {
            var treatmentBMPs = new List<TreatmentBMP>();

            if (viewModel.EntityIDList != null)
            {
                treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => viewModel.EntityIDList.Contains(x.TreatmentBMPID)).OrderBy(x => x.TreatmentBMPName).ToList();
            }
            ModelState.Clear(); // we intentionally want to clear any error messages here since this post route is returning a view
            var viewData = new BulkRowTreatmentBMPViewData(treatmentBMPs, SitkaRoute<BulkRowController>.BuildUrlFromExpression(x => x.MarkTreatmentBMPAsVerifiedModal(null)), "Treatment BMP", "The BMP inventory for the selected BMPs will be marked as Verified until the inventory is updated or a Jurisdiction Manager later flags the data as provisional.");
            return RazorPartialView<BulkRowTreatmentBMP, BulkRowTreatmentBMPViewData, BulkRowTreatmentBMPViewModel>(viewData, viewModel);
        }

        [CrossAreaRoute]
        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult BulkRowBMPDelineation()
        {
            return new ContentResult();
        }

        [CrossAreaRoute]
        [HttpPost]
        [JurisdictionManageFeature]
        public PartialViewResult BulkRowBMPDelineation(BulkRowBMPDelineationViewModel viewModel)
        {
            var bmpDelineations = new List<Delineation>();
            if (viewModel.EntityIDList != null)
            {
                bmpDelineations = HttpRequestStorage.DatabaseEntities.Delineations
                    .Where(x => viewModel.EntityIDList.Contains(x.DelineationID))
                    .ToList().OrderBy(x => x.TreatmentBMP.TreatmentBMPName).ToList();
            }
            ModelState.Clear();
            var viewData = new BulkRowBMPDelineationViewData(bmpDelineations, SitkaRoute<BulkRowController>.BuildUrlFromExpression(x => x.MarkBMPDelineationAsVerifiedModal(null)), "BMP Delineation", "The BMP Delineations for the selected BMP Delineations will be marked as Verified until the delineation is updated or a Jurisdiction Manager later flags the data as provisional.");
            return RazorPartialView<BulkRowBMPDelineation, BulkRowBMPDelineationViewData, BulkRowBMPDelineationViewModel
            >(viewData, viewModel);
        }


        [CrossAreaRoute]
        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult BulkRowFieldVisits()
        {
            return new ContentResult();
        }


        [CrossAreaRoute]
        [HttpPost]
        [JurisdictionManageFeature]
        public PartialViewResult BulkRowFieldVisits(BulkRowFieldVisitViewModel viewModel)
        {
            var fieldVisits = new List<FieldVisit>();

            if (viewModel.EntityIDList != null)
            {
                fieldVisits = HttpRequestStorage.DatabaseEntities.FieldVisits.Where(x => viewModel.EntityIDList.Contains(x.FieldVisitID)).OrderBy(x => x.TreatmentBMP.TreatmentBMPName).ToList();
            }
            ModelState.Clear(); // we intentionally want to clear any error messages here since this post route is returning a view
            var viewData = new BulkRowFieldVisitViewData(fieldVisits, SitkaRoute<BulkRowController>.BuildUrlFromExpression(x => x.MarkFieldVistsVerifiedModal(null)), "Field Visit", "The selected Field Visits will be marked as Verified until the Field Visit is updated or a Jurisdiction Manager later flags the data as provisional.");
            return RazorPartialView<BulkRowFieldVisit, BulkRowFieldVisitViewData, BulkRowFieldVisitViewModel>(viewData, viewModel);
        }
    }
}