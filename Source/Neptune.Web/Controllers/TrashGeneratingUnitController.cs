/*-----------------------------------------------------------------------
<copyright file="TrashGeneratingUnitController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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

using LtInfo.Common.MvcResults;
using Neptune.Web.Common;
using Neptune.Web.Models;
using Neptune.Web.Security;
using Neptune.Web.Views.TrashGeneratingUnit;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Index = Neptune.Web.Views.TrashGeneratingUnit.Index;
using IndexViewData = Neptune.Web.Views.TrashGeneratingUnit.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class TrashGeneratingUnitController : NeptuneBaseController
    {
        [HttpGet]
        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var viewData = new IndexViewData(CurrentPerson);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<TrashGeneratingUnit> TrashGeneratingUnitGridJsonData()
        {
            // ReSharper disable once InconsistentNaming
            var treatmentBMPs = GetTrashGeneratingUnitsAndGridSpec(out var gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<TrashGeneratingUnit>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<TrashGeneratingUnit> GetTrashGeneratingUnitsAndGridSpec(out TrashGeneratingUnitGridSpec gridSpec)
        {
            gridSpec = new TrashGeneratingUnitGridSpec();

            return HttpRequestStorage.DatabaseEntities.TrashGeneratingUnits.OrderByDescending(x=>x.LastUpdateDate).Include(x => x.TreatmentBMP)
                .Include(x => x.OnlandVisualTrashAssessmentArea).Include(x => x.LandUseBlock)
                .Include(x => x.StormwaterJurisdiction.Organization).ToList();
        }
    }
}
