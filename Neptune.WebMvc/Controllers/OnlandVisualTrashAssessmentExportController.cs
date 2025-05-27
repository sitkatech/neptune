﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Neptune.Common.GeoSpatial;
using Neptune.Common.Services.GDAL;
using Neptune.EFModels.Entities;
using Neptune.WebMvc.Common;
using Neptune.WebMvc.Security;
using Neptune.WebMvc.Views.OnlandVisualTrashAssessmentExport;
using NetTopologySuite.Features;

namespace Neptune.WebMvc.Controllers
{
    public class OnlandVisualTrashAssessmentExportController(
        NeptuneDbContext dbContext,
        ILogger<OnlandVisualTrashAssessmentExportController> logger,
        IOptions<WebConfiguration> webConfiguration,
        LinkGenerator linkGenerator,
        GDALAPIService gdalApiService)
        : NeptuneBaseController<OnlandVisualTrashAssessmentExportController>(dbContext, logger, linkGenerator,
            webConfiguration)
    {
        [HttpGet]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public ViewResult ExportAssessmentGeospatialData()
        {
            var viewModel = new ExportAssessmentGeospatialDataViewModel();
            return ViewExportAssessmentGeospatialData(viewModel);
        }

        [HttpPost]
        [NeptuneViewAndRequiresJurisdictionsFeature]
        public async Task<ActionResult> ExportAssessmentGeospatialData(ExportAssessmentGeospatialDataViewModel viewModel)
        {
            var stormwaterJurisdictionName = _dbContext.StormwaterJurisdictions.Include(x => x.Organization)
                .Single(x => x.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID)
                .GetOrganizationDisplayName();
            var areaFeatureCollection = new FeatureCollection();
            var areas = _dbContext.OnlandVisualTrashAssessmentAreas
                .Include(x => x.OnlandVisualTrashAssessments)
                .Where(x => x.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID).ToList();
            
            foreach (var area in areas)
            {
                var attributesTable = new AttributesTable
                {
                    { "OVTAAreaName", area.OnlandVisualTrashAssessmentAreaName },
                    { "Description", area.AssessmentAreaDescription ?? null },
                    { "CreatedOn", area.OnlandVisualTrashAssessments?.MaxBy(x => x.CreatedDate)?.CreatedDate }
                };
                areaFeatureCollection.Add(new Feature(area.OnlandVisualTrashAssessmentAreaGeometry, attributesTable));
            }

            if (areaFeatureCollection.Count == 0)
            {
                var attributesTable = new AttributesTable
                {
                    { "OVTAAreaName", null },
                    { "Description", null },
                    { "CreatedOn", null }
                };
                areaFeatureCollection.Add(new Feature(null, attributesTable));
            }

            var gdbInput = new GdbInput()
            {
                FileContents = GeoJsonSerializer.SerializeToByteArray(areaFeatureCollection, GeoJsonSerializer.DefaultSerializerOptions),
                LayerName = "ovta-areas",
                CoordinateSystemID = Proj4NetHelper.NAD_83_HARN_CA_ZONE_VI_SRID,
                GeometryTypeName = "POLYGON",
            };

            var jurisdictionName = stormwaterJurisdictionName.Replace(' ', '-');
            var bytes = await gdalApiService.Ogr2OgrInputToGdbAsZip(new GdbInputsToGdbRequestDto()
            {
                GdbInputs = new List<GdbInput> { gdbInput },
                GdbName = $"ovta-export-{jurisdictionName}"
            });


            return File(bytes, "application/zip", $"ovta-export-{jurisdictionName}.zip");
        }

        private ViewResult ViewExportAssessmentGeospatialData(ExportAssessmentGeospatialDataViewModel viewModel)
        {
            var stormwaterJurisdictions = StormwaterJurisdictions.ListViewableByPersonForBMPs(_dbContext, CurrentPerson);
            var neptunePage = NeptunePages.GetNeptunePageByPageType(_dbContext, NeptunePageType.ExportAssessmentGeospatialData);
            var stormwaterJurisdictionIDList = stormwaterJurisdictions.Select(x => x.StormwaterJurisdictionID).ToList();
            var onlandVisualTrashAssessmentAreas = OnlandVisualTrashAssessmentAreas.ListByStormwaterJurisdictionIDList(_dbContext,
                stormwaterJurisdictionIDList).ToLookup(x => x.StormwaterJurisdictionID);
            var onlandVisualTrashAssessments = OnlandVisualTrashAssessments
                .ListByStormwaterJurisdictionIDList(_dbContext, stormwaterJurisdictionIDList)
                .ToLookup(x => x.StormwaterJurisdictionID);
            var uploadOVTAAreaUrl = SitkaRoute<OnlandVisualTrashAssessmentAreaController>.BuildUrlFromExpression(_linkGenerator, x => x.BulkUploadOVTAAreas());
            var viewData = new ExportAssessmentGeospatialDataViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, neptunePage, stormwaterJurisdictions, _webConfiguration.MapServiceUrl, onlandVisualTrashAssessmentAreas, onlandVisualTrashAssessments, uploadOVTAAreaUrl);
            return RazorView<ExportAssessmentGeospatialData, ExportAssessmentGeospatialDataViewData, ExportAssessmentGeospatialDataViewModel>(
                viewData, viewModel);
        }

    }
}