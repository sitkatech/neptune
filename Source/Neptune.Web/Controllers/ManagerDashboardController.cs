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

using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.ManagerDashboard;


namespace Neptune.Web.Controllers
{
    public class ManagerDashboardController : NeptuneBaseController
    {
        [HttpGet]
        [JurisdictionManageFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.ManagerDashboard);
            var fieldVisitCount = HttpRequestStorage.DatabaseEntities.FieldVisits.GetProvisionalFieldVisits(CurrentPerson).Count;
            var treatmentBMPsCount = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.GetProvisionalTreatmentBMPs(CurrentPerson).Count;
            var bmpDelineationsCount = HttpRequestStorage.DatabaseEntities.Delineations
                .GetProvisionalBMPDelineations(CurrentPerson).Count;
            var viewData = new IndexViewData(CurrentPerson, neptunePage, fieldVisitCount, treatmentBMPsCount);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [HttpGet]
        [JurisdictionManageFeature]
        public GridJsonNetJObjectResult<FieldVisit> AllFieldVisitsGridJsonData(string gridName)
        {
            var gridSpec = new ProvisionalFieldVisitGridSpec(CurrentPerson, gridName);
            var fieldVisits = HttpRequestStorage.DatabaseEntities.FieldVisits.GetProvisionalFieldVisits(CurrentPerson).OrderBy(x => x.TreatmentBMP.TreatmentBMPName).ThenBy(x => x.VisitDate).ToList();
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<FieldVisit>(fieldVisits, gridSpec);
            return gridJsonNetJObjectResult;
        }

        [JurisdictionManageFeature]
        public GridJsonNetJObjectResult<TreatmentBMP> ProvisionalTreatmentBMPGridJsonData(string gridName)
        {
            var gridSpec = new ProvisionalTreatmentBMPGridSpec(CurrentPerson, gridName);
            var treatmentBMPs = HttpRequestStorage.DatabaseEntities.TreatmentBMPs.Include(x=>x.TreatmentBMPBenchmarkAndThresholds).GetProvisionalTreatmentBMPs(CurrentPerson);
            return new GridJsonNetJObjectResult<TreatmentBMP>(treatmentBMPs, gridSpec);
        }
    }
}