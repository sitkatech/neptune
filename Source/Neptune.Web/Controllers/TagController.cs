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

        //[JurisdictionManageFeature]
        //public ViewResult Index()
        //{
        //    var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManagerDashboard);
        //    var viewData = new IndexViewData(CurrentPerson, neptunePage);
        //    return RazorView<Index, IndexViewData>(viewData);
        //}


        [AnonymousUnclassifiedFeature]
        public JsonResult Find(string term)
        {
            //var projectFindResults = GetViewableEIPProjectsFromSearchCriteria(term.Trim());
            //var results = projectFindResults.Take(ProjectsCountLimit).Select(pfr => new ListItem(pfr.Project.GetDisplayName().ToEllipsifiedString(100), pfr.GetFactSheetUrl())).ToList();
            //if (projectFindResults.Count > ProjectsCountLimit)
            //{
            //    results.Add(
            //        new ListItem($"<span style='font-weight:bold'>Displaying {ProjectsCountLimit} of {projectFindResults.Count}</span><span style='color:blue; margin-left:8px'>See All Results</span>",
            //            SitkaRoute<ProjectController>.BuildUrlFromExpression(x => x.Search(term))));
            //}
            return new JsonResult();
        }


        /// <summary>
        /// Dummy get signature so that it can find the post action
        /// </summary>
        [CrossAreaRoute]
        [HttpGet]
        [JurisdictionManageFeature]
        public ContentResult AddTagsToProjectModal()
        {
            return new ContentResult();
        }

        [CrossAreaRoute]
        [HttpPost]
        [JurisdictionManageFeature]
        [AutomaticallyCallEntityFrameworkSaveChangesWhenModelValid]
        public ActionResult AddTagsToProjectModal(BulkRowProjectsViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return new ModalDialogFormJsonResult();
            }
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
            var viewData = new BulkRowProjectsViewData(projectDisplayNames, SitkaRoute<TagController>.BuildUrlFromExpression(x => x.AddTagsToProjectModal(null)));
            return RazorPartialView<BulkRowProjects, BulkRowProjectsViewData, BulkRowProjectsViewModel>(viewData, viewModel);
        }
    }
}