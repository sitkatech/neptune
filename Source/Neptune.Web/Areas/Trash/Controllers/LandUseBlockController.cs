/*-----------------------------------------------------------------------
<copyright file="LandUseBlockController.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
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
using Neptune.Web.Areas.Trash.Views.LandUseBlock;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;
using Neptune.Web.Security;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Neptune.Web.Areas.Trash.Controllers
{
    public class LandUseBlockController : NeptuneBaseController
    {
        [HttpGet]
        [JurisdictionEditFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePage.GetNeptunePageByPageType(NeptunePageType.LandUseBlock);
            var landUseBlockBulkUploadUrl =
                SitkaRoute<LandUseBlockUploadController>.BuildUrlFromExpression(x => x.UpdateLandUseBlockGeometry());
            var viewData = new IndexViewData(CurrentPerson, neptunePage, landUseBlockBulkUploadUrl);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [JurisdictionEditFeature]
        public GridJsonNetJObjectResult<LandUseBlock> LandUseBlockGridJsonData()
        {
            var treatmentBMPs = GetLandUseBlocksAndGridSpec(out var gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<LandUseBlock>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<LandUseBlock> GetLandUseBlocksAndGridSpec(out LandUseBlockGridSpec gridSpec)
        {
            gridSpec = new LandUseBlockGridSpec();

            var stormwaterJurisdictionsPersonCanEdit = CurrentPerson.GetStormwaterJurisdictionsPersonCanEdit().Select(x=>x.StormwaterJurisdictionID).ToList();

            return HttpRequestStorage.DatabaseEntities.LandUseBlocks.Include(x=>x.TrashGeneratingUnits).Where(x => stormwaterJurisdictionsPersonCanEdit.Contains(x.StormwaterJurisdictionID)).ToList();
        }
    }
}