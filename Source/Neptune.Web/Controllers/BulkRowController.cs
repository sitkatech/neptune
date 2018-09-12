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
using Neptune.Web.Security.Shared;
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
        public ActionResult MarkTreatmentBMPAsVerifiedModal(BulkRowEntityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => viewModel.EntityIDList.Contains(x.TreatmentBMPID)).ToList();
            treatmentBMPs = treatmentBMPs.Select(x => { x.InventoryIsVerified = true; return x; }).ToList();
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
        public ActionResult MarkFieldVistsVerifiedModal(BulkRowEntityViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            var fieldVisit = HttpRequestStorage.DatabaseEntities.FieldVisits.Where(x => viewModel.EntityIDList.Contains(x.FieldVisitID)).ToList();
            fieldVisit = fieldVisit.Select(x => { x.IsFieldVisitVerified = true; return x; }).ToList();
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
        public PartialViewResult BulkRowTreatmentBMPs(BulkRowEntityViewModel viewModel)
        {
            var treatmentBMPDisplayNames = new List<string>();

            if (viewModel.EntityIDList != null)
            {
                var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => viewModel.EntityIDList.Contains(x.TreatmentBMPID)).ToList();
                treatmentBMPDisplayNames = treatmentBMPs.Select(x => x.TreatmentBMPName).OrderBy(x => x).ToList();
            }
            ModelState.Clear(); // we intentionally want to clear any error messages here since this post route is returning a view
            var viewData = new BulkRowEntityViewData(treatmentBMPDisplayNames, SitkaRoute<BulkRowController>.BuildUrlFromExpression(x => x.MarkTreatmentBMPAsVerifiedModal(null)), "Treatment BMP", "The BMP inventory for the selected BMPs will be marked as Verified until the inventory is updated or a Jurisdiction Manager later flags the data as provisional.");
            return RazorPartialView<BulkRowEntity, BulkRowEntityViewData, BulkRowEntityViewModel>(viewData, viewModel);
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
        public PartialViewResult BulkRowFieldVisits(BulkRowEntityViewModel viewModel)
        {
            var fieldVisitDisplayNames = new List<string>();

            if (viewModel.EntityIDList != null)
            {
                var fieldVisits = HttpRequestStorage.DatabaseEntities.FieldVisits.Where(x => viewModel.EntityIDList.Contains(x.FieldVisitID)).ToList();
                fieldVisitDisplayNames = fieldVisits.Select(x => x.TreatmentBMP.TreatmentBMPName).OrderBy(x => x).ToList();
            }
            ModelState.Clear(); // we intentionally want to clear any error messages here since this post route is returning a view
            var viewData = new BulkRowEntityViewData(fieldVisitDisplayNames, SitkaRoute<BulkRowController>.BuildUrlFromExpression(x => x.MarkFieldVistsVerifiedModal(null)), "Field Visit", "The selected Field Visits will be marked as Verified until the Field Visit is updated or a Jurisdiction Manager later flags the data as provisional.");
            return RazorPartialView<BulkRowEntity, BulkRowEntityViewData, BulkRowEntityViewModel>(viewData, viewModel);
        }
    }
}