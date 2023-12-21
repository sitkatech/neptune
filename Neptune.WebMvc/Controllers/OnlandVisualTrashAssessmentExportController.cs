﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessmentExport;

namespace Neptune.WebMvc.Controllers
{
    //[Area("Trash")]
    //[Route("[area]/[controller]/[action]", Name = "[area]_[controller]_[action]")]
    public class OnlandVisualTrashAssessmentExportController : NeptuneBaseController<OnlandVisualTrashAssessmentExportController>
    {
        public OnlandVisualTrashAssessmentExportController(NeptuneDbContext dbContext, ILogger<OnlandVisualTrashAssessmentExportController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
        }

        [HttpGet]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public ViewResult ExportAssessmentGeospatialData()
        {
            var stormwaterJurisdictions = StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson);
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ExportAssessmentGeospatialData);
            var viewData = new ExportAssessmentGeospatialDataViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, neptunePage, stormwaterJurisdictions, _webConfiguration.MapServiceUrl);
            return RazorView<ExportAssessmentGeospatialData, ExportAssessmentGeospatialDataViewData>(
                viewData);
        }

    }
}