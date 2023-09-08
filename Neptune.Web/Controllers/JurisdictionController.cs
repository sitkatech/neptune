/*-----------------------------------------------------------------------
<copyright file="JurisdictionController.cs" company="Tahoe Regional Planning Agency">
Copyright (c) Tahoe Regional Planning Agency. All rights reserved.
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

using Neptune.Web.Security;
using Neptune.Web.Views.Jurisdiction;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.Common.Mvc;
using Neptune.EFModels.Entities;
using Neptune.Web.Common;
using Neptune.Web.Common.MvcResults;
using Neptune.Web.Services.Filters;
using Neptune.Web.Views.TreatmentBMP;
using Detail = Neptune.Web.Views.Jurisdiction.Detail;
using DetailViewData = Neptune.Web.Views.Jurisdiction.DetailViewData;
using Edit = Neptune.Web.Views.Jurisdiction.Edit;
using EditViewData = Neptune.Web.Views.Jurisdiction.EditViewData;
using EditViewModel = Neptune.Web.Views.Jurisdiction.EditViewModel;
using Index = Neptune.Web.Views.Jurisdiction.Index;
using IndexViewData = Neptune.Web.Views.Jurisdiction.IndexViewData;

namespace Neptune.Web.Controllers
{
    public class JurisdictionController : NeptuneBaseController<JurisdictionController>
    {
        public JurisdictionController(NeptuneDbContext dbContext, ILogger<JurisdictionController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [NeptuneAdminFeature]
        public ViewResult Index()
        {
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.Jurisdiction);
            var viewData = new IndexViewData(HttpContext, _linkGenerator, CurrentPerson, neptunePage);
            return RazorView<Index, IndexViewData>(viewData);
        }

        [NeptuneAdminFeature]
        public GridJsonNetJObjectResult<StormwaterJurisdiction> IndexGridJsonData()
        {
            var jurisdictions = GetJurisdictionsAndGridSpec(out var gridSpec);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<StormwaterJurisdiction>(jurisdictions, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<StormwaterJurisdiction> GetJurisdictionsAndGridSpec(out IndexGridSpec gridSpec)
        {
            gridSpec = new IndexGridSpec(_linkGenerator);
            return StormwaterJurisdictions.List(_dbContext);
        }

        [HttpGet("{stormwaterJurisdictionPrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("stormwaterJurisdictionPrimaryKey")]
        public PartialViewResult Edit([FromRoute] StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey)
        {
            var jurisdiction = stormwaterJurisdictionPrimaryKey.EntityObject;
            var viewModel = new EditViewModel(jurisdiction);
            return ViewEdit(viewModel);
        }

        [HttpPost("{stormwaterJurisdictionPrimaryKey}")]
        [NeptuneAdminFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("stormwaterJurisdictionPrimaryKey")]
        public async Task<IActionResult> Edit([FromRoute] StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey, EditViewModel viewModel)
        {
            var jurisdiction = stormwaterJurisdictionPrimaryKey.EntityObject;
            if (!ModelState.IsValid)
            {
                return ViewEdit(viewModel);
            }
            viewModel.UpdateModel(jurisdiction, CurrentPerson);
            await _dbContext.SaveChangesAsync();
            return new ModalDialogFormJsonResult();
        }

        private PartialViewResult ViewEdit(EditViewModel viewModel)
        {
            var stormwaterJurisdictionPublicBMPVisibilityTypes = StormwaterJurisdictionPublicBMPVisibilityType.All
                .OrderBy(x => x.StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName)
                .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionPublicBMPVisibilityTypeID.ToString(CultureInfo.InvariantCulture),
                    x => x.StormwaterJurisdictionPublicBMPVisibilityTypeDisplayName);
            var stormwaterJurisdictionPublicWQMPVisibilityTypes = StormwaterJurisdictionPublicWQMPVisibilityType.All
                .OrderBy(x => x.StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName)
                .ToSelectListWithEmptyFirstRow(x => x.StormwaterJurisdictionPublicWQMPVisibilityTypeID.ToString(CultureInfo.InvariantCulture),
                    x => x.StormwaterJurisdictionPublicWQMPVisibilityTypeDisplayName);
            var viewData = new EditViewData(stormwaterJurisdictionPublicBMPVisibilityTypes, stormwaterJurisdictionPublicWQMPVisibilityTypes);
            return RazorPartialView<Edit, EditViewData, EditViewModel>(viewData, viewModel);
        }

        [HttpGet("{stormwaterJurisdictionPrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("stormwaterJurisdictionPrimaryKey")]
        public ViewResult Detail([FromRoute] StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey)
        {
            var stormwaterJurisdiction = StormwaterJurisdictions.GetByID(_dbContext, stormwaterJurisdictionPrimaryKey);
            var viewData = new DetailViewData(HttpContext, _linkGenerator, CurrentPerson, stormwaterJurisdiction);
            return RazorView<Detail, DetailViewData>(viewData);        
        }

        [HttpGet("{stormwaterJurisdictionPrimaryKey}")]
        [NeptuneViewFeature]
        [ValidateEntityExistsAndPopulateParameterFilter("stormwaterJurisdictionPrimaryKey")]
        public GridJsonNetJObjectResult<vTreatmentBMPDetailed> JurisdictionTreatmentBMPGridJsonData([FromRoute] StormwaterJurisdictionPrimaryKey stormwaterJurisdictionPrimaryKey)
        {
            var stormwaterJurisdiction = stormwaterJurisdictionPrimaryKey.EntityObject;
            var treatmentBMPs = GetJurisdictionTreatmentBMPsAndGridSpec(out var gridSpec, CurrentPerson, stormwaterJurisdiction);
            var gridJsonNetJObjectResult = new GridJsonNetJObjectResult<vTreatmentBMPDetailed>(treatmentBMPs, gridSpec);
            return gridJsonNetJObjectResult;
        }

        private List<vTreatmentBMPDetailed> GetJurisdictionTreatmentBMPsAndGridSpec(out TreatmentBMPGridSpec gridSpec, Person currentPerson, StormwaterJurisdiction stormwaterJurisdiction)
        {
            gridSpec = new TreatmentBMPGridSpec(currentPerson, false, false, _linkGenerator);
            return _dbContext.vTreatmentBMPDetaileds.ToList()
                .Where(x => currentPerson.IsAssignedToStormwaterJurisdiction(x.StormwaterJurisdictionID) && x.StormwaterJurisdictionID == stormwaterJurisdiction.StormwaterJurisdictionID).ToList();
        }
    }
}
