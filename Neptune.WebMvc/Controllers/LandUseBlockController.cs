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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Common.MvcResults;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.LandUseBlock;
using Index = Neptune.WebMvc.Views.LandUseBlock.Index;

namespace Neptune.WebMvc.Controllers
{
    //[Area("Trash")]
    //[Route("[area]/[controller]/[action]", Name = "[area]_[controller]_[action]")]
    public class LandUseBlockController : NeptuneBaseController<LandUseBlockController>
    {
        public LandUseBlockController(NeptuneDbContext dbContext, ILogger<LandUseBlockController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [JurisdictionEditFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.LandUseBlock);
            var landUseBlockBulkUploadUrl = SitkaRoute<LandUseBlockUploadController>.BuildUrlFromExpression(_linkGenerator, x => x.UpdateLandUseBlockGeometry());
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, neptunePage, landUseBlockBulkUploadUrl);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [JurisdictionEditFeature]
        public GridJsonNetJObjectResult<LandUseBlock> LandUseBlockGridJsonData()
        {
            var gridSpec = new LandUseBlockGridSpec(_linkGenerator);
            var stormwaterJurisdictionsPersonCanView = StormwaterJurisdictionPeople.ListViewableStormwaterJurisdictionIDsByPersonForBMPs(_dbContext, CurrentPerson);
            var treatmentBMPs = _dbContext.LandUseBlocks.Include(x => x.TrashGeneratingUnits).Include(x => x.StormwaterJurisdiction).ThenInclude(x => x.Organization).AsNoTracking()
                .Where(x => stormwaterJurisdictionsPersonCanView.Contains(x.StormwaterJurisdictionID)).ToList();
            return new GridJsonNetJObjectResult<LandUseBlock>(treatmentBMPs, gridSpec);
        }
    }
}