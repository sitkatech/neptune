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

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Security;
using Neptune.Web.Views.ManagerDashboard;
using Index = Neptune.Web.Views.ManagerDashboard.Index;

namespace Neptune.Web.Controllers
{
    public class ManagerDashboardController : NeptuneBaseController<ManagerDashboardController>
    {
        public ManagerDashboardController(NeptuneDbContext dbContext, ILogger<ManagerDashboardController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ManagerDashboard);
            var stormwaterJurisdictionIDs = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
            var fieldVisitCount = vFieldVisitDetaileds.GetProvisionalFieldVisits(_dbContext, stormwaterJurisdictionIDs).Count;
            var treatmentBMPsCount = TreatmentBMPs.GetProvisionalTreatmentBMPs(_dbContext, CurrentPerson).Count;
            var bmpDelineationsCount = Delineations.GetProvisionalBMPDelineations(_dbContext, CurrentPerson).Count;
            var viewData = new IndexViewData(HttpContext, _linkGenerator, _webConfiguration, CurrentPerson, neptunePage, fieldVisitCount, treatmentBMPsCount, bmpDelineationsCount);
            return RazorView<Index, IndexViewData>(viewData);

        }

        [HttpGet("{gridName}")]
        [JurisdictionManageFeature]
        public GridJsonNetJObjectResult<vFieldVisitDetailed> AllFieldVisitsGridJsonData([FromRoute] string gridName)
        {
            var gridSpec = new ProvisionalFieldVisitGridSpec(CurrentPerson, gridName, _linkGenerator);
            var fieldVisits = vFieldVisitDetaileds.GetProvisionalFieldVisits(_dbContext, CurrentPerson).OrderBy(x => x.TreatmentBMPName).ThenBy(x => x.VisitDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vFieldVisitDetailed>(fieldVisits, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [HttpGet("{gridName}")]
        [JurisdictionManageFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> ProvisionalTreatmentBMPGridJsonData([FromRoute] string gridName)
        {
            var gridSpec = new ProvisionalTreatmentBMPGridSpec(_linkGenerator, CurrentPerson, gridName);
            var treatmentBMPs = TreatmentBMPs.GetProvisionalTreatmentBMPs(_dbContext, CurrentPerson);
            return new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBMPs, gridSpec);
        }

        [HttpGet("{gridName}")]
        [JurisdictionManageFeature]
        public GridJsonNetJObjectResult<Delineation> ProvisionalBMPDelineationsGridJson([FromRoute] string gridName)
        {
            var gridSpec = new ProvisionalBMPDelineationsGridSpec(_linkGenerator, CurrentPerson, gridName);
            var bmpDelineations = Delineations.GetProvisionalBMPDelineations(_dbContext, CurrentPerson);
                return new GridJsonNetJObjectResult<Delineation>(bmpDelineations, gridSpec);
        }
    }
}