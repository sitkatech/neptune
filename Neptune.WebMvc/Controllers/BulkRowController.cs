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

using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;
using LtInfo.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.Common;
using Neptune.EFModels;
using Neptune.EFModels.Entities;
using Neptune.EFModels.Nereid;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Models;
using Neptune.WebMvc.Views.BulkRow;


namespace Neptune.WebMvc.Controllers
{
    public class BulkRowController : NeptuneBaseController<BulkRowController>
    {
        public BulkRowController(NeptuneDbContext dbContext, ILogger<BulkRowController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult MarkTreatmentBMPAsVerifiedModal()
        {
            return new ContentResult();
        }

        [HttpPost]
        [JurisdictionManageFeature]
        public async Task<IActionResult> MarkTreatmentBMPAsVerifiedModal(BulkRowTreatmentBMPViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            var treatmentBMPs = _dbContext.TreatmentBMPs.Where(x => viewModel.EntityIDList.Contains(x.TreatmentBMPID)).ToList();
            foreach (var treatmentBMP in treatmentBMPs)
            {
                treatmentBMP.MarkAsVerified(CurrentPerson);
            }
            await _dbContext.SaveChangesAsync();
            var numberOfVerifiedTreatmentBMPs = treatmentBMPs.Count;
            SetMessageForDisplay($"{numberOfVerifiedTreatmentBMPs} BMPs were successfully verified.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult MarkDelineationAsVerifiedModal()
        {
            return new ContentResult();
        }

        [HttpPost]
        [JurisdictionManageFeature]
        public async Task<IActionResult> MarkDelineationAsVerifiedModal(BulkRowTreatmentBMPViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            var delineations = _dbContext.Delineations.Where(x => viewModel.EntityIDList.Contains(x.DelineationID)).ToList();
            foreach (var delineation in delineations)
            {
                delineation.MarkAsVerified(CurrentPerson);
            }
            await NereidUtilities.MarkDelineationDirty(delineations, _dbContext);
            var numberOfVerifiedBMPDelineations = delineations.Count;
            SetMessageForDisplay($"{numberOfVerifiedBMPDelineations} BMP Delineations were successfully verified.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult MarkFieldVisitsVerifiedModal()
        {
            return new ContentResult();
        }

        [HttpPost]
        [JurisdictionManageFeature]
        public async Task<IActionResult> MarkFieldVisitsVerifiedModal(BulkRowFieldVisitViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            var fieldVisits = _dbContext.FieldVisits.Where(x => viewModel.EntityIDList.Contains(x.FieldVisitID)).ToList();
            foreach (var fieldVisit in fieldVisits)
            {
                fieldVisit.VerifyFieldVisit();
            }
            await _dbContext.SaveChangesAsync();
            SetMessageForDisplay($"{fieldVisits.Count} Field Visits were successfully verified.");
            return new ModalDialogFormJsonResult();
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult BulkRowTreatmentBMPs()
        {
            return new ContentResult();
        }

        [HttpPost]
        [JurisdictionManageFeature]
        public PartialViewResult BulkRowTreatmentBMPs([FromBody] BulkRowTreatmentBMPViewModel viewModel)
        {
            var treatmentBMPs = new List<TreatmentBMP>();
            if (viewModel.EntityIDList != null)
            {
                treatmentBMPs = TreatmentBMPs.ListByTreatmentBMPIDList(_dbContext, viewModel.EntityIDList).ToList();
            }
            ModelState.Clear(); // we intentionally want to clear any error messages here since this post route is returning a view
            var viewData = new BulkRowTreatmentBMPViewData(treatmentBMPs, SitkaRoute<BulkRowController>.BuildUrlFromExpression(_linkGenerator, x => x.MarkTreatmentBMPAsVerifiedModal(null)), "Treatment BMP", "The BMP inventory for the selected BMPs will be marked as Verified until the inventory is updated or a Jurisdiction Manager later flags the data as provisional.");
            return RazorPartialView<BulkRowTreatmentBMP, BulkRowTreatmentBMPViewData, BulkRowTreatmentBMPViewModel>(viewData, viewModel);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult BulkRowBMPDelineation()
        {
            return new ContentResult();
        }

        [HttpPost]
        [JurisdictionManageFeature]
        public PartialViewResult BulkRowBMPDelineation([FromBody] BulkRowBMPDelineationViewModel viewModel)
        {
            var delineations = new List<Delineation>();
            if (viewModel.EntityIDList != null)
            {
                delineations = Delineations.ListByDelineationIDList(_dbContext, viewModel.EntityIDList);
            }
            ModelState.Clear();
            var viewData = new BulkRowBMPDelineationViewData(delineations, SitkaRoute<BulkRowController>.BuildUrlFromExpression(_linkGenerator, x => x.MarkDelineationAsVerifiedModal(null)), "BMP Delineation", "The BMP Delineations for the selected BMP Delineations will be marked as Verified until the delineation is updated or a Jurisdiction Manager later flags the data as provisional.");
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

        [HttpPost]
        [JurisdictionManageFeature]
        public PartialViewResult BulkRowFieldVisits([FromBody] BulkRowFieldVisitViewModel viewModel)
        {
            var fieldVisits = new List<EFModels.Entities.FieldVisit>();
            if (viewModel.EntityIDList != null)
            {
                fieldVisits = FieldVisits.ListByFieldVisitIDList(_dbContext, viewModel.EntityIDList);
            }
            ModelState.Clear(); // we intentionally want to clear any error messages here since this post route is returning a view
            var viewData = new BulkRowFieldVisitViewData(fieldVisits, SitkaRoute<BulkRowController>.BuildUrlFromExpression(_linkGenerator, x => x.MarkFieldVisitsVerifiedModal(null)), "Field Visit", "The selected Field Visits will be marked as Verified until the Field Visit is updated or a Jurisdiction Manager later flags the data as provisional.");
            return RazorPartialView<BulkRowFieldVisit, BulkRowFieldVisitViewData, BulkRowFieldVisitViewModel>(viewData, viewModel);
        }
    }
}