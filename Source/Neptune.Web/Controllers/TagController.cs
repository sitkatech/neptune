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
    public class TagController : NeptuneBaseController
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
        public ActionResult MarkTreatmentBMPAsVerifiedModal(BulkRowProjectsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }

            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => viewModel.ProjectIDList.Contains(x.TreatmentBMPID)).ToList();
            treatmentBMPs = treatmentBMPs.Select(x => { x.InventoryIsVerified = true; return x; }).ToList();
            return new ModalDialogFormJsonResult();
        }

       


        [CrossAreaRoute]
        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult BulkRowProjects()
        {
            return new ContentResult();
        }


        [CrossAreaRoute]
        [HttpPost]
        [JurisdictionManageFeature]
        public PartialViewResult BulkRowProjects(BulkRowProjectsViewModel viewModel)
        {
            var projectDisplayNames = new List<string>();

            if (viewModel.ProjectIDList != null)
            {
                var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Where(x => viewModel.ProjectIDList.Contains(x.TreatmentBMPID)).ToList();
                projectDisplayNames = treatmentBMPs.Select(x => x.TreatmentBMPName).ToList();
            }
            ModelState.Clear(); // we intentionally want to clear any error messages here since this post route is returning a view
            var viewData = new BulkRowProjectsViewData(projectDisplayNames, SitkaRoute<TagController>.BuildUrlFromExpression(x => x.MarkTreatmentBMPAsVerifiedModal(null)));
            return RazorPartialView<BulkRowProjects, BulkRowProjectsViewData, BulkRowProjectsViewModel>(viewData, viewModel);
        }
    }
}