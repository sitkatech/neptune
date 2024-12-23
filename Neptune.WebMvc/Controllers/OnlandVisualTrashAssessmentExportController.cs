using Microsoft.AspNetCore.Mvc;
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
    //[Area("Trash")]
    //[Route("[area]/[controller]/[action]", Name = "[area]_[controller]_[action]")]
    public class OnlandVisualTrashAssessmentExportController : NeptuneBaseController<OnlandVisualTrashAssessmentExportController>
    {
        private readonly GDALAPIService _gdalApiService;
        public OnlandVisualTrashAssessmentExportController(NeptuneDbContext dbContext, ILogger<OnlandVisualTrashAssessmentExportController> logger, IOptions<WebConfiguration> webConfiguration, LinkGenerator linkGenerator, GDALAPIService gdalApiService) : base(dbContext, logger, linkGenerator, webConfiguration)
        {
            _gdalApiService=gdalApiService;
        }

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
            var transectLineFeatureCollection = new FeatureCollection();
            var observationPointFeatureCollection = new FeatureCollection();
            var areas = _dbContext.OnlandVisualTrashAssessmentAreas
                .Include(x => x.OnlandVisualTrashAssessment)
                .Where(x => x.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID).ToList();
            var observations = _dbContext.OnlandVisualTrashAssessmentObservations
                .Include(x => x.OnlandVisualTrashAssessmentObservationPhotos).ThenInclude(
                    onlandVisualTrashAssessmentObservationPhoto =>
                        onlandVisualTrashAssessmentObservationPhoto.FileResource)
                .Include(x => x.OnlandVisualTrashAssessment)
                .ThenInclude(x => x.OnlandVisualTrashAssessmentArea)
                .Where(x => x.OnlandVisualTrashAssessment.StormwaterJurisdictionID == viewModel.StormwaterJurisdictionID).ToList();

            foreach (var area in areas)
            {
                var attributesTable = new AttributesTable
                {
                    { "OVTAAreaName", area.OnlandVisualTrashAssessmentAreaName },
                    { "Description", area.AssessmentAreaDescription ?? null },
                    { "CreatedOn", area.OnlandVisualTrashAssessment?.CreatedDate }
                };
                areaFeatureCollection.Add(new Feature(area.OnlandVisualTrashAssessmentAreaGeometry4326, attributesTable));

                var attributeTable2 = new AttributesTable()
                {
                    { "OVTAAreaName", area.OnlandVisualTrashAssessmentAreaName },
                    { "JurisID", area.StormwaterJurisdictionID }
                };
                transectLineFeatureCollection.Add(new Feature(area.TransectLine4326, attributeTable2));
            }

            foreach (var observation in observations)
            {
                var fileResourceGuid = observation.OnlandVisualTrashAssessmentObservationPhotos.FirstOrDefault()?.FileResource.FileResourceGUID;
                var attributeTable3 = new AttributesTable()
                {
                    { "OVTAAreaName", observation.OnlandVisualTrashAssessment.OnlandVisualTrashAssessmentArea?.OnlandVisualTrashAssessmentAreaName },
                    { "AssessmentID", observation.OnlandVisualTrashAssessment?.OnlandVisualTrashAssessmentID },
                    { "Note", observation.Note },
                    { "JurisID", observation.OnlandVisualTrashAssessment?.StormwaterJurisdictionID },
                    { "JurisName", stormwaterJurisdictionName },
                    { "Score", observation.OnlandVisualTrashAssessment?.OnlandVisualTrashAssessmentScore?.OnlandVisualTrashAssessmentScoreDisplayName },
                    { "CompletedDate", observation.OnlandVisualTrashAssessment?.CompletedDate },
                    { "PhotoUrl", fileResourceGuid != null ? $"/FileResource/DisplayResource/{fileResourceGuid}" : null },
                };
                observationPointFeatureCollection.Add(new Feature(observations.FirstOrDefault(x => x.OnlandVisualTrashAssessmentID == observation.OnlandVisualTrashAssessment?.OnlandVisualTrashAssessmentID)?.LocationPoint4326, attributeTable3));
            }

            var gdbInput = new GdbInput()
            {
                FileContents = GeoJsonSerializer.SerializeToByteArray(areaFeatureCollection, GeoJsonSerializer.DefaultSerializerOptions),
                LayerName = "ovta-areas",
                CoordinateSystemID = Proj4NetHelper.WEB_MERCATOR,
                GeometryTypeName = "POLYGON",
            };
            var gdbInput2 = new GdbInput()
            {
                FileContents = GeoJsonSerializer.SerializeToByteArray(transectLineFeatureCollection, GeoJsonSerializer.DefaultSerializerOptions),
                LayerName = "transect-lines",
                CoordinateSystemID = Proj4NetHelper.WEB_MERCATOR,
                GeometryTypeName = "LINESTRING",
            };
            var gdbInput3 = new GdbInput()
            {
                FileContents = GeoJsonSerializer.SerializeToByteArray(observationPointFeatureCollection, GeoJsonSerializer.DefaultSerializerOptions),
                LayerName = "observation-point",
                CoordinateSystemID = Proj4NetHelper.WEB_MERCATOR,
                GeometryTypeName = "POINT",
            };
            var jurisdictionName = stormwaterJurisdictionName.Replace(' ', '-');
            var bytes = await _gdalApiService.Ogr2OgrInputToGdbAsZip(new GdbInputsToGdbRequestDto()
            {
                GdbInputs = new List<GdbInput> { gdbInput, gdbInput2, gdbInput3 },
                GdbName = $"ovta-export-{jurisdictionName}"
            });


            return File(bytes, "application/zip", $"ovta-export-{jurisdictionName}.gdb.zip");
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
            var viewData = new ExportAssessmentGeospatialDataViewData(HttpContext, _linkGenerator, CurrentPerson, _webConfiguration, neptunePage, stormwaterJurisdictions, _webConfiguration.MapServiceUrl, onlandVisualTrashAssessmentAreas, onlandVisualTrashAssessments);
            return RazorView<ExportAssessmentGeospatialData, ExportAssessmentGeospatialDataViewData, ExportAssessmentGeospatialDataViewModel>(
                viewData, viewModel);
        }

    }
}